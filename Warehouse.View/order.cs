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
        private string switchi;
        public Order()
        {
            InitializeComponent();
        }
        public Order(string switchiIn,string orderIDIn, string senderIn, string recieverIn, DateTime dateSentIn, DateTime dateRecievedIn, string state)
        {
            switchi = switchiIn;
            switch (switchi)
            {
                case "new":
                    orderID = orderIDIn;
                    sender = senderIn;
                    reciever = recieverIn;
                    dateSent = dateSentIn;
                    dateRecieved = dateRecievedIn;
                    InitializeComponent();
                    this.comboBox1.SelectedIndex = comboBox1.Items.IndexOf(state);
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
                    this.comboBox1.SelectedIndex = comboBox1.Items.IndexOf(state);
                    var palletslist = Warehouse.Logic.Warehouse.GetAllPallets(orderID);
                    
                    foreach (PalletResult pallet in palletslist)
                    {
                        listBox1.Items.Add(pallet.kod_palety);
                    };

                    if (listBox1.Items.Count != 0)
                    {
                        listBox1.SelectedIndex = 0;
                        this.listBox2.Items.Clear();
                        var palletProducts = Warehouse.Logic.Warehouse.GetAllProducts(this.listBox1.SelectedItem.ToString());
                        foreach (ProductResult product in palletProducts)
                        {
                            this.listBox2.Items.Add(product.nazwa);
                        }
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

            switch (switchi)
            {
                case "new":
                    Warehouse.Logic.Warehouse.AddOrder(this.textBox1.Text, this.textBox2.Text, this.dateTimePicker1.Value, this.dateTimePicker2.Value,this.comboBox1.SelectedItem.ToString());
                    break;
                case "exists":
                    Warehouse.Logic.Warehouse.UpdateOrder(orderID, this.textBox1.Text, this.textBox2.Text, this.dateTimePicker1.Value, this.dateTimePicker2.Value,this.comboBox1.SelectedItem.ToString());
                    break;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.Equals(this.textBox1.Text, "") && !string.Equals(this.textBox2.Text, "") && switchi == "new")
            {
                var newPallet = new Pallet("new", orderID);
                var palletResult = newPallet.ShowDialog();
                Warehouse.Logic.Warehouse.AddOrder(this.textBox1.Text, this.textBox2.Text, this.dateTimePicker1.Value, this.dateTimePicker2.Value, this.comboBox1.SelectedItem.ToString());
                if (palletResult == DialogResult.OK)
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
                newPallet.Dispose();
            }
            else
            {
                MessageBox.Show("Najpierw uzupełnij dane zamówienia!");
            }
            
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
            if (this.listBox1.Items.Count >= 1)
            {
                currentPallet = Warehouse.Logic.Warehouse.GetPallet(listBox1.SelectedItem.ToString());

                var editPallet = new Pallet("exists", orderID, currentPallet.kod_palety, currentPallet.kod_miejsca_w_mag, currentPallet.Id.ToString());
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
            else
            {
                MessageBox.Show("Najpierw utwórz i zaznacz paletę!");
            }
      
        }

    }
}
