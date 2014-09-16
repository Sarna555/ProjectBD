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
using Security;
namespace Warehouse.View
{
    public partial class Pallet : Form
    {
        public string palletCode, warehouseCode, orderID, palletID;
        private string switchi;
        List<ProductResult> productsOnPallet;
        private ProductResult currentProduct;

        public Pallet(string switchiIin,string orderIDIn)
        {
            switchi = switchiIin;
            this.orderID = orderIDIn;
            InitializeComponent();
        }
        public Pallet(string switchiIn,string orderIDIn, string palletCodeInput, string warehouseCodeInput, string palletIDin)
        {
            try
            {
                InitializeComponent();
                switchi = switchiIn;
                palletID = palletIDin;
                this.orderID = orderIDIn;
                this.palletCode = palletCodeInput;
                this.textBox1.Text = palletCodeInput;
                this.warehouseCode = warehouseCodeInput;
                this.textBox2.Text = warehouseCodeInput;
                productsOnPallet = Warehouse.Logic.Warehouse.GetAllProducts(palletCode);   
                if (switchi == "exists")
                {
                    foreach (ProductResult product in productsOnPallet)
                    {
                        this.listBox1.Items.Add(product.nazwa);
                    }
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

        public void InitialHideComponents()
        {
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button2.Enabled = false;
            listBox1.Enabled = false;
        }
        public void ShowComponenets(IUserCtx user)
        {
            var roles = user.GetAllRoles();
            foreach (var role in roles)
            {
                switch (role)
                {
                    case "AddProduct":
                        button3.Enabled = true;
                        break;
                    case "UpdateProduct":
                        button4.Enabled = true;
                        break;
                    case "DeleteProduct":
                        button5.Enabled = true;
                        break;
                    case "GetAllProducts":
                        listBox1.Enabled = true;
                        break;
                    case "UpdatePallet":
                        button2.Enabled = true;
                        break;
                }
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var addProduct = new Product("new", palletCode, null, null, DateTime.Today);
                var productResult = addProduct.ShowDialog();
    
                if (productResult == DialogResult.OK)
                {
                    this.listBox1.Items.Clear();
                     
                    foreach (ProductResult product in productsOnPallet)
                    {
                        this.listBox1.Items.Add(product.nazwa);
                    }
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
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
            }
            catch (System.Security.SecurityException se)
            {
                MessageBox.Show("Permission denied " + se.Message);
            }
            catch (Exception se)
            {
                MessageBox.Show(se.Message);
            }
            this.Close();
        }
        

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Warehouse.Logic.Warehouse.DeleteProduct(currentProduct.Id.ToString());
                productsOnPallet = Warehouse.Logic.Warehouse.GetAllProducts(palletCode);
                listBox1.Items.Clear();
                foreach (var product in productsOnPallet)
                {
                    listBox1.Items.Add(product.nazwa);
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
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
            catch (System.Security.SecurityException se)
            {
                MessageBox.Show("Permission denied " + se.Message);
            }
            catch (Exception se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ProductResult product in productsOnPallet)
            {
                if (product.nazwa == this.listBox1.SelectedItem.ToString())
                this.currentProduct = product;
            }
        }
    }
}
