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
namespace Admin.View
{
    public partial class Mainwindow : Form
    {
        string choice;

        public Mainwindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (choice)
            {
                case "group":
                    var editGroup = new EditGroup(this.listBox1.SelectedItem.ToString());
                    editGroup.Show();
                    break;
                case "user":
                    new EditUser().Show();
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
                if (!loggingResult)
                {
                    MessageBox.Show("Błąd logowania: nieprawidłowy login lub hasło. \nSpróbuj ponownie");
                }
            }
        }

        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().Show();
        }


        private void dodajUżytkownikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new EditUser().Show();
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
            new EditGroup().Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var allPermissions = Administration.GetAllOperations();
            string login = this.listBox1.SelectedItem.ToString();
            checkedListBox1.Items.Clear();
            checkedListBox2.Items.Clear();
            switch (choice)
            {
                case "group":
                    var permissionListGroups = Administration.GetGroupOperations(login);
                    
                    foreach (String permission in permissionListGroups)
                    {
                        checkedListBox1.Items.Add(permission, true);
                    }

                    break;
                case "user":
                    
                    var userPermissions = Administration.GetAllUserOperations(login);
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
                        checkedListBox1.Sorted = true;
                        
                    }
                    foreach (String group in allGroups)
                    {
                        checkedListBox2.Items.Add(group, false);
                    }
                    foreach (String group in userGroups)
                    {
                        checkedListBox2.Items.Remove(group);
                        checkedListBox2.Items.Add(group, true);
                        checkedListBox2.Sorted = true;
                    }
                    break;
                default:
                    MessageBox.Show("Wybierz użytkownika/grupę");
                    break;
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
            new AddPermission().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //Update
            }
            catch (System.Security.SecurityException se)
            {
                MessageBox.Show("Permission denied " + se.Message);
            }
        }

        private void wylogujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserCtx.Logout(ref _userCtx);
        }
    }
}
