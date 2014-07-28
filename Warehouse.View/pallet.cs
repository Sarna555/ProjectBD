using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Warehouse.Logic;

namespace Warehouse.View
{
    public partial class Pallet : Form
    {
        public string palletCode, warehouseCode, orderID, palletID;
        

        public Pallet(string orderIDIn)
        {
            this.orderID = orderIDIn;
            InitializeComponent();
        }
        public Pallet(string orderIDIn, string palletCodeInput, string warehouseCodeInput)
        {
            InitializeComponent();
            this.orderID = orderIDIn;
            this.palletCode = palletCodeInput;
            this.textBox1.Text = palletCodeInput;
            this.warehouseCode = warehouseCodeInput;
            this.textBox2.Text = warehouseCodeInput;
            var productlist = Warehouse.Logic.Warehouse.GetAllProducts(palletCode);
            foreach ( ProductResult product in productlist)
            {
                this.listBox1.Items.Add(product.nazwa);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var addProduct = new Product(this.palletID);
            var productResult = addProduct.ShowDialog();
            if (productResult == DialogResult.OK)
            {
                this.listBox1.Items.Clear();
                var productsOnPallet = Warehouse.Logic.Warehouse.GetAllProducts(palletCode);
                foreach (ProductResult product in productsOnPallet)
                {
                    this.listBox1.Items.Add(product.nazwa);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ( (orderID != null) && (!string.Equals(this.textBox1.Text,"")) && (!string.Equals(this.textBox2.Text,"")) )
            Warehouse.Logic.Warehouse.AddPallet(orderID,this.textBox1.Text, this.textBox2.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
