using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Cashier
{
    public partial class Menu : UserControl
    {
            //string myConnectionString = "server=192.168.15.8; uid=root; pwd=; database=menu";
        //string myConnectionString = "server=192.168.0.105; uid=roota; pwd=; database=menu";
        
        //string myConnectionString = "server=192.168.15.6; uid=root; pwd=; database=menu";
        //string myConnectionString = "server=localhost; uid=root; pwd=; database=menu";


        //string myConnectionString = "server=192.168.15.6; uid=root; pwd=; database=menu";
        //string myConnectionString = "server=localhost; uid=root; pwd=; database=menu";

        //string myConnectionString = "server=192.168.15.8; uid=root; pwd=; database=menu";
        //string myConnectionString = "server=192.168.0.105; uid=roota; pwd=; database=menu";
        //string myConnectionString = "server=localhost; uid=root; pwd=a3gISrZQMA; database=FGCO";
        string myConnectionString = "server=localhost; uid=root; pwd=; database=menu";


        public Menu()
        {
           // MySqlConnection connection = new MySqlConnection(myConnectionString);
            InitializeComponent();

        }


        private void Menu_Load(object sender, EventArgs e)
        {
            
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            //          try
            //          {
            //              connection.Open();
            //          }
            //          catch (Exception ee) { 
            //MessageBox.Show("Cant Connect to Database: menu");
            ////Application.Exit(); 
            //}
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            string query  = "";
            zee();
            connection.Close();
            for (int x = 1; x <= 300; x++)
            {
                combotable.Items.Add("Table "+x);
            }

            try
            {
                combotable.SelectedIndex = 0;
                connection.Open();
                command = connection.CreateCommand();
                command.Connection = connection;
                query = "SELECT * FROM menu WHERE itemstat = 'Available'";
                command.CommandText = query;
                //Object temp = command.ExecuteScalar();
                //MessageBox.Show(temp.ToString());


                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, myConnectionString))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    datagridMenu.DataBindings.Clear();
                    datagridMenu.Rows.Clear();
                    for (int x = 0; x < ds.Tables[0].Rows.Count; x++) {
                        datagridMenu.Rows.Add(ds.Tables[0].Rows[x].ItemArray[0], ds.Tables[0].Rows[x].ItemArray[1], float.Parse(ds.Tables[0].Rows[x].ItemArray[2].ToString()), ds.Tables[0].Rows[x].ItemArray[4], "", ds.Tables[0].Rows[x].ItemArray[3]);
                    }
                }

            }
            catch (Exception ee)
            { //MessageBox.Show(ee.ToString()); 
            }

        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {

            if (this.z.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.z.Text = text;
            }
        }

        public void zee()
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            
            MySqlCommand command = connection.CreateCommand();
            connection.Open();
            command.Connection = connection;
            string query = "SELECT count FROM ordercount";
            command.CommandText = query;
            Object temp = command.ExecuteScalar();
            SetText(temp.ToString());
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                txtitemname.Visible = false;
                txtitemprice.Visible = false;
                textBox1.Text = "";
                panel1.SendToBack();
                datagridMenu.Enabled = true;
                txtQty.Value = 1;
                txtNote.Text = "";
                string s = (sender as Button).Text;
                
                if (s == "All") { label1.Text = "All Items"; }
                else { label1.Text = s; }

                MySqlConnection connection = new MySqlConnection(myConnectionString);

                try
                {
                    string query = "";
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.Connection = connection;
                    if (s == "All") { query = "SELECT * FROM menu"; }
                    else if (s == "Pica") { query = "SELECT * FROM menu WHERE itemstat = 'Available' AND itemcategory =  'Pica1' OR itemcategory = 'Pica2'"; }
                    else { query = "SELECT * FROM menu WHERE itemstat = 'Available' AND itemcategory LIKE  '" + s + "'"; }

                    command.CommandText = query;
                    //Object temp = command.ExecuteScalar();
                    //MessageBox.Show(temp.ToString());


                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, myConnectionString))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        datagridMenu.DataBindings.Clear();
                        datagridMenu.Rows.Clear();
                        for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                        {
                            datagridMenu.Rows.Add(ds.Tables[0].Rows[x].ItemArray[0], ds.Tables[0].Rows[x].ItemArray[1], float.Parse(ds.Tables[0].Rows[x].ItemArray[2].ToString()), ds.Tables[0].Rows[x].ItemArray[4], "", ds.Tables[0].Rows[x].ItemArray[3]);
                        }
                    }
                    connection.Close();

                }
                catch (Exception ee)
                {
                //    MessageBox.Show(ee.ToString());
                }

            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.ToString());
            }

        }

        private void datagridMenu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) { }
            else if (e.ColumnIndex == 4 && datagridMenu.CurrentRow.Cells[3].Value.ToString() == "Available") {
                string val = datagridMenu.CurrentRow.Cells[5].Value.ToString();
                if (val == "Beers") { checkadd.Enabled = false; checkadd.Checked = false; }
                else { checkadd.Enabled = true; }
                txtQty.Value = 1;
                txtQty.ForeColor = Color.Black;
                //datagridMenu.Rows[e.RowIndex].Cells[4].Value = true;
                //datagridOrder.Rows.Add(datagridMenu.Rows[e.RowIndex].Cells[0].Value.ToString(), datagridMenu.Rows[e.RowIndex].Cells[1].Value.ToString(), "1",datagridMenu.Rows[e.RowIndex].Cells[2].Value.ToString(),"");
                txtname.Text = datagridMenu.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtprice.Text = datagridMenu.Rows[e.RowIndex].Cells[2].Value.ToString();
                datagridMenu.Enabled = false;
                panel1.BringToFront();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtQty.Text.ToString() == "" || txtQty.Text.ToString() == "0") { lblqty.ForeColor = Color.Red; goto endd; }

                else if (txtitemname.Visible == true) { if (txtitemname.Text == "" || txtitemprice.Text == "") { MessageBox.Show("Empty Fields not allowed."); goto endd; }}

            
                
                
                string s = "";
                string name = "";
                string price = "";
                if (txtitemname.Visible == false)
                {
                    s = datagridMenu.CurrentRow.Cells[5].Value.ToString();
                    name = datagridMenu.CurrentRow.Cells[1].Value.ToString();
                    price = datagridMenu.CurrentRow.Cells[2].Value.ToString();


                }

                else if (txtitemname.Visible == true)
                {
                    name = txtitemname.Text.ToString();
                    price = txtitemprice.Text.ToString();
                    s = "Extra";

                }
                datagridMenu.Enabled = true;


                if (checkadd.Checked == true) { name += "*"; }

                panel1.SendToBack();
                datagridOrder.Rows.Add(datagridMenu.CurrentRow.Cells[0].Value.ToString(), name, txtQty.Text.ToString(), (float.Parse(price) * float.Parse(txtQty.Text.ToString())), txtNote.Text.ToString(), "", s);
                txtQty.Value = 1;
                txtNote.Text = "";
                txtname.Text = "";
                txtprice.Text = "";
                txtitemname.Text = "";
                txtitemname.Visible = false;
                txtitemprice.Text = "";
                txtitemprice.Visible = false;
                checkadd.Checked = false;
                checkadd.Enabled = true;
                
            endd:; 
            }
            catch(Exception ee) {
                //MessageBox.Show(ee.ToString()); 
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtitemname.Text = "";
            txtitemname.Visible = false;
            txtitemprice.Text = "";
            txtitemprice.Visible = false;
            datagridMenu.Enabled = true;
            txtQty.Value = 1;
            txtNote.Text = "";
            panel1.SendToBack();
            checkadd.Checked = false;

        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (datagridOrder.Rows.Count > 0)
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(myConnectionString);
                    string query = "";
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.Connection = connection;

                    //connection.Open();
                    try
                    {
                        query = "UPDATE ordercount SET count = '" + (int.Parse(z.Text.ToString()) + 1).ToString() + "',tableid = '" + combotable.Text.ToString() + "' WHERE id = '1'";
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ee)
                    {
                        //MessageBox.Show(ee.ToString()); 
                    }



                    //connection.Close();
                    //connection.Open();

                    // 

                    connection.Close();
                    connection.Open();
                    for (int x = 0; x < datagridOrder.Rows.Count; x++)
                    {
                        query = "INSERT INTO orders( itemname, itemquantity, itemnote, itemstat, tableid, itemcategory, orderid, itemprice, datetime) " +
                            "VALUES ('" + datagridOrder.Rows[x].Cells[1].Value.ToString() + "','" + datagridOrder.Rows[x].Cells[2].Value.ToString() + "','" + datagridOrder.Rows[x].Cells[4].Value.ToString() + "','prep','" + combotable.Text.ToString() + "','" + datagridOrder.Rows[x].Cells[6].Value.ToString() + "','" + z.Text.ToString() + "','" + datagridOrder.Rows[x].Cells[3].Value.ToString() + "','" + DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm:ss tt") + "')";
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                    connection.Open();

                    //for (int x = 0; x < datagridOrder.Rows.Count; x++)
                    //{
                    //    query = "INSERT INTO tables (itemid, itemname, itemquantity, itemprice, itemnote, itemstat, tableid, datetime, orderid) " +
                    //        "VALUES('" + datagridOrder.Rows[x].Cells[0].Value.ToString() + "','" + datagridOrder.Rows[x].Cells[1].Value.ToString() + "','" + datagridOrder.Rows[x].Cells[2].Value.ToString() + "','" + datagridOrder.Rows[x].Cells[3].Value.ToString() + "','"+ datagridOrder.Rows[x].Cells[4].Value.ToString() + "','prep','" + combotable.Text.ToString()+ "','"+ DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss tt") + "','" + z.Text.ToString() + "')";
                    //    command.CommandText = query;
                    //    command.ExecuteNonQuery();
                    //}
                    //MessageBox.Show("ORDER SUBMITTED");
                    connection.Close();
                    datagridOrder.Rows.Clear();


                    zee();

                }
                catch (Exception ee) { }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            datagridOrder.Rows.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                panel1.SendToBack();
                for (int x = 0; x < datagridMenu.Rows.Count; x++)
                {
                    if (datagridMenu.Rows[x].Cells[1].Value.ToString().ToLower().Contains(textBox1.Text) == false) {
                        datagridMenu.Rows[x].Visible = false;
                    }
                    else if (datagridMenu.Rows[x].Cells[1].Value.ToString().ToLower().Contains(textBox1.Text) == true)
                    {
                        datagridMenu.Rows[x].Visible = true;
                    }
                }
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.ToString());
            }
        }

        private void datagridOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) { }
            else if(e.ColumnIndex == 5) {
                datagridOrder.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void datagridOrder_MouseClick(object sender, MouseEventArgs e)
        {
            if (datagridOrder.Rows.Count > 0)
            {
                if (e.Button == MouseButtons.Left) { }
                else if (e.Button == MouseButtons.Right)
                {
                    ContextMenuStrip priority = new ContextMenuStrip();
                    int position = datagridOrder.HitTest(e.X, e.Y).RowIndex;

                    if (position >= 0)
                    {
                        priority.Items.Add("★").Name = "0";
                        priority.Items.Add("☆").Name = "1";
                        priority.Items.Add("Cancel").Name = "2";
                    }

                    priority.Show(datagridOrder, new Point(e.X, e.Y));
                    priority.ItemClicked += new ToolStripItemClickedEventHandler(priority_ItemClicked);
                }
            }
            else { }
        }

        private void priority_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string val = datagridOrder.CurrentRow.Cells[1].Value.ToString();
            //string pattern = @"\★";
            //Match m = Regex.Match(val, pattern);
            //switch (e.ClickedItem.Name.ToString())
            //{
            //    case "0":
            //        if (m.Success) { }
            //        else
            //        {
            //            val += "\\"+"★";
            //            datagridOrder.CurrentRow.Cells[1].Value = val;
            //        }
            //        break;

            //    case "1":
            //        if (m.Success) {
            //            val = val.Remove(val.Length - 2);
            //            datagridOrder.CurrentRow.Cells[1].Value = val;
            //        }
            //        break;

            //    case "2":
            //        SendKeys.Send("{ESC}");
            //        break;
            //}

            switch (e.ClickedItem.Name.ToString())
            {
                case "0":
                    if (val.Contains("*")) { }
                    else
                    {
                        val += "*";
                        datagridOrder.CurrentRow.Cells[1].Value = val;
                    }
                    break;

                case "1":
                    if (val.Contains("*"))
                    {
                        val = val.Replace("*","" );
                        datagridOrder.CurrentRow.Cells[1].Value = val;
                    }
                    break;

                case "2":
                    SendKeys.Send("{ESC}");
                    break;
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
       (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void Btnmanual_Click(object sender, EventArgs e)
        {
            panel1.BringToFront();
            txtitemname.Visible = true;
            txtitemprice.Visible = true;
        }
    }
}
