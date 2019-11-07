using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Cashier
{
    public partial class Form1 : Form
    {
        //string myConnectionString = "server=192.168.15.8; uid=root; pwd=; database=menu";
        //string myConnectionString = "server=192.168.0.105; uid=roota; pwd=; database=menu";
        
        //string myConnectionString = "server=192.168.15.6; uid=root; pwd=; database=menu";
        //string myConnectionString = "server=localhost; uid=root; pwd=; database=menu";
      //string myConnectionString = "server=192.168.15.8; uid=root; pwd=; database=menu";
        //string myConnectionString = "server=192.168.0.105; uid=roota; pwd=; database=menu";
        
        //string myConnectionString = "server=192.168.15.6; uid=root; pwd=; database=menu";
        //string myConnectionString = "server=localhost; uid=root; pwd=; database=menu";


        string myConnectionString = "server=localhost; uid=root; pwd=; database=menu";
        public Form1()
        {
            InitializeComponent();
            txtuser.Focus();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void btnTables_Click(object sender, EventArgs e)
        {
            
            tables1.BringToFront();
            tables1.clear();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            menu1.BringToFront();
        }

        public string cashier() {
            return txtcashier.Text.ToString();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            history1.BringToFront();
            History hist = new History();
            hist.reload();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            login();
        }

        public void login() {

            MySqlConnection connection = new MySqlConnection(myConnectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            string query = "";
            try
            {
                command.Connection = connection;
                query = "SELECT cashieruser,cashierpas, fname, lname FROM cashier WHERE cashieruser= '" + txtuser.Text + "' AND cashierpas = '" + txtpass.Text + "'";
                command.CommandText = query;
                //Object temp = command.ExecuteScalar();
                //MessageBox.Show(temp.ToString());


                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, myConnectionString))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("Welcome, " + ds.Tables[0].Rows[0].ItemArray[2].ToString() + " " + ds.Tables[0].Rows[0].ItemArray[3].ToString());
                        txtcashier.Text = "";
                        txtpass.Text = "";
                        button1.Enabled = false;
                        panel2.SendToBack();
                        txtcashier.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Credentials. Try again.");
                        txtcashier.Text = "";
                        txtpass.Text = "";
                        //MessageBox.Show(ds.Tables[0].Rows[0].ItemArray[0].ToString() + " " + ds.Tables[0].Rows[0].ItemArray[1].ToString()); 
                    }
                      else
                    {
                        MessageBox.Show("Contant ADMIN");
                        txtcashier.Text = "";
                        txtpass.Text = "";
                        //Mess

                }
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.ToString()); //
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            panel2.BringToFront();
            txtcashier.Text = "";
            button1.Enabled = true;
        }

        private void txtpass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                login();
            }
        }
    }
}

