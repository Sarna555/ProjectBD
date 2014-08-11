using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Security.Principal;
using System.Security.Permissions;
using Security;


namespace Admin.Logic
{
    public class Administration
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <exception cref="SqlException">When error with database occurs</exception>
        /// <exception cref="InvalidOperationException">When user is not found</exception>
        /// <returns>user</returns>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role=Operation.GetUser)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role=Operation.Admin)]
        public static UserResult GetUser(string login)
        {
            var db = new SQLtoLinqDataContext();
            
            var user = (from u in db.Users
                        where u.login == login
                        select new UserResult
                        {
                            login = u.login,
                            name = u.name,
                            surname = u.surname
                        }).Single();
            return user;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="SqlException">When error with database occurs</exception>
        /// <returns>List of all users</returns>
        //[PrincipalPermissionAttribute(SecurityAction.Demand, Role = "ReadUsers")]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.GetAllUsers)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role=Operation.Admin)]
        public static List<UserResult> GetAllUsers()
        {

            var db = new SQLtoLinqDataContext();
            var userResults = (from p in db.Users
                               select new UserResult
                               {
                                   user_ID = p.user_ID,
                                   name = p.name,
                                   surname = p.surname,
                                   login = p.login
                               }).ToList<UserResult>();
            return userResults;
        }

        /// <summary>
        /// Returns all user operations included in operations and groups
        /// </summary>
        /// <param name="login"></param>
        /// <exception cref="SqlException">When error with database occurs</exception>
        /// <returns>List of all user operations including operations form groups</returns>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.GetAllUserOperations)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role=Operation.Admin)]
        public static List<string> GetAllUserOperations(string login)
        {
            var db = new SQLtoLinqDataContext();
            var result = (from o in db.operations
                          from u2o in db.users2operations
                          from u in db.Users
                          where o.operation_ID == u2o.operation_ID &&
                          u.user_ID == u2o.user_ID && u.login == login
                          select o.name)
                                .Union
                                (from o in db.operations
                                 from g2o in db.groups2operations
                                 from u2g in db.users2groups
                                 from u in db.Users
                                 from g in db.groups
                                 where u.user_ID == u2g.user_ID && g.group_ID == u2g.group_ID &&
                                 g.group_ID == g2o.group_ID && o.operation_ID == g2o.operation_ID &&
                                 u.login == login
                                 select o.name).ToList<string>();
            return result;
            //Nic nie poradze, jak na razie optymalniej nie chce działać :D
        }


        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="SqlException">When error with database occurs</exception>
        /// <returns>List of all groups</returns>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.GetAllGroups)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role=Operation.Admin)]
        public static List<string> GetAllGroups()
        {
            var db = new SQLtoLinqDataContext();
            var result = (from g in db.groups
                          select g.name).ToList<string>();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="SqlException">When error with database occurs</exception>
        /// <returns>List of all operations</returns>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.GetAllOperations)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role=Operation.Admin)]
        public static List<string> GetAllOperations()
        {
            var db = new SQLtoLinqDataContext();
            var result = (from o in db.operations
                          select o.name).ToList<string>();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="SqlException">When error with database occurs</exception>
        /// <returns>List of group operations</returns>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.GetGroupOperations)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role=Operation.Admin)]
        public static List<string> GetGroupOperations(string name)
        {
            var db = new SQLtoLinqDataContext();
            var result = (from g in db.groups
                          from o in db.operations
                          from g2o in db.groups2operations
                          where g.group_ID == g2o.group_ID && g2o.operation_ID == o.operation_ID
                          && g.name == name
                          select o.name).ToList<string>();
            return result;
        }

        /// <summary>
        /// Returns user operations. Operations in group are omitted
        /// </summary>
        /// <param name="login"></param>
        /// <exception cref="SqlException">When error with database occurs</exception>
        /// <returns>user operations</returns>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.GetGroupOperations)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role=Operation.Admin)]
        public static List<string> GetUserOperations(string login)
        {
            var db = new SQLtoLinqDataContext();
            
            var result = (from u in db.Users
                          from o in db.operations
                          from u2o in db.users2operations
                          where u.user_ID == u2o.user_ID && u2o.operation_ID == o.operation_ID
                          && u.login == login
                          select o.name).ToList<string>();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        /// <exception cref="SqlException">When error with database occurs</exception>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.GetUserGroups)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role=Operation.Admin)]
        public static List<string> GetUserGroups(string login)
        {
            var db = new SQLtoLinqDataContext();

            var result = (from u in db.Users
                          from g in db.groups
                          from u2g in db.users2groups
                          where u.user_ID == u2g.user_ID && u2g.group_ID == g.group_ID
                          && u.login == login
                          select g.name).ToList<string>();
            return result;
        }


        /// <summary>
        /// Changes user info, if parameter is null then it will remain unchanged
        /// </summary>
        /// <param name="login"></param>
        /// <param name="newLogin"></param>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="password"></param>
        /// <exception cref="ArgumentNullException">When login is null</exception>
        /// <exception cref="InvalidOperationException">When user is not in database</exception>
        /// <exception cref="SqlException">When error with database occurs</exception>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.UpdateUser)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role=Operation.Admin)]
        public static void UpdateUser(string login, string newLogin, string name, string surname, string password)
        {
            var db = new SQLtoLinqDataContext();
            var result = (from u in db.Users
                          where u.login == login
                          select u).Single();
            if (newLogin != null)
                result.login = newLogin;
            if (name != null)
                result.name = name;
            if (surname != null)
                result.surname = surname;
            if (password != null)
                result.password = UserCtx.Encrypt(password);

            db.SubmitChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="operations">List of ALL user operations</param>
        /// <exception cref="SqlException">When error with database occurs</exception>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.AddUserOperations)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role=Operation.Admin)]
        public static void AddUserOperations(string login, List<string> operations)
        {
            var db = new SQLtoLinqDataContext();
            var result = (from u in db.Users
                          from u2o in db.users2operations
                          where u.login == login && u.user_ID == u2o.user_ID
                          select u2o).ToList<users2operation>();

            db.users2operations.DeleteAllOnSubmit(result);
            db.SubmitChanges();

            foreach (string s in operations)
            {
                var result1 = (from u in db.Users
                               from o in db.operations
                               where u.login == login && o.name == s
                               select new
                               {
                                   user_ID = u.user_ID,
                                   operation_ID = o.operation_ID
                               }).SingleOrDefault();
                if (result1 != null)
                {
                    var temp = new users2operation();
                    temp.user_ID = result1.user_ID;
                    temp.operation_ID = result1.operation_ID;
                    db.users2operations.InsertOnSubmit(temp);
                }

            }
            db.SubmitChanges();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <exception cref="SqlException">When error with database occurs</exception>
        /// <exception cref="Exception">When user already exists</exception>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.AddUser)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role=Operation.Admin)]
        public static void AddUser(string login, string password, string name, string surname)
        {
            var db = new SQLtoLinqDataContext();
            var user = new User();

            var result = (from u in db.Users
                          where login == u.login
                          select u).SingleOrDefault();

            if (result != null)
                throw new Exception("User already exists");

            user.login = login;
            user.password = UserCtx.Encrypt(password);
            user.name = name;
            user.surname = surname;

            db.Users.InsertOnSubmit(user);
            db.SubmitChanges();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="Exception">When group already exists</exception>
        /// <exception cref="SqlException">When error with database occurs</exception>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.AddGroup)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role=Operation.Admin)]
        public static void AddGroup(string name)
        {
            var db = new SQLtoLinqDataContext();
            var newGroup = new group();

            var result = (from g in db.groups
                          where g.name == name
                          select g).SingleOrDefault();

            if (result != null)
                throw new Exception("Group already exists");

            newGroup.name = name;
            db.groups.InsertOnSubmit(newGroup);
            db.SubmitChanges();
        }

        /// <summary>
        /// Changes group info
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException">When group is not in database</exception>
        /// <exception cref="SqlException">When error with database occurs</exception>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.UpdateGroup)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role=Operation.Admin)]
        public static void UpdateGroup(string oldName, string newName)
        {
            var db = new SQLtoLinqDataContext();
            var result = (from g in db.groups
                          where g.name == oldName
                          select g).Single();

            result.name = newName;
            db.SubmitChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="operations"></param>
        /// <exception cref="Exception">When group is not found</exception>
        /// <exception cref="SqlException">When error with database occurs</exception>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.UpdateGroup)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role=Operation.Admin)]
        public static void UpdateGroup(string name, List<string> operations)
        {
            var db = new SQLtoLinqDataContext();
            var result = (from g in db.groups
                          from o in db.operations
                          from g2o in db.groups2operations
                          where g.name == name && g.group_ID == g2o.group_ID
                          && g2o.operation_ID == o.operation_ID
                          select g2o).ToList<groups2operation>();

            db.groups2operations.DeleteAllOnSubmit(result);

            foreach (string oper in operations)
            {
                var result1 = (from g in db.groups
                               from o in db.operations
                               where g.name == name && o.name == oper
                               select new groups2operation
                               {
                                   group_ID = g.group_ID,
                                   operation_ID = o.operation_ID
                               }).SingleOrDefault();
                if (result1 == null)
                    db.groups2operations.InsertOnSubmit(result1);
            }
            db.SubmitChanges();
        }

        /// <summary>
        /// Adds operation to database
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="Exception">When operation already exists</exception>
        /// <exception cref="SqlException">When error with database occurs</exception>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.AddOperation)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role=Operation.Admin)]
        public static void AddOperation(string name)
        {
            var db = new SQLtoLinqDataContext();
            var newOperation = new operation();

            var result = (from o in db.operations
                          where o.name == name
                          select o).SingleOrDefault();
            if (result != null)
                throw new Exception("Operation already exists");

            newOperation.name = name;
            db.operations.InsertOnSubmit(newOperation);
            db.SubmitChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="groups"></param>
        /// <exception cref="SqlException">When error with database occurs</exception>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.AddUserGroups)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role=Operation.Admin)]
        public static void AddUserGroups(string login, List<string> groups)
        {
            var db = new SQLtoLinqDataContext();

            var result = (from u in db.Users
                          from g in db.groups
                          from u2g in db.users2groups
                          where u.login == login && u.user_ID == u2g.user_ID
                          && u2g.group_ID == g.group_ID
                          select u2g);
            db.users2groups.DeleteAllOnSubmit(result);
            db.SubmitChanges();

            foreach (string s in groups)
            {
                var result1 = (from u in db.Users
                               from g in db.groups
                               where u.login == login && g.name == s
                               select new 
                               {
                                   user_ID = u.user_ID,
                                   group_ID = g.group_ID
                               }).SingleOrDefault();
                if (result1 != null)
                {
                    var temp = new users2group();
                    temp.group_ID = result1.group_ID;
                    temp.user_ID = result1.user_ID;
                    db.users2groups.InsertOnSubmit(temp);
                }
            }
            db.SubmitChanges();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <exception cref="SqlException">When error with database occurs</exception>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.DeleteUser)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role=Operation.Admin)]
        public static void DeleteUser(string login)
        {
            var db = new SQLtoLinqDataContext();
            var user = (from u in db.Users
                        where u.login == login
                        select u).Single();

            var operations = (from u2o in db.users2operations
                          where u2o.user_ID == user.user_ID
                          select u2o);
            db.users2operations.DeleteAllOnSubmit(operations);
            db.SubmitChanges();

            var groups = (from u2g in db.users2groups
                      where u2g.user_ID == user.user_ID
                      select u2g);
            db.users2groups.DeleteAllOnSubmit(groups);
            db.SubmitChanges();

            db.Users.DeleteOnSubmit(user);
            db.SubmitChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="SqlException">When error with database occurs</exception>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.DeleteGroup)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role=Operation.Admin)]
        public static void DeleteGroup(string name)
        {
            var db = new SQLtoLinqDataContext();
            var groupa = (from g in db.groups
                         where g.name == name
                         select g).Single();

            var users = (from u2g in db.users2groups
                         where u2g.group_ID == groupa.group_ID
                         select u2g);
            db.users2groups.DeleteAllOnSubmit(users);
            db.SubmitChanges();

            var operations = (from g2o in db.groups2operations
                              where g2o.group_ID == groupa.group_ID
                              select g2o);
            db.groups2operations.DeleteAllOnSubmit(operations);
            db.SubmitChanges();

            db.groups.DeleteOnSubmit(groupa);
            db.SubmitChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="SqlException">When error with database occurs</exception>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.DeleteOperation)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role=Operation.Admin)]
        public static void DeleteOperation(string name)
        {
            var db = new SQLtoLinqDataContext();
            var operation = (from o in db.operations
                         where o.name == name
                         select o).Single();

            var users = (from u2o in db.users2operations
                         where u2o.operation_ID == operation.operation_ID
                         select u2o);
            db.users2operations.DeleteAllOnSubmit(users);
            db.SubmitChanges();

            var groups = (from g2o in db.groups2operations
                              where g2o.operation_ID == operation.operation_ID
                              select g2o);
            db.groups2operations.DeleteAllOnSubmit(groups);
            db.SubmitChanges();

            db.operations.DeleteOnSubmit(operation);
            db.SubmitChanges();
        }

    }
}
