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
        public EditUser()
        {
            InitializeComponent();
        }
        public EditUser(string type)
        {
            switchi = type;
            InitializeComponent();
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
                        Administration.UpdateUser(/*oldlogin,*/
                                                  this.textBox3.Text,
                                                  this.textBox1.Text,
                                                  this.textBox2.Text,
                                                  this.textBox4.Text);
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
