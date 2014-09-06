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
using Security;
namespace Admin.View
{
    public partial class EditUser : Form
    {
        private string switchi;
        private string oldlogin,newlogin,name,surname,password;
        public EditUser()
        {
            InitializeComponent();
        }
        public EditUser(string type)
        {
            switchi = type;
            InitializeComponent();
        }
        public EditUser(string type, UserResult input)
        {
            InitializeComponent();
            this.switchi = type;
            this.oldlogin = input.login;
            this.name = input.name;
            this.surname = input.surname;
            this.newlogin = input.login;
            this.password = null;
            textBox1.Text = input.name;
            textBox2.Text = input.surname;
            textBox3.Text = input.login;
            
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
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    case "exists":

                        if (!string.Equals(this.textBox4.Text, ""))
                        {
                            password = this.textBox4.Text;
                        }
                        if (!string.Equals(this.textBox1.Text, ""))
                        {
                            name = this.textBox1.Text;
                        }
                        if (!string.Equals(this.textBox2.Text, ""))
                        {
                            surname = this.textBox2.Text;
                        }
                        if (!string.Equals(this.textBox3.Text, ""))
                        {
                            newlogin = this.textBox3.Text;
                        }
                        Administration.UpdateUser(oldlogin, newlogin, name, surname, password);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                }
            }
            catch (System.Security.SecurityException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
