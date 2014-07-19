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
using System.Data.SqlClient;
namespace Admin.View
{
    public partial class EditUser : Form
    {
        private string switchi;
        public string oldlogin,name,surname,password;
        public EditUser()
        {
            InitializeComponent();
        }
        public EditUser(string type)
        {
            switchi = type;
            InitializeComponent();
        }
        public EditUser(string type, string login, string name, string surname, string password)
        {
            InitializeComponent();
            switchi = type;
            textBox1.Text = name;
            textBox2.Text = surname;
            textBox3.Text = login;
            textBox4.Text = password;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                switch (switchi)
                {
                    case "new":
                        Administration.AddUser(this.textBox3.Text,
                                               this.textBox4.Text,
                                               this.textBox1.Text,
                                               this.textBox2.Text);
                        break;
                    case "exists":
                        Administration.UpdateUser(oldlogin,this.textBox3.Text,this.textBox1.Text,this.textBox2.Text,this.textBox4.Text);
                        break;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
