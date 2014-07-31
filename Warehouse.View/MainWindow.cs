using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Security;
using Warehouse.Logic;
namespace Warehouse.View
{
    public partial class Form1 : Form
    {
        public IUserCtx uctx;
        private OrderResult currentOrder; 
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItem != null)
                {
                    var orderModify = new Order("exists", this.listBox1.SelectedItem.ToString(),
                                                         this.currentOrder.nadawca,
                                                         this.currentOrder.odbiorca,
                                                         this.currentOrder.data_nadania,
                                                         this.currentOrder.data_odbioru,
                                                         this.currentOrder.nazwa_stanu);
                    orderModify.orderID = this.listBox1.SelectedItem.ToString();
                    orderModify.ShowDialog();

                    this.listBox1.Items.Clear();
                    this.listBox2.Items.Clear();
                    var ordersList = Warehouse.Logic.Warehouse.GetAllOrders();
                    foreach (OrderResult orders in ordersList)
                    {
                        this.listBox1.Items.Add(orders.Id);
                        this.listBox2.Items.Add(orders.nadawca + " stan: " + orders.nazwa_stanu);
                    }
                }
                else
                {
                    MessageBox.Show("Zaznacz element!");
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            DialogResult loginresult = loginForm.ShowDialog(this);
            if (loginresult == DialogResult.OK)
            {
                string email = loginForm.email;
                string password = loginForm.password;


                if (UserCtx.Login(email, password, out uctx))
                {
                    label1.Text = email;
                }
                else
                {
                    MessageBox.Show("No such user exists");
                }
            }
            loginForm.Dispose();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserCtx.Logout(ref uctx);
            label1.Text = "Nie jesteś zalogowany";
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(uctx != null)
                MessageBox.Show(uctx.uname);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.currentOrder = Warehouse.Logic.Warehouse.GetOrder(this.listBox1.SelectedItem.ToString());
        }


        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                Warehouse.Logic.Warehouse.DeleteOrder(this.listBox1.SelectedItem.ToString());
                this.listBox1.Items.Clear();
                var ordersList = Warehouse.Logic.Warehouse.GetAllOrders();
                foreach (OrderResult orderFromList in ordersList)
                {
                    this.listBox1.Items.Add(orderFromList.Id);
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

        private void dodajZamówienieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                var order = new Order("new", null, null, null, DateTime.MinValue, DateTime.MinValue, "oczekujaca");
                var resultOrder = order.ShowDialog();
                if (resultOrder == DialogResult.OK)
                {
                    this.listBox1.Items.Clear();
                    this.listBox2.Items.Clear();
                    order.Dispose();
                    var ordersList = Warehouse.Logic.Warehouse.GetAllOrders();
                    foreach (OrderResult orderFromList in ordersList)
                    {
                        this.listBox1.Items.Add(orderFromList.Id);
                        this.listBox2.Items.Add(orderFromList.nadawca + " stan: " + orderFromList.nazwa_stanu);
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

        private void wyświelListęToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            this.listBox2.Items.Clear();
            try
            {
                var ordersList = Warehouse.Logic.Warehouse.GetAllOrders();
                foreach (OrderResult orders in ordersList)
                {
                    this.listBox1.Items.Add(orders.Id);
                    this.listBox2.Items.Add(orders.nadawca + " stan: " + orders.nazwa_stanu);
                }
            }
            catch (SqlException se)
            {
                MessageBox.Show("Błąd bazy danych" + se.Message);
            }
            catch (System.Security.SecurityException se)
            {
                MessageBox.Show("Brak uprawnień" + se.Message);
            }
        }

        private void dodajKategorięProduktówToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var addCategory = new Category();
            addCategory.ShowDialog();
        }
    }
}
