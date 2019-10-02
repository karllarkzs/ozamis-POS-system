using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;
using System.Drawing.Printing;

namespace Cashier
{



    public partial class History : UserControl
    {
        //string myConnectionString = "server=192.168.15.8; uid=root; pwd=; database=menu";
        //string myConnectionString = "server=192.168.0.105; uid=roota; pwd=; database=menu";
        string myConnectionString = "server=localhost; uid=root; pwd=; database=menu";

        public History()
        {
            InitializeComponent();
            
        }


        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            txtsearch.Text = "";
            txtdate.Text = string.Format(monthCalendar1.SelectionRange.Start.ToString("dddd, dd MMMM yyyy"));
            txttotal.Text = "₱0";
            //txtdate.Text = "All Receipts";
            //string s = (sender as Button).Text;
            try
            {
                //datagridOrders.Enabled = true;
                //panel5.SendToBack();
                MySqlConnection connection = new MySqlConnection(myConnectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                string query = "SELECT * FROM receipts";
                command.CommandText = query;
                command.ExecuteNonQuery();
                connection.Close();
                //txtsub.Text = "₱0";
                //lbldate.Text = "";
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, myConnectionString))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    //datagridOrders.DataBindings.Clear();
                    datagridOrders.Rows.Clear();
                    //label2.Text = s;
                    //lbldate.Text = ds.Tables[0].Rows[0].ItemArray[9].ToString();
                    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                    {
                        if (ds.Tables[0].Rows[x].ItemArray[7].ToString().Contains(txtdate.Text))
                        {
                            datagridOrders.Rows.Add(float.Parse(ds.Tables[0].Rows[x].ItemArray[0].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[1].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[2].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[3].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[4].ToString()), ds.Tables[0].Rows[x].ItemArray[5], ds.Tables[0].Rows[x].ItemArray[7], ds.Tables[0].Rows[x].ItemArray[8], ds.Tables[0].Rows[x].ItemArray[9].ToString(), ds.Tables[0].Rows[x].ItemArray[10].ToString());
                            txttotal.Text = (float.Parse(txttotal.Text.ToString(), System.Globalization.NumberStyles.Currency) + float.Parse(ds.Tables[0].Rows[x].ItemArray[2].ToString())).ToString("C2");
                        }
                    }
                }
            }
            catch (Exception ee) { MessageBox.Show(ee.ToString()); }
        }

        public void reload()
        {
            datagridOrders.Rows.Clear();
            string s = txtdate.Text;
            try
            {
                //datagridOrders.Enabled = true;
                //panel5.SendToBack();
                MySqlConnection connection = new MySqlConnection(myConnectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                string query = "SELECT * FROM receipts";
                command.CommandText = query;
                command.ExecuteNonQuery();
                connection.Close();
                //txtsub.Text = "₱0";
                //lbldate.Text = "";
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, myConnectionString))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    //datagridOrders.DataBindings.Clear();
                    //datagridOrders.Rows.Clear();
                    //label2.Text = s;
                    //lbldate.Text = ds.Tables[0].Rows[0].ItemArray[9].ToString();
                    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                    {
                        datagridOrders.Rows.Add(float.Parse(ds.Tables[0].Rows[x].ItemArray[0].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[1].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[2].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[3].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[4].ToString()), ds.Tables[0].Rows[x].ItemArray[5], ds.Tables[0].Rows[x].ItemArray[7], ds.Tables[0].Rows[x].ItemArray[8], ds.Tables[0].Rows[x].ItemArray[9], ds.Tables[0].Rows[x].ItemArray[10].ToString());
                        txttotal.Text = (float.Parse(txttotal.Text.ToString(), System.Globalization.NumberStyles.Currency) + float.Parse(ds.Tables[0].Rows[x].ItemArray[2].ToString())).ToString("C2");
                        //MessageBox.Show(ds.Tables[0].Rows[x].ItemArray[10].ToString());
                        //MessageBox.Show(txttotal.Text);
                    }
                }

            }
            catch (Exception ee) { }
        }

        private void History_Load(object sender, EventArgs e)
        {
            
            for (int x = 2019; x < 2100; x++) {
                comboyear.Items.Add(x);
            }
            comboyear.SelectedIndex = 0;
            combocolumn.SelectedIndex = 6;
            datagridOrders.Rows.Clear();

            string s = txtdate.Text;
            try
            {
                //datagridOrders.Enabled = true;
                //panel5.SendToBack();
                MySqlConnection connection = new MySqlConnection(myConnectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                string query = "SELECT * FROM receipts";
                command.CommandText = query;
                command.ExecuteNonQuery();
                connection.Close();
                //txtsub.Text = "₱0";
                //lbldate.Text = "";
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, myConnectionString))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    //datagridOrders.DataBindings.Clear();
                    //datagridOrders.Rows.Clear();
                    //label2.Text = s;
                    //lbldate.Text = ds.Tables[0].Rows[0].ItemArray[9].ToString();
                    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                    {
                        datagridOrders.Rows.Add(float.Parse(ds.Tables[0].Rows[x].ItemArray[0].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[1].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[2].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[3].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[4].ToString()), ds.Tables[0].Rows[x].ItemArray[5], ds.Tables[0].Rows[x].ItemArray[7], ds.Tables[0].Rows[x].ItemArray[8], ds.Tables[0].Rows[x].ItemArray[9], ds.Tables[0].Rows[x].ItemArray[10].ToString());
                        txttotal.Text = (float.Parse(txttotal.Text.ToString(), System.Globalization.NumberStyles.Currency) + float.Parse(ds.Tables[0].Rows[x].ItemArray[2].ToString())).ToString("C2");
                        //MessageBox.Show(ds.Tables[0].Rows[x].ItemArray[10].ToString());
                        //MessageBox.Show(txttotal.Text);
                    }
                }

            }
            catch (Exception ee) { }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            txttotal.Text = "₱0";
            txtdate.Text = "All Receipts";
            try
            {
                //datagridOrders.Enabled = true;
                //panel5.SendToBack();
                MySqlConnection connection = new MySqlConnection(myConnectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                string query = "SELECT * FROM receipts";
                command.CommandText = query;
                command.ExecuteNonQuery();
                connection.Close();
                //txtsub.Text = "₱0";
                //lbldate.Text = "";
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, myConnectionString))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    //datagridOrders.DataBindings.Clear();
                    datagridOrders.Rows.Clear();
                    //label2.Text = s;
                    //lbldate.Text = ds.Tables[0].Rows[0].ItemArray[9].ToString();
                    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                    {
                        datagridOrders.Rows.Add(float.Parse(ds.Tables[0].Rows[x].ItemArray[0].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[1].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[2].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[3].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[4].ToString()), ds.Tables[0].Rows[x].ItemArray[5], ds.Tables[0].Rows[x].ItemArray[7], ds.Tables[0].Rows[x].ItemArray[8], ds.Tables[0].Rows[x].ItemArray[9].ToString(), ds.Tables[0].Rows[x].ItemArray[10].ToString());
                        txttotal.Text = (float.Parse(txttotal.Text.ToString(), System.Globalization.NumberStyles.Currency) + float.Parse(ds.Tables[0].Rows[x].ItemArray[2].ToString())).ToString("C2");
                        //MessageBox.Show(txttotal.Text);
                    }
                }
            }
            catch (Exception ee) {  try
            {
                //datagridOrders.Enabled = true;
                //panel5.SendToBack();
                MySqlConnection connection = new MySqlConnection(myConnectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                string query = "SELECT * FROM receipts";
                command.CommandText = query;
                command.ExecuteNonQuery();
                connection.Close();
                //txtsub.Text = "₱0";
                //lbldate.Text = "";
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, myConnectionString))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    //datagridOrders.DataBindings.Clear();
                    datagridOrders.Rows.Clear();
                    //label2.Text = s;
                    //lbldate.Text = ds.Tables[0].Rows[0].ItemArray[9].ToString();
                    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                    {
                        datagridOrders.Rows.Add(float.Parse(ds.Tables[0].Rows[x].ItemArray[0].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[1].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[2].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[3].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[4].ToString()), ds.Tables[0].Rows[x].ItemArray[5], ds.Tables[0].Rows[x].ItemArray[7], ds.Tables[0].Rows[x].ItemArray[8], ds.Tables[0].Rows[x].ItemArray[9].ToString(), ds.Tables[0].Rows[x].ItemArray[10].ToString());
                        txttotal.Text = (float.Parse(txttotal.Text.ToString(), System.Globalization.NumberStyles.Currency) + float.Parse(ds.Tables[0].Rows[x].ItemArray[2].ToString())).ToString("C2");
                        //MessageBox.Show(txttotal.Text);
                    }
                } }
               
                 catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString());
                }

        }

        private void datagridOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {

                try
                {
                    datagridReprint.Rows.Clear();
                    panel3.BringToFront();
                    string id = datagridOrders.CurrentRow.Cells[7].Value.ToString();
                    txttable.Text = datagridOrders.CurrentRow.Cells[9].Value.ToString();

                    MySqlConnection connection = new MySqlConnection(myConnectionString);
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    string query = "";
                    try
                    {
                        command.Connection = connection;
                        query = "SELECT * FROM pastorders WHERE receiptid = '" + id + "'";
                        //MessageBox.Show(id + " " + txttable.Text);
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception ee)
                    {
                        //MessageBox.Show(ee.ToString()); 
                    }
                    //txtsub.Text = "₱0";
                    //lbldate.Text = "";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, myConnectionString))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        //datagridOrders.DataBindings.Clear();
                        //datagridOrders.Rows.Clear();
                        //label2.Text = s;
                        //lbldate.Text = ds.Tables[0].Rows[0].ItemArray[9].ToString();
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            try
                            {
                                for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                                {
                                    datagridReprint.Rows.Add(ds.Tables[0].Rows[x].ItemArray[2].ToString(), ds.Tables[0].Rows[x].ItemArray[1].ToString(), float.Parse(ds.Tables[0].Rows[x].ItemArray[3].ToString()), ds.Tables[0].Rows[x].ItemArray[4].ToString(), ds.Tables[0].Rows[x].ItemArray[5].ToString(), ds.Tables[0].Rows[x].ItemArray[5]);
                                    //txttotal.Text = (float.Parse(txttotal.Text.ToString(), System.Globalization.NumberStyles.Currency) + float.Parse(ds.Tables[0].Rows[x].ItemArray[2].ToString())).ToString("C2");
                                    //MessageBox.Show(txttotal.Text);
                                }
                            }
                            catch (Exception ee) {
                                MessageBox.Show("Older transactions prior to may 23 not supported ");
                            }
                        }
                       
                    }

                }
                catch (Exception ee)
                {
                    //MessageBox.Show(ee.ToString());
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            panel3.SendToBack();
            datagridReprint.Rows.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("custom", 816, 1056);
            printPreviewDialog1.Document = printDocument1;
            printDocument1.Print();
            //printPreviewDialog1.Show();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                //e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(10, 10));
                e.Graphics.DrawString("The Coastal Gastro District Cafe", new Font("FakeReceipt-Regular", 9, FontStyle.Bold), Brushes.Black, new Point(10, 10));
                e.Graphics.DrawString("Contact #: 09164929330", new Font("Arial", 7, FontStyle.Regular), Brushes.Black, new Point(10, 30));
                e.Graphics.DrawString("Seaside Coastal Road, Malaubang Ozamiz City", new Font("Arial", 7, FontStyle.Regular), Brushes.Black, new Point(10, 45));
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(10, 53));
                //e.Graphics.DrawString("Receipt ID: " + string.Format("{0:D8}", int.Parse(datagridOrders.CurrentRow.Cells[0].Value.ToString())), new Font("OCR A", 7, FontStyle.Regular), Brushes.Black, new Point(10, 65));
                e.Graphics.DrawString("Issued: " + DateTime.Now.ToString("dd-MMM-yy hh:mm:ss tt"), new Font("Arial", 7, FontStyle.Regular), Brushes.Black, new Point(10, 65));
                e.Graphics.DrawString(txttable.Text.ToString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(10, 80));
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 7, FontStyle.Regular), Brushes.Black, new Point(10, 103));

                e.Graphics.DrawString("The Coastal Gastro District Cafe", new Font("FakeReceipt-Regular", 9, FontStyle.Bold), Brushes.Black, new Point(315, 10));
                e.Graphics.DrawString("Contact #: 09164929330", new Font("Arial", 7, FontStyle.Regular), Brushes.Black, new Point(315, 30));
                e.Graphics.DrawString("Seaside Coastal Road, Malaubang Ozamiz City", new Font("Arial", 7, FontStyle.Regular), Brushes.Black, new Point(315, 45));
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(315, 53));
                //e.Graphics.DrawString("Receipt ID: " + string.Format("{0:D8}", int.Parse(datagridOrders.CurrentRow.Cells[0].Value.ToString())), new Font("OCR A", 7, FontStyle.Regular), Brushes.Black, new Point(315, 65));
                e.Graphics.DrawString("Issued: " + DateTime.Now.ToString("dd-MMM-yy hh:mm:ss tt"), new Font("Arial", 7, FontStyle.Regular), Brushes.Black, new Point(315, 65));
                e.Graphics.DrawString(txttable.Text.ToString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(315, 80));
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 7, FontStyle.Regular), Brushes.Black, new Point(315, 103));
                //e.Graphics.DrawImage(newImage, xx, y, srcRect, units);
            }
            catch (Exception eee)
            {// MessageBox.Show(eee.ToString()); 
            }
            try
            {
                int level = 115;
                for (int x = 0; x < datagridReprint.Rows.Count; x++)
                {
                    string a = "";
                    if (datagridReprint.Rows[x].Cells[0].Value.ToString().Length > 40)
                    {
                        a = datagridReprint.Rows[x].Cells[0].Value.ToString().Substring(0, 20);
                    }

                    else
                    {
                        a = datagridReprint.Rows[x].Cells[0].Value.ToString();
                    }

                    e.Graphics.DrawString(datagridReprint.Rows[x].Cells[1].Value.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, level));
                    e.Graphics.DrawString(a, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(30, level));

                    e.Graphics.DrawString(datagridReprint.Rows[x].Cells[1].Value.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(315, level));
                    e.Graphics.DrawString(a, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(335, level));

                    if (datagridReprint.Rows[x].Cells[4].Value.ToString() == "Cancelled") { }
                    else
                    {
                        e.Graphics.DrawString("₱" + datagridReprint.Rows[x].Cells[2].Value.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(210, level));
                        e.Graphics.DrawString("₱" + datagridReprint.Rows[x].Cells[2].Value.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(515, level));
                    }
                    //e.Graphics.DrawString(dataGridView2.Rows[x].Cells[3].Value.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(250, level));

                    level += 15;
                }
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, level));
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(315, level));
                e.Graphics.DrawString("Subtotal", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(150, level += 15));
                e.Graphics.DrawString("Subtotal", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(415, level));
                e.Graphics.DrawString("₱" + datagridOrders.CurrentRow.Cells[1].Value.ToString(), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(210, level));
                e.Graphics.DrawString("₱" + datagridOrders.CurrentRow.Cells[1].Value.ToString(), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(515, level));
                e.Graphics.DrawString("Total", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(150, level += 15));
                e.Graphics.DrawString("Total", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(415, level));
                e.Graphics.DrawString("₱" + datagridOrders.CurrentRow.Cells[2].Value.ToString(), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(210, level));
                e.Graphics.DrawString("₱" + datagridOrders.CurrentRow.Cells[2].Value.ToString(), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(515, level));

                e.Graphics.DrawString("Received", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(150, level += 15));
                e.Graphics.DrawString("Received", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(415, level));
                e.Graphics.DrawString("₱" + datagridOrders.CurrentRow.Cells[3].Value.ToString(), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(210, level));
                e.Graphics.DrawString("₱" + datagridOrders.CurrentRow.Cells[3].Value.ToString(), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(515, level));
                e.Graphics.DrawString("Change", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(150, level += 15));
                e.Graphics.DrawString("Change", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(415, level));
                e.Graphics.DrawString("₱" + datagridOrders.CurrentRow.Cells[4].Value.ToString(), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(210, level));
                e.Graphics.DrawString("₱" + datagridOrders.CurrentRow.Cells[4].Value.ToString(), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(515, level));

                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, level += 15));
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(315, level));
                e.Graphics.DrawString("Note: This Serves as a Temporary Receipt ", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(10, level += 15));
                e.Graphics.DrawString("Note: This Serves as a Temporary Receipt ", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(315, level));
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, level += 15));
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(315, level));
            }

            catch (Exception ee)

            {
                //MessageBox.Show(ee.ToString());
            }
        }

        private void datagridReprint_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Radiodaily_CheckedChanged(object sender, EventArgs e)
        {
            panelday.BringToFront();
        }

        private void Radiomonthly_CheckedChanged(object sender, EventArgs e)
        {
            panelmonth.BringToFront();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            txtsearch.Text = "";
            datagridOrders.Rows.Clear();
            txttotal.Text = "₱0";
            //txtdate.Text = "All Receipts";
            string s = (sender as Button).Text;
            txtmonth.Text = s;
            try
            {
                //datagridOrders.Enabled = true;
                //panel5.SendToBack();
                MySqlConnection connection = new MySqlConnection(myConnectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                string query = "SELECT * FROM receipts";
                command.CommandText = query;
                command.ExecuteNonQuery();
                connection.Close();
                //txtsub.Text = "₱0";
                //lbldate.Text = "";
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, myConnectionString))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    //datagridOrders.DataBindings.Clear();
                    datagridOrders.Rows.Clear();
                    //label2.Text = s;
                    //lbldate.Text = ds.Tables[0].Rows[0].ItemArray[9].ToString();
                    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                    {
                        if(ds.Tables[0].Rows[x].ItemArray[7].ToString().Contains(s) && ds.Tables[0].Rows[x].ItemArray[7].ToString().Contains(comboyear.Text.ToString()))
                        {
                            datagridOrders.Rows.Add(float.Parse(ds.Tables[0].Rows[x].ItemArray[0].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[1].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[2].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[3].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[4].ToString()), ds.Tables[0].Rows[x].ItemArray[5], ds.Tables[0].Rows[x].ItemArray[7], ds.Tables[0].Rows[x].ItemArray[8], ds.Tables[0].Rows[x].ItemArray[9].ToString(), ds.Tables[0].Rows[x].ItemArray[10].ToString());
                            txttotal.Text = (float.Parse(txttotal.Text.ToString().Replace("₱", "")) + float.Parse(ds.Tables[0].Rows[x].ItemArray[2].ToString())).ToString("C2");
                        }
                    }
                }
            }
            catch (Exception ee) { MessageBox.Show(ee.ToString()); }
        }

        private void Comboyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            datagridOrders.Rows.Clear();
            txttotal.Text = "₱0";
            //txtdate.Text = "All Receipts";
            //string s = (sender as Button).Text;
            //txtmonth.Text = s;
            try
            {
                //datagridOrders.Enabled = true;
                //panel5.SendToBack();
                MySqlConnection connection = new MySqlConnection(myConnectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                string query = "SELECT * FROM receipts";
                command.CommandText = query;
                command.ExecuteNonQuery();
                connection.Close();
                //txtsub.Text = "₱0";
                //lbldate.Text = "";
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, myConnectionString))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    //datagridOrders.DataBindings.Clear();
                    datagridOrders.Rows.Clear();
                    //label2.Text = s;
                    //lbldate.Text = ds.Tables[0].Rows[0].ItemArray[9].ToString();
                    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                    {
                        if (ds.Tables[0].Rows[x].ItemArray[7].ToString().Contains(txtmonth.Text.ToString()) && ds.Tables[0].Rows[x].ItemArray[7].ToString().Contains(comboyear.Text.ToString()))
                        {
                            datagridOrders.Rows.Add(float.Parse(ds.Tables[0].Rows[x].ItemArray[0].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[1].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[2].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[3].ToString()), float.Parse(ds.Tables[0].Rows[x].ItemArray[4].ToString()), ds.Tables[0].Rows[x].ItemArray[5], ds.Tables[0].Rows[x].ItemArray[7], ds.Tables[0].Rows[x].ItemArray[8], ds.Tables[0].Rows[x].ItemArray[9].ToString(), ds.Tables[0].Rows[x].ItemArray[10].ToString());
                            txttotal.Text =  (float.Parse(txttotal.Text.ToString().Replace("₱", "")) + float.Parse(ds.Tables[0].Rows[x].ItemArray[2].ToString())).ToString("C2");
                        }
                    }
                }
            }
            catch (Exception ee) { MessageBox.Show(ee.ToString()); }
        }

        private void Combocolumn_SelectedIndexChanged(object sender, EventArgs e)
        {

            
            string s = txtsearch.Text.ToString().ToLower();
            if (txtsearch.Text != "")
            {
                txttotal.Text = "₱0";
                for (int x = 0; x < datagridOrders.Rows.Count; x++)
                {
                    if (datagridOrders.Rows[x].Cells[(combocolumn.SelectedIndex + 2)].Value.ToString().ToLower().Contains(s))
                    {
                        datagridOrders.Rows[x].Visible = true;
                        txttotal.Text = (float.Parse(txttotal.Text.ToString().Replace("₱", "")) + float.Parse(datagridOrders.Rows[x].Cells[2].Value.ToString().Replace("₱", ""))).ToString("C2");
                    }
                    else
                    {
                        datagridOrders.Rows[x].Visible = false;
                    }
                }
            }
            else if (txtsearch.Text == "")
            {
                txttotal.Text = "₱0";
                for (int x = 0; x < datagridOrders.Rows.Count; x++)
                {
                    
                    datagridOrders.Rows[x].Visible = true;
                    txttotal.Text = (float.Parse(txttotal.Text.ToString().Replace("₱", "")) + float.Parse(datagridOrders.Rows[x].Cells[2].Value.ToString().Replace("₱", ""))).ToString("C2");
                }
            }
        }
        private void Txtsearch_TextChanged(object sender, EventArgs e)
        {
            
            string s = txtsearch.Text.ToString().ToLower();
            if (txtsearch.Text != "")
               
            { txttotal.Text = "₱0";
               
                for (int x = 0; x < datagridOrders.Rows.Count; x++)
                {
                    if (datagridOrders.Rows[x].Cells[(combocolumn.SelectedIndex + 2)].Value.ToString().ToLower().Contains(s))
                    {
                        datagridOrders.Rows[x].Visible = true;
                        txttotal.Text =(float.Parse(txttotal.Text.ToString().Replace("₱", "")) + float.Parse(datagridOrders.Rows[x].Cells[2].Value.ToString().Replace("₱", ""))).ToString("C2");
                    }
                    else
                    {
                        datagridOrders.Rows[x].Visible = false;
                    }
                }
            }
            else if (txtsearch.Text == "") {
                txttotal.Text = "₱0";
                for (int x = 0; x < datagridOrders.Rows.Count; x++)
                {
                    
                    datagridOrders.Rows[x].Visible = true;
                    txttotal.Text = (float.Parse(txttotal.Text.ToString().Replace("₱", "")) + float.Parse(datagridOrders.Rows[x].Cells[2].Value.ToString().Replace("₱", ""))).ToString("C2");
                }
            }
        }
    }
}