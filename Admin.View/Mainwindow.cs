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
                    new EditGroup().Show();
                    break;
                case "user":
                    new EditUser().Show();
                    break;
                default:
                    new Error().Show();
                    break;
            }
        }

        private void zalogujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Login().Show();
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
        }

        private void dodajGrupęToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new EditGroup().Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string login = this.listBox1.SelectedItem.ToString();
            checkedListBox1.Items.Clear();
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
                    var permissionListUser = Administration.GetUserOperations(login);
                    foreach (String permission in permissionListUser)
                    {
                        checkedListBox1.Items.Add(permission, true);
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
            choice = "group";
            this.listBox1.DataSource = Administration.GetAllGroups();
        }
    }
}
