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

namespace View
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var res = Class1.showAll();
            foreach(UserResult ele in res)
            {
                textBox1.AppendText(ele.name + " " + ele.surname + "\n");
                // Perform logic on the item
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
            loginForm.ShowDialog();
            String email = loginForm.email;
            String password = loginForm.password;
            loginForm.Dispose();

            var user = Class1.FindUser(email, password);

            if (user.Count != 0)
            {
                MessageBox.Show(user[0].name + " " + user[0].surname);
            }
            else
            {
                MessageBox.Show("Couldnt find user");
            }
        }
    }
}
