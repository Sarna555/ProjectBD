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
            IEnumerator<String> enumerator = res.GetEnumerator();
            while (enumerator.MoveNext())
            {
                object item = enumerator.Current;
                textBox1.AppendText(item.ToString());
                // Perform logic on the item
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
