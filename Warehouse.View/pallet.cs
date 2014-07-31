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
        private string switchi;

        public Pallet(string switchiIin,string orderIDIn)
        {
            switchi = switchiIin;
            this.orderID = orderIDIn;
            InitializeComponent();
        }
        public Pallet(string switchiIn,string orderIDIn, string palletCodeInput, string warehouseCodeInput, string palletIDin)
        {
            InitializeComponent();
            switchi = switchiIn;
            palletID = palletIDin;
            this.orderID = orderIDIn;
            this.palletCode = palletCodeInput;
            this.textBox1.Text = palletCodeInput;
            this.warehouseCode = warehouseCodeInput;
            this.textBox2.Text = warehouseCodeInput;
            if (switchi == "exists")
            {
                var productlist = Warehouse.Logic.Warehouse.GetAllProducts(palletCode);
                foreach (ProductResult product in productlist)
                {
                    this.listBox1.Items.Add(product.nazwa);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            var addProduct = new Product("new", palletCode, null, null, DateTime.Today);
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
            switch (switchi)
            {
                case "new":
                    if ( (orderID != null) && (!string.Equals(this.textBox1.Text,"")) && (!string.Equals(this.textBox2.Text,"")) )
                    Warehouse.Logic.Warehouse.AddPallet(orderID,this.textBox1.Text, this.textBox2.Text);
                    this.DialogResult = DialogResult.OK;
                    break;
                case "exists":
                    if ((orderID != null) && (!string.Equals(this.textBox1.Text, "")) && (!string.Equals(this.textBox2.Text, "")))
                        Warehouse.Logic.Warehouse.UpdatePallet(palletID, this.textBox1.Text, orderID, this.textBox2.Text);
                    this.DialogResult = DialogResult.OK;
                    break;
            }
            this.Close();
        }
        

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var productCategory = Warehouse.Logic.Warehouse.GetProduct(this.listBox1.SelectedItem.ToString());
            var addProduct = new Product("exists", palletCode, this.listBox1.SelectedItem.ToString(),null, DateTime.Today);
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
    }
}
