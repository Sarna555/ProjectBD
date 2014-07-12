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
    public partial class AddPermission : Form
    {
        public string returnValue1;
        public AddPermission()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            returnValue1 = this.textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
