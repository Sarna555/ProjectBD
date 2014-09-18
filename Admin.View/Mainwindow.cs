using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Admin.Logic;
using Security;
using System.Data.SqlClient;
using System.IO;
namespace Admin.View
{
    public partial class Mainwindow : Form
    {
        string choice;

        public Mainwindow()
        {
            InitializeComponent();
            InitialHideComponents();
            checkedListBox2.Sorted = true;
            checkedListBox1.Sorted = true;
        }
        public void InitialHideComponents()
        {
            listBox1.Enabled = false;
            checkedListBox1.Enabled = false;
            checkedListBox2.Enabled = false;
            dodajGrupęToolStripMenuItem.Enabled = false;
            dodajNoweUprawnienieToolStripMenuItem.Enabled = false;
            dodajUżytkownikaToolStripMenuItem.Enabled = false;
            zobaczListęToolStripMenuItem1.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
 
        }
        public void ShowComponents(IUserCtx user)
        {
            var roles = user.GetAllRoles();
            foreach (var role in roles)
            {
                switch (role)
                {
                    case "admin":
                        listBox1.Enabled = true;
                        checkedListBox1.Enabled = true;
                        checkedListBox2.Enabled = true;
                        dodajGrupęToolStripMenuItem.Enabled = true;
                        dodajNoweUprawnienieToolStripMenuItem.Enabled = true;
                        dodajUżytkownikaToolStripMenuItem.Enabled = true;
                        zobaczListęToolStripMenuItem1.Enabled = true;
                        button1.Enabled = true;
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button4.Enabled = true;
                        button5.Enabled = true;
                        break;

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (choice)
            {
                case "group":
                    var editGroup = new EditGroup("exists",this.listBox1.SelectedItem.ToString());
                    var result = editGroup.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        try
                        {
                            this.listBox1.DataSource = Administration.GetAllGroups();
                        }
                        catch (SqlException ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        catch (System.Security.SecurityException se)
                            {
                                MessageBox.Show("Permission denied " + se.Message);
                            } 
                    }

                    break;
                case "user":
                    var userToEdit = Administration.GetUser(listBox1.SelectedItem.ToString());
                    var editUser = new EditUser("exists",userToEdit);
                    var resultuser = editUser.ShowDialog();
                    if (resultuser == DialogResult.OK)
                    {
                        try
                        {
                            var userslist = Administration.GetAllUsers();
                            List<String> usernamelist = new List<String>();
                            foreach (UserResult resultlist in userslist)
                            {
                                usernamelist.Add(resultlist.login);
                            }
                            this.listBox1.DataSource = usernamelist;
                        }
                        catch (System.Security.SecurityException se)
                        {
                            MessageBox.Show("Permission denied " + se.Message);
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    break;
                default:
                    MessageBox.Show("Wybierz element do modyfikacji");
                    break;
            }
        }
        
        private void zalogujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            var logging = new Login();
            var result = logging.ShowDialog();
            if (result == DialogResult.OK)

            {
                string username = logging.ReturnValue1;            //values preserved after close
                string password = logging.ReturnValue2;
         
                bool loggingResult = UserCtx.Login(username, password, out _userCtx);
                ShowComponents(_userCtx);
                if (!loggingResult)
                {
                    MessageBox.Show("Błąd logowania: nieprawidłowy login lub hasło. \nSpróbuj ponownie");
                }
                if (!_userCtx.HasRoleRight(Operation.Admin))
                {
                    MessageBox.Show("Użytkownik nie posiada praw administratora");
                    UserCtx.Logout(ref _userCtx);
                }
            }
        }

        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().Show();
        }


        private void dodajUżytkownikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editUser = new EditUser("new");
             var result = editUser.ShowDialog();
             if (result == DialogResult.OK && this.choice !=null) 
             {
                 var userslist = Administration.GetAllUsers();
                 List<String> usernamelist = new List<String>();
                 foreach (UserResult resultuser in userslist)
                 {
                     usernamelist.Add(resultuser.login);
                 }
                 this.listBox1.DataSource = usernamelist;
             }
        }

        private void zobaczListęToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                choice = "user";
                this.checkedListBox2.Show();
                var userslist = Administration.GetAllUsers();              
                List<String> usernamelist = new List<String>();
                foreach(UserResult result in userslist)
                {
                   usernamelist.Add(result.login);
                }
                this.listBox1.DataSource = usernamelist;
            }
            catch (System.Security.SecurityException se)
            {
                MessageBox.Show("Permission denied " + se.Message);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dodajGrupęToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var adding = new EditGroup("new");
            var result = adding.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    //listBox1.d
                    this.listBox1.DataSource = Administration.GetAllGroups();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (System.Security.SecurityException se)
                {
                    MessageBox.Show("Permission denied " + se.Message);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var allPermissions = Administration.GetAllOperations();
            string login = this.listBox1.SelectedItem.ToString();
            checkedListBox1.Items.Clear();
            checkedListBox2.Items.Clear();
            try
            {

                switch (choice)
                {
                    case "group":
                        var permissionListGroups = Administration.GetGroupOperations(login);
                        foreach (String permission in allPermissions)
                        {
                            checkedListBox1.Items.Add(permission, false);
                        }
                        foreach (String permission in permissionListGroups)
                        {
                            checkedListBox1.Items.Remove(permission);
                            checkedListBox1.Items.Add(permission, true);
                        }

                        break;
                    case "user":
                        var userPermissions = Administration.GetUserOperations(login);
                        var allGroups = Administration.GetAllGroups();
                        var userGroups = Administration.GetUserGroups(login);

                        foreach (String permission in allPermissions)
                        {
                            checkedListBox1.Items.Add(permission, false);
                        }

                        foreach (String permission in userPermissions)
                        {
                            checkedListBox1.Items.Remove(permission);
                            checkedListBox1.Items.Add(permission, true);
                        }
                        foreach (String group in allGroups)
                        {
                            checkedListBox2.Items.Add(group, false);
                        }
                        foreach (String group in userGroups)
                        {
                            checkedListBox2.Items.Remove(group);
                            checkedListBox2.Items.Add(group, true);
                        }
                        break;
                    default:
                        MessageBox.Show("Wybierz użytkownika/grupę");
                        break;
                }

            }
            catch (System.Security.SecurityException se)
            {
                MessageBox.Show("Permission denied: " + se.Message);
            }
        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void zobaczListęToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                choice = "group";
                this.checkedListBox2.Items.Clear();
                this.checkedListBox2.Hide();
                try
                {
                    this.listBox1.DataSource = Administration.GetAllGroups();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (System.Security.SecurityException se)
            {
                MessageBox.Show("Permission denied " + se.Message);
            } 
        }

        private void dodajNoweUprawnienieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var addPermission = new AddPermission();
            var result = addPermission.ShowDialog();
            string login;
            if (result == DialogResult.OK)
            {
                var allPermissions = Administration.GetAllOperations();
                if (this.listBox1.SelectedItem != null)
                {
                    login = this.listBox1.SelectedItem.ToString();
                    try
                    {
                        switch (choice)
                        {
                            case "group":
                                checkedListBox1.Items.Clear();
                                var permissionListGroups = Administration.GetGroupOperations(login);
                                foreach (String permission in allPermissions)
                                {
                                    checkedListBox1.Items.Add(permission, false);
                                }
                                foreach (String permission in permissionListGroups)
                                {
                                    checkedListBox1.Items.Remove(permission);
                                    checkedListBox1.Items.Add(permission, true);
                                }

                                break;
                            case "user":
                                checkedListBox1.Items.Clear();
                                var userPermissions = Administration.GetUserOperations(login);
                                foreach (String permission in allPermissions)
                                {
                                    checkedListBox1.Items.Add(permission, false);
                                }

                                foreach (String permission in userPermissions)
                                {
                                    checkedListBox1.Items.Remove(permission);
                                    checkedListBox1.Items.Add(permission, true);

                                }
                                break;
                            default:
                                MessageBox.Show("Wystąpił błąd");
                                break;
                        }

                    }
                    catch (System.Security.SecurityException se)
                    {
                        MessageBox.Show("Permission denied: " + se.Message);
                    }
                }
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var login = this.listBox1.SelectedItem.ToString();
                var allPermissions = Administration.GetAllOperations();
                switch (choice)
                {
                    case "user":
                        List<string> userGroups = new List<string>();
                        List<string> userPermissions = new List<string>();
                        foreach (String group in checkedListBox2.CheckedItems)
                        {
                            userGroups.Add(group);
                        }
                        foreach (String permission in checkedListBox1.CheckedItems)
                        {
                            userPermissions.Add(permission);
                        }
                        Administration.AddUserOperations(login, userPermissions);
                        Administration.AddUserGroups(login, userGroups);
                        this.checkedListBox1.Items.Clear();
                        this.checkedListBox2.Items.Clear();
                        var allGroups = Administration.GetAllGroups();
                        var permissionListUser = Administration.GetUserOperations(login);
                        foreach (String permission in allPermissions)
                            {
                                checkedListBox1.Items.Add(permission, false);
                            }
                        foreach (String permission in permissionListUser)
                            {
                                checkedListBox1.Items.Remove(permission);
                                checkedListBox1.Items.Add(permission, true);
                            }
                        foreach (String group in allGroups)
                            {
                                checkedListBox2.Items.Add(group, false);
                            }
                        foreach (String group in userGroups)
                            {
                                checkedListBox2.Items.Remove(group);
                                checkedListBox2.Items.Add(group, true);
                             
                            }
                      
                        break;

                    case "group":
                        List<string> groupPermissions = new List<string>();
                        foreach (String permission in checkedListBox1.CheckedItems)
                        {
                            groupPermissions.Add(permission);
                        }
                        
                        Administration.UpdateGroup(login, groupPermissions);
                        checkedListBox1.Items.Clear();
                      
                        var permissionListGroups = Administration.GetGroupOperations(login);
                           foreach (String permission 
                               in allPermissions)
                           {
                               checkedListBox1.Items.Add(permission, false);
                           }
                           foreach (String permission in permissionListGroups)
                           {
                               checkedListBox1.Items.Remove(permission);
                               checkedListBox1.Items.Add(permission, true);
                           }
                        break;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error:\n" + ex.Message);
            }
            catch (System.Security.SecurityException se)
            {
                MessageBox.Show("Permission denied " + se.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void wylogujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserCtx.Logout(ref _userCtx);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (choice)
            {
                case "user":
                    Administration.DeleteUser(this.listBox1.SelectedItem.ToString());
                    try
                    {
                        var userslist = Administration.GetAllUsers();
                        List<String> usernamelist = new List<String>();
                        foreach (UserResult resultlist in userslist)
                        {
                            usernamelist.Add(resultlist.login);
                        }
                        this.listBox1.DataSource = usernamelist;
                    }
                    catch (System.Security.SecurityException se)
                    {
                        MessageBox.Show("Permission denied " + se.Message);
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                case "group":
                    Administration.DeleteGroup(this.listBox1.SelectedItem.ToString());
                    try
                    {
                        this.listBox1.DataSource = Administration.GetAllGroups();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch (System.Security.SecurityException se)
                    {
                        MessageBox.Show("Permission denied " + se.Message);
                    } 
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Administration.DeleteOperation(this.checkedListBox1.SelectedItem.ToString());
            var allPermissions = Administration.GetAllOperations();
            string login = this.listBox1.SelectedItem.ToString();
            try
            {

                switch (choice)
                {
                    case "group":
                        checkedListBox1.Items.Clear();
                        var permissionListGroups = Administration.GetGroupOperations(login);
                        foreach (String permission in allPermissions)
                        {
                            checkedListBox1.Items.Add(permission, false);
                        }
                        foreach (String permission in permissionListGroups)
                        {
                            checkedListBox1.Items.Remove(permission);
                            checkedListBox1.Items.Add(permission, true);
                        }

                        break;
                    case "user":
                        checkedListBox1.Items.Clear();
                        var userPermissions = Administration.GetUserOperations(login);
                        foreach (String permission in allPermissions)
                        {
                            checkedListBox1.Items.Add(permission, false);
                        }

                        foreach (String permission in userPermissions)
                        {
                            checkedListBox1.Items.Remove(permission);
                            checkedListBox1.Items.Add(permission, true);


                        }
                        break;
                    default:
                        MessageBox.Show("Wystąpił błąd");
                        break;
                }

            }
            catch (System.Security.SecurityException se)
            {
                MessageBox.Show("Permission denied: " + se.Message);
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            StreamWriter writeRaport = new StreamWriter("raport.txt");
            var users = Administration.GetAllUsers();
            foreach (UserResult person in users)
            {
                writeRaport.WriteLine(person.name + " " + person.surname);
                writeRaport.WriteLine("Login: " + person.login);
                writeRaport.WriteLine();
                writeRaport.WriteLine("Uprawnienia: ");
                var permissions = Administration.GetAllUserOperations(person.login);
                foreach (string permission in permissions)
                {
                    writeRaport.WriteLine("\t" + permission);
                }
                writeRaport.WriteLine();
            }
            writeRaport.Close();
            MessageBox.Show("Wygenerowano raport!");
        }
    }
}
