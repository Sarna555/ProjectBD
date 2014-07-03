using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logic;
using Security;
using System.Data.SqlClient;
namespace View
{
    public partial class Form1 : Form
    {
        public IUserCtx uctx;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var res = Class1.showAll();
                foreach (UserResult ele in res)
                {
                    // Perform logic on the item
                }
            }
            catch (System.Security.SecurityException se)
            {
                MessageBox.Show("Permission denied " + se.Message);
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            DialogResult lel = loginForm.ShowDialog(this);
            if (lel == DialogResult.OK)
            {
                string email = loginForm.email;
                string password = loginForm.password;


                if (UserCtx.Login(email, password, out uctx))
                {
                    label1.Text = email;
                }
                else
                {
                    MessageBox.Show("No such user exists");
                }
            }
            loginForm.Dispose();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserCtx.Logout(ref uctx);
            label1.Text = "";
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(uctx != null)
                MessageBox.Show(uctx.uname);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var jakaszmienna = Class1.showAll();
                List<String> names = new List<String>();
                
                foreach (UserResult x in jakaszmienna)
                {
                    string temp = x.name + " " + x.surname;
                    names.Add(temp);
                }

                this.listBox1.DataSource = names;
            }
            catch (System.Security.SecurityException se)
            {
                MessageBox.Show("Permission denied " + se.Message);
            }
        }
    }
}
