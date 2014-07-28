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
    public partial class Order : Form
    {
        public string sender, reciever, orderID;
        public PalletResult currentPallet;
        public DateTime dateSent, dateRecieved;
        public Order()
        {
            InitializeComponent();
        }
        public Order(string switchi,string orderIDIn, string senderIn, string recieverIn, DateTime dateSentIn, DateTime dateRecievedIn)
        {
            switch (switchi)
            {
                case "new":
                    orderID = orderIDIn;
                    sender = senderIn;
                    reciever = recieverIn;
                    dateSent = dateSentIn;
                    dateRecieved = dateRecievedIn;

                    InitializeComponent();
                    break;
                case "exists":
                    InitializeComponent();
                    orderID = orderIDIn;
                    this.textBox1.Text = senderIn;
                    sender = senderIn;
                    this.textBox2.Text = recieverIn;
                    reciever = recieverIn;
                    this.dateTimePicker1.Value = dateSentIn;
                    dateSent = dateSentIn;
                    this.dateTimePicker2.Value = dateRecievedIn;
                    dateRecieved = dateRecievedIn;
                    var palletslist = Warehouse.Logic.Warehouse.GetAllPallets(orderID);
                    
                    foreach (PalletResult pallet in palletslist)
                    {
                        listBox1.Items.Add(pallet.kod_palety);
                    };

                    listBox1.SelectedIndex = 0;
                    var palletProducts = Warehouse.Logic.Warehouse.GetAllProducts(this.listBox1.SelectedItem.ToString());
                    foreach (ProductResult product in palletProducts)
                    {
                        this.listBox2.Items.Add(product.nazwa);
                    }
                    break;
            }
 
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var palletProducts = Warehouse.Logic.Warehouse.GetAllProducts(this.listBox1.SelectedItem.ToString());
            this.listBox2.Items.Clear();
            foreach (ProductResult product in palletProducts)
            {
                this.listBox2.Items.Add(product.nazwa);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.sender = this.textBox1.Text;
            this.reciever = this.textBox2.Text;
            this.dateSent = this.dateTimePicker1.Value;
            this.dateRecieved = this.dateTimePicker2.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var newPallet = new Pallet(orderID);
            var palletResult = newPallet.ShowDialog();

            if (palletResult == DialogResult.OK) ;
            {
                var palletslist = Warehouse.Logic.Warehouse.GetAllPallets(orderID);

                foreach (PalletResult pallet in palletslist)
                {
                    listBox1.Items.Add(pallet.kod_palety);
                };

                listBox1.SelectedIndex = 0;
                var palletProducts = Warehouse.Logic.Warehouse.GetAllProducts(this.listBox1.SelectedItem.ToString());
                foreach (ProductResult product in palletProducts)
                {
                    this.listBox2.Items.Add(product.nazwa);
                }
                
            }
            newPallet.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Warehouse.Logic.Warehouse.DeletePallet(this.listBox1.SelectedItem.ToString());
            this.listBox1.Items.Clear();
            this.listBox2.Items.Clear();
            var palletslist = Warehouse.Logic.Warehouse.GetAllPallets(orderID);

            foreach (PalletResult pallet in palletslist)
            {
                listBox1.Items.Add(pallet.kod_palety);
            };

            if (listBox1.Items.Count >= 1)
            {
                listBox1.SelectedIndex = 0;
                var palletProducts = Warehouse.Logic.Warehouse.GetAllProducts(this.listBox1.SelectedItem.ToString());
                foreach (ProductResult product in palletProducts)
                {
                    this.listBox2.Items.Add(product.nazwa);
                }
            }
                
        }

        private void button5_Click(object sender, EventArgs e)
        {
            currentPallet = Warehouse.Logic.Warehouse.GetPallet(listBox1.SelectedItem.ToString());
            var editPallet = new Pallet(orderID,currentPallet.kod_palety, currentPallet.kod_miejsca_w_mag);
            var modifyResult = editPallet.ShowDialog();
            if (modifyResult == DialogResult.OK)
            {
                this.listBox1.Items.Clear();
                this.listBox2.Items.Clear();
                var palletslist = Warehouse.Logic.Warehouse.GetAllPallets(orderID);

                foreach (PalletResult pallet in palletslist)
                {
                    listBox1.Items.Add(pallet.kod_palety);
                };

                listBox1.SelectedIndex = 0;
                var palletProducts = Warehouse.Logic.Warehouse.GetAllProducts(this.listBox1.SelectedItem.ToString());
                foreach (ProductResult product in palletProducts)
                {
                    this.listBox2.Items.Add(product.nazwa);
                }
                
            }

      
        }
    }
}
