using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admin.View
{
    public partial class EditGroup : Form
    {
        public EditGroup()
        {
            InitializeComponent();
        }
        public EditGroup(string name)
        {
            InitializeComponent();
            this.textBox1.Text = name;
            this.Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
