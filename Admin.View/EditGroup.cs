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
namespace Admin.View
{
    public partial class EditGroup : Form
    {
        private string oldName;
        private string choice;
        public EditGroup()
        {
            InitializeComponent();
        }
        public EditGroup(string switchi, string name)
        {
            choice = switchi;
            oldName = name;
            InitializeComponent();
            this.textBox1.Text = name;
     
        }
        public EditGroup(string switchi)
        {
            choice = switchi;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                switch (choice)
                {
                    case "new":
                        if (this.textBox1.Text != null)
                            Administration.AddGroup(this.textBox1.Text);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;

                    case "exists":
                        if (this.textBox1.Text != null)
                            this.DialogResult = DialogResult.OK;
                        Administration.UpdateGroup(oldName, this.textBox1.Text);
                        this.Close();
                        break;
                }
            }
            catch (System.Security.SecurityException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
