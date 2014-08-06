using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Security;

namespace Warehouse
{
    public partial class Product : Form
    {
        public string category, palletCode, productID;
        private string switchi;
        public Product(string switchiIn, string palletCodeIn, string name, string category, DateTime bestBefore)
        {
            try
            {
                switchi = switchiIn;
                palletCode = palletCodeIn;
                InitializeComponent();
                var categorylist = Warehouse.Logic.Warehouse.GetAllCategories();
                comboBox1.DataSource = categorylist;
                if (string.Equals(switchi, "exists"))
                {
                    this.textBox1.Text = name;
                    this.dateTimePicker1.Value = bestBefore;
                    this.comboBox1.SelectedIndex = this.comboBox1.Items.IndexOf(category);
                }
            }
            catch (System.Security.SecurityException se)
            {
                MessageBox.Show("Permission denied " + se.Message);
            }
            catch (Exception se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                switch (switchi)
                {
                    case "new":
                        Warehouse.Logic.Warehouse.AddProduct(palletCode, this.textBox1.Text, this.dateTimePicker1.Value, comboBox1.SelectedItem.ToString());
                        break;
                    case "exists":
                        Warehouse.Logic.Warehouse.UpdateProduct(productID, this.textBox1.Text, this.dateTimePicker1.Value, comboBox1.SelectedItem.ToString());
                        break;
                }
            }
            catch (System.Security.SecurityException se)
            {
                MessageBox.Show("Permission denied " + se.Message);
            }
            catch (Exception se)
            {
                MessageBox.Show(se.Message);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
