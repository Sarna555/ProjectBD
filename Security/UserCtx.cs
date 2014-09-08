using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Principal;
using System.Security.Cryptography;
using System.IO;
using System.Configuration;

namespace Security
{
    public class UserCtx : IUserCtx
    {
        static readonly string PasswordHash = "S@jW$sk12";
        static readonly string SaltKey = "kW23!1@#maS";
        static readonly string VIKey = "@fB2x3d1e5W6g0H8";

        public String uname { get; private set; }
        public List<string> OpersRoles { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="un"></param>
        public UserCtx(String un)
        {
            uname = un;
            OpersRoles = new List<String>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uname"></param>
        /// <param name="opersRole"></param>
        public UserCtx(String uname, List<String> opersRole)
        {
            this.uname = uname;
            this.OpersRoles = opersRole;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OperRoleName"></param>
        /// <returns></returns>
        public bool HasRoleRight(String operRoleName)
        {
            return this.OpersRoles.Contains(operRoleName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<String> GetAllRoles()
        {
            return OpersRoles;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uc"></param>
        public static void Logout(ref IUserCtx uc)
        {
            // IPrincipal  gp ;
            //gp = Thread.CurrentPrincipal;

            uc = null;
            Thread.CurrentPrincipal = null;
            //IIdentity id = gp.Identity;
            //id = null;
            //gp = null;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uname">login</param>
        /// <param name="pass">password</param>
        /// <param name="uc">User Context</param>
        /// <exception cref="SqlException">When error with database occurs</exception>
        /// <returns>If user exists returns true</returns>
        public static bool Login(String login, String pass, out IUserCtx uc)
        {
            uc = null;

            try
            {
                string lol = ConfigurationManager.ConnectionStrings["AdminDatabase"].ConnectionString;
                var db = new SQLtoLinqDataContext(ConfigurationManager.ConnectionStrings["AdminDatabase"].ConnectionString);
                pass = UserCtx.Encrypt(pass);

                var result = (from p in db.Users
                              where p.login == login && p.password == pass
                              select new UserResult
                              {
                                  user_ID = p.user_ID,
                                  name = p.name,
                                  surname = p.surname,
                                  login = p.login
                              }).SingleOrDefault();
                if (result == null)
                    return false;

                var roles = (from o in db.operations
                             from u2o in db.users2operations
                             from u in db.Users
                             where o.operation_ID == u2o.operation_ID && u.user_ID == u2o.user_ID && u.user_ID == result.user_ID
                             select o.name)
                             .Union
                             (from o in db.operations
                              from g2o in db.groups2operations
                              from u2g in db.users2groups
                              from u in db.Users
                              from g in db.groups
                              where u.user_ID == u2g.user_ID && g.group_ID == u2g.group_ID && g.group_ID == g2o.group_ID && o.operation_ID == g2o.operation_ID && u.user_ID == result.user_ID
                              select o.name).ToList<string>();
                uc = new UserCtx(login, roles);

                GenericIdentity gi = new GenericIdentity(login);

                GenericPrincipal gp = new GenericPrincipal(gi, roles.ToArray());

                Thread.CurrentPrincipal = gp;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            return true;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }
    }
}
