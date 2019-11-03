using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing.Printing; 
namespace Cashier
{
    public partial class Tables : UserControl
    {
      
        //string myConnectionString = "server=192.168.15.6; uid=root; pwd=; database=menu";
        //string myConnectionString = "server=localhost; uid=root; pwd=; database=menu";

        //string myConnectionString = "server=192.168.15.8; uid=root; pwd=; database=menu";
        //string myConnectionString = "server=192.168.0.105; uid=roota; pwd=; database=menu";
        string myConnectionString = "server=localhost; uid=root; pwd=; database=menu";
        public Tables()
        {
            InitializeComponent();
        }

        private void table1_Click(object sender, EventArgs e)
        {
            btnBillOut.Enabled = false;
            btnPay.Enabled = false;

            {
                string s = (sender as Button).Text;
                try
                {
                    datagridOrders.Enabled = true;
                    panel5.SendToBack();
                    MySqlConnection connection = new MySqlConnection(myConnectionString);
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.Connection = connection;
                    string query = "SELECT * FROM orders WHERE tableid = '" + s + "'";
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    connection.Close();
                    txtsub.Text = "₱0";
                    lbldate.Text = "";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, myConnectionString))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        datagridOrders.DataBindings.Clear();
                        datagridOrders.Rows.Clear();
                        lbltable.Text = s;
                        lbldate.Text = ds.Tables[0].Rows[0].ItemArray[9].ToString();
                        for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                        {

                            datagridOrders.Rows.Add(ds.Tables[0].Rows[x].ItemArray[0], ds.Tables[0].Rows[x].ItemArray[4], ds.Tables[0].Rows[x].ItemArray[3], ds.Tables[0].Rows[x].ItemArray[5], ds.Tables[0].Rows[x].ItemArray[6], float.Parse(ds.Tables[0].Rows[x].ItemArray[10].ToString()), false, false, false, ds.Tables[0].Rows[x].ItemArray[8].ToString(),"");
                            if (ds.Tables[0].Rows[x].ItemArray[6].ToString() != "Cancelled")
                            {
                                txtsub.Text = "₱" + (float.Parse(txtsub.Text.ToString(), System.Globalization.NumberStyles.Currency) + float.Parse(ds.Tables[0].Rows[x].ItemArray[10].ToString())).ToString();
                                txtsub2.Text = txtsub.Text;
                                txtTotal.Text = txtsub.Text;

                            }
                        }
                        btnBillOut.Enabled = true;
                        btnPay.Enabled = true;
                    }

                }
                catch (Exception ee) { }
            }


        }

        public void clear() {
            lbldate.Text = "";
            lbltable.Text = "";
            datagridOrders.Rows.Clear();
            btnPay.Enabled = false;
            btnBillOut.Enabled = false;
            panel5.SendToBack();
        }

        private void btnBillOut_Click(object sender, EventArgs e)
        {
            printDocument2.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("custom", 300, 1056);
            printPreviewDialog2.Document = printDocument2;
            //printPreviewDialog2.ShowDialog();
            printDocument2.Print();
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
                e.Graphics.DrawString(lbltable.Text.ToString(), new Font("Arial", 12, FontStyle.Regular),  Brushes.Black, new Point(10, 80));
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 7, FontStyle.Regular), Brushes.Black, new Point(10, 103));

                e.Graphics.DrawString("The Coastal Gastro District Cafe", new Font("FakeReceipt-Regular", 9, FontStyle.Bold), Brushes.Black, new Point(315, 10));
                e.Graphics.DrawString("Contact #: 09164929330", new Font("Arial", 7, FontStyle.Regular), Brushes.Black, new Point(315, 30));
                e.Graphics.DrawString("Seaside Coastal Road, Malaubang Ozamiz City", new Font("Arial", 7, FontStyle.Regular), Brushes.Black, new Point(315, 45));
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(315, 53));
                //e.Graphics.DrawString("Receipt ID: " + string.Format("{0:D8}", int.Parse(datagridOrders.CurrentRow.Cells[0].Value.ToString())), new Font("OCR A", 7, FontStyle.Regular), Brushes.Black, new Point(315, 65));
                e.Graphics.DrawString("Issued: " + DateTime.Now.ToString("dd-MMM-yy hh:mm:ss tt"), new Font("Arial", 7, FontStyle.Regular), Brushes.Black, new Point(315, 65));
                e.Graphics.DrawString(lbltable.Text.ToString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(315, 80));
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 7, FontStyle.Regular), Brushes.Black, new Point(315, 103));
                //e.Graphics.DrawImage(newImage, xx, y, srcRect, units);
            }
            catch (Exception eee)
            {// MessageBox.Show(eee.ToString()); 
            }
            try
            {
                int level = 115;
                for (int x = 0; x < datagridOrders.Rows.Count; x++)
                {
                    string a = "";
                    if (datagridOrders.Rows[x].Cells[2].Value.ToString().Length > 40)
                    {
                        a = datagridOrders.Rows[x].Cells[2].Value.ToString().Substring(0, 20);
                    }

                    else
                    {
                        a = datagridOrders.Rows[x].Cells[2].Value.ToString();
                    }

                    e.Graphics.DrawString(datagridOrders.Rows[x].Cells[1].Value.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, level));
                    e.Graphics.DrawString(a, new Font("Arial",8, FontStyle.Regular), Brushes.Black, new Point(30, level));

                    e.Graphics.DrawString(datagridOrders.Rows[x].Cells[1].Value.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(315, level));
                    e.Graphics.DrawString(a, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(335, level));

                    if (datagridOrders.Rows[x].Cells[4].Value.ToString() == "Cancelled") { }
                    else
                    {
                        e.Graphics.DrawString("₱" + datagridOrders.Rows[x].Cells[5].Value.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(210, level));
                        e.Graphics.DrawString("₱" + datagridOrders.Rows[x].Cells[5].Value.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(515, level));
                    }
                    //e.Graphics.DrawString(dataGridView2.Rows[x].Cells[3].Value.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(250, level));

                    level += 15;
                }
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, level));
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(315, level));
                e.Graphics.DrawString("Subtotal", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(150, level += 15));
                e.Graphics.DrawString("Subtotal", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(415, level));
                e.Graphics.DrawString(txtsub2.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(210, level));
                e.Graphics.DrawString(txtsub2.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(515, level));
                e.Graphics.DrawString("Total", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(150, level += 15));
                e.Graphics.DrawString("Total", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(415, level));
                e.Graphics.DrawString(txtTotal.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(210, level));
                e.Graphics.DrawString(txtTotal.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(515, level));

                e.Graphics.DrawString("Received", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(150, level += 15));
                e.Graphics.DrawString("Received", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(415, level));
                e.Graphics.DrawString("₱" + txtcash.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(210, level));
                e.Graphics.DrawString("₱" + txtcash.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(515, level));
                e.Graphics.DrawString("Change", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(150, level += 15));
                e.Graphics.DrawString("Change", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(415, level));
                e.Graphics.DrawString(txtChange.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(210, level));
                e.Graphics.DrawString(txtChange.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(515, level));

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

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.DrawString("The Coastal Gastro District Cafe", new Font("FakeReceipt-Regular", 9, FontStyle.Bold), Brushes.Black, new Point(10, 10));
                e.Graphics.DrawString("Contact #: 09164929330", new Font("Arial", 7, FontStyle.Regular), Brushes.Black, new Point(10, 30));
                e.Graphics.DrawString("Seaside Coastal Road, Malaubang Ozamiz City", new Font("Arial", 7, FontStyle.Regular), Brushes.Black, new Point(10, 45));
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, 53));
                //e.Graphics.DrawString("Receipt ID: " + string.Format("{0:D8}", int.Parse(datagridOrders.CurrentRow.Cells[0].Value.ToString())), new Font("OCR A", 7, FontStyle.Regular), Brushes.Black, new Point(10, 65));
                e.Graphics.DrawString("Issued: " + DateTime.Now.ToString("dd-MMM-yy hh:mm:ss tt"), new Font("Arial", 7, FontStyle.Regular), Brushes.Black, new Point(10, 65));
                e.Graphics.DrawString(lbltable.Text.ToString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(10, 80));
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, 103));
            }
            catch (Exception ee) { }
            try
            {
                int level = 115;
                for (int x = 0; x < datagridOrders.Rows.Count; x++)
                {
                    string a = "";
                    if (datagridOrders.Rows[x].Cells[2].Value.ToString().Length > 40)
                    {
                        a = datagridOrders.Rows[x].Cells[2].Value.ToString().Substring(0, 20);
                    }

                    else
                    {
                        a = datagridOrders.Rows[x].Cells[2].Value.ToString();
                    }

                    e.Graphics.DrawString(datagridOrders.Rows[x].Cells[1].Value.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, level));
                    e.Graphics.DrawString(a, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(30, level));

                    //e.Graphics.DrawString(datagridOrders.Rows[x].Cells[1].Value.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(315, level));
                    //e.Graphics.DrawString(a, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(335, level));

                    if (datagridOrders.Rows[x].Cells[4].Value.ToString() == "Cancelled") { }
                    else
                    {
                        e.Graphics.DrawString("₱" + datagridOrders.Rows[x].Cells[5].Value.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(210, level));
                        //e.Graphics.DrawString("₱" + datagridOrders.Rows[x].Cells[5].Value.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(515, level));
                    }
                    //e.Graphics.DrawString(dataGridView2.Rows[x].Cells[3].Value.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(250, level));

                    level += 15;
                }
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, level));
                e.Graphics.DrawString("Total", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(150, level += 15));
                e.Graphics.DrawString(txtsub.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(210, level));
                //e.Graphics.DrawString("Total", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(150, level += 15));
                //e.Graphics.DrawString(txtsub.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(210, level));
                //e.Graphics.DrawString("Cash", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(150, level += 15));
                //e.Graphics.DrawString("Change", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(150, level += 15));

                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, level += 15));
                e.Graphics.DrawString("Note: This Serves as a Temporary Receipt ", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(10, level += 15));
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, level += 15));
            }

            catch (Exception ee)

            {
                //MessageBox.Show(ee.ToString());
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (datagridOrders.Rows.Count != 0)
            {
                btnPay.Enabled = false;
                btnBillOut.Enabled = false;
                datagridOrders.Enabled = false;
                panel5.BringToFront();
            }
        }

        private void datagridOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(datagridOrders.Rows[e.RowIndex].Cells[7].Value.ToString());
            if(e.RowIndex != -1) {
                //MessageBox.Show("not header");
                if (e.ColumnIndex == 6 && bool.Parse(datagridOrders.CurrentRow.Cells[6].Value.ToString()) == false)
                {

                    if (datagridOrders.CurrentRow.Cells[4].Value.ToString() != "Cancelled")
                    {
                        datagridOrders.CurrentRow.Cells[6].Value = true;
                        //datagridOrders.CurrentRow.Cells[7].Value = ((float.Parse(datagridOrders.CurrentRow.Cells[5].Value.ToString()) * 0.2).ToString());
                        txtTotal.Text = "₱" + (float.Parse(txtsub.Text, System.Globalization.NumberStyles.Currency) - (float.Parse(datagridOrders.CurrentRow.Cells[5].Value.ToString())) * .1).ToString();
                        txtsub.Text = txtTotal.Text;
                        txtsubhidden.Text = txtsub2.Text;
                    }
                    //txtTotal.Text = txtsub.Text;
                }

                else if (e.ColumnIndex == 6 && bool.Parse(datagridOrders.CurrentRow.Cells[6].Value.ToString()) == true)
                {
                    if (datagridOrders.CurrentRow.Cells[4].Value.ToString() != "Cancelled")
                    {
                        datagridOrders.CurrentRow.Cells[6].Value = false;
                        //datagridOrders.CurrentRow.Cells[7].Value = ((float.Parse(datagridOrders.CurrentRow.Cells[5].Value.ToString()) * 0.2).ToString());
                        txtTotal.Text = "₱" + (float.Parse(txtsub.Text, System.Globalization.NumberStyles.Currency) + (float.Parse(datagridOrders.CurrentRow.Cells[5].Value.ToString())) * .1).ToString();
                        txtsub.Text = txtTotal.Text;
                        txtsubhidden.Text = txtsub2.Text;
                        //txtTotal.Text = txtsub.Text;
                    }
                }

                else if (e.ColumnIndex == 7 && bool.Parse(datagridOrders.CurrentRow.Cells[7].Value.ToString()) == false)
                {
                    if (datagridOrders.CurrentRow.Cells[4].Value.ToString() != "Cancelled")
                    {

                        datagridOrders.CurrentRow.Cells[7].Value = true;
                        //datagridOrders.CurrentRow.Cells[7].Value = ((float.Parse(datagridOrders.CurrentRow.Cells[5].Value.ToString()) * 0.3).ToString());
                        txtTotal.Text = "₱" + (float.Parse(txtsub.Text, System.Globalization.NumberStyles.Currency) - (float.Parse(datagridOrders.CurrentRow.Cells[5].Value.ToString())) * .2).ToString();
                        txtsub.Text = txtTotal.Text;
                        txtsubhidden.Text = txtsub2.Text;
                        //txtTotal.Text = txtsub.Text;
                    }
                }

                else if (e.ColumnIndex == 7 && bool.Parse(datagridOrders.CurrentRow.Cells[7].Value.ToString()) == true)
                {
                    if (datagridOrders.CurrentRow.Cells[4].Value.ToString() != "Cancelled")
                    {

                        datagridOrders.CurrentRow.Cells[7].Value = false;
                        //datagridOrders.CurrentRow.Cells[7].Value = ((float.Parse(datagridOrders.CurrentRow.Cells[5].Value.ToString()) * 0.3).ToString());
                        txtTotal.Text = "₱" + (float.Parse(txtsub.Text, System.Globalization.NumberStyles.Currency) + (float.Parse(datagridOrders.CurrentRow.Cells[5].Value.ToString())) * .2).ToString();
                        txtsub.Text = txtTotal.Text;
                        txtsubhidden.Text = txtsub2.Text;
                        //txtTotal.Text = txtsub.Text;
                    }
                }

                else if (e.ColumnIndex == 8 && bool.Parse(datagridOrders.CurrentRow.Cells[8].Value.ToString()) == false)
                {
                    if (datagridOrders.CurrentRow.Cells[4].Value.ToString() != "Cancelled")
                    {

                        datagridOrders.CurrentRow.Cells[8].Value = true;
                        //datagridOrders.CurrentRow.Cells[7].Value = ((float.Parse(datagridOrders.CurrentRow.Cells[5].Value.ToString()) * 0.3).ToString());
                        txtTotal.Text = "₱" + (float.Parse(txtsub.Text, System.Globalization.NumberStyles.Currency) - (float.Parse(datagridOrders.CurrentRow.Cells[5].Value.ToString())) * .3).ToString();
                        txtsub.Text = txtTotal.Text;
                        txtsubhidden.Text = txtsub2.Text;
                        //txtTotal.Text = txtsub.Text;
                    }
                }

                else if (e.ColumnIndex == 8 && bool.Parse(datagridOrders.CurrentRow.Cells[8].Value.ToString()) == true)
                {
                    if (datagridOrders.CurrentRow.Cells[4].Value.ToString() != "Cancelled")
                    {

                        datagridOrders.CurrentRow.Cells[8].Value = false;
                        //datagridOrders.CurrentRow.Cells[7].Value = ((float.Parse(datagridOrders.CurrentRow.Cells[5].Value.ToString()) * 0.3).ToString());
                        txtTotal.Text = "₱" + (float.Parse(txtsub.Text, System.Globalization.NumberStyles.Currency) + (float.Parse(datagridOrders.CurrentRow.Cells[5].Value.ToString())) * .3).ToString();
                        txtsub.Text = txtTotal.Text;
                        txtsubhidden.Text = txtsub2.Text;
                        //txtTotal.Text = txtsub.Text;
                    }
                }

                else if (e.ColumnIndex == 10)
                {
                    //MessageBox.Show("clicked here "+datagridOrders.CurrentRow.Cells[9].Value.ToString()+" _");   
                    MySqlConnection connection = new MySqlConnection(myConnectionString);
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.Connection = connection;
                    string s = "";

                    if (datagridOrders.CurrentRow.Cells[4].Value.ToString() == "Cancelled")
                    {
                        s = "Served";
                        txtTotal.Text = "₱" + (float.Parse(txtTotal.Text, System.Globalization.NumberStyles.Currency) + float.Parse(datagridOrders.CurrentRow.Cells[5].Value.ToString())).ToString();
                        //txtTotal.Text = txtcash.Text;
                        txtsub.Text = txtTotal.Text;
                    }
                    else if (datagridOrders.CurrentRow.Cells[4].Value.ToString() == "Served" || datagridOrders.CurrentRow.Cells[4].Value.ToString() == "prep")
                    {
                        s = "Cancelled";
                        txtTotal.Text = "₱" + (float.Parse(txtTotal.Text, System.Globalization.NumberStyles.Currency) - float.Parse(datagridOrders.CurrentRow.Cells[5].Value.ToString())).ToString();
                        //txtTotal.Text = txtcash.Text;
                        txtsub.Text = txtTotal.Text;
                    }
                    datagridOrders.CurrentRow.Cells[4].Value = s;
                    string query = "UPDATE orders SET itemstat ='" + s + "' WHERE id = '" + datagridOrders.CurrentRow.Cells[0].Value.ToString() + "'";
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtcash.Text = "";
            combodisc.SelectedIndex = -1;
            panel5.SendToBack();

            btnPay.Enabled = true;
            btnBillOut.Enabled = true;
            datagridOrders.Enabled = true;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtcash.Text = "";
            combodisc.SelectedIndex = -1;
            panel5.SendToBack();

            btnPay.Enabled = true;
            btnBillOut.Enabled = true;
            datagridOrders.Enabled = true;
        }

        private void txtcash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtcash.Text == "") { txtChange.Text = "₱" + " "; }
                if (txtcash.Text != "")
                {
                    string s = "₱" + (float.Parse(txtcash.Text, System.Globalization.NumberStyles.Currency) - float.Parse(txtTotal.Text, System.Globalization.NumberStyles.Currency)).ToString();
                    if (float.Parse(s.Substring(1)) < 0)
                    {
                        txtChange.Text = "₱" + " ";
                    }
                    else
                    {
                        txtChange.Text = s;
                    }
                }
            }
            catch (Exception ee) { }
        }

        private void txtcash_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnconfirm_Click(object sender, EventArgs e)
        {
            if (txtcash.Text != "" && txtChange.Text.Substring(1) != " ")
            {
                if (float.Parse(txtChange.Text.ToString().Substring(1)) >= 0)
                {
                    try
                    {

                        string ss = lbltable.Text;
                        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                        var stringChars = new char[8];
                        var random = new Random();

                        for (int i = 0; i < stringChars.Length; i++)
                        {
                            stringChars[i] = chars[random.Next(chars.Length)];
                        }

                        var finalString = new String(stringChars);

                        MySqlConnection connection = new MySqlConnection(myConnectionString);
                        string query = "";
                        connection.Open();
                        MySqlCommand command = connection.CreateCommand();
                        command.Connection = connection;
                        string disc = "";
                        if (float.Parse(txtTotal.Text.ToString().Replace("₱", "")) < float.Parse(txtsub2.Text.ToString().Replace("₱", ""))) { disc = "Discounted"; }

                        //connection.Open();

                            string s = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm:ss tt");
                            query = "INSERT INTO receipts ( subtotal, total, received, cashchange, discounted, datetimeordered, datetimeissued, receiptid, cashier, tableid) VALUES('" + txtsub2.Text.ToString().Substring(1) + "','" + txtTotal.Text.ToString().Substring(1) + "','" + txtcash.Text.ToString() + "','" + txtChange.Text.ToString().Substring(1) + "','" + disc + "','" + lbldate.Text + "','" + s + "','" + finalString + "','" + mon.cashier() + "','" + ss + "')";
                            command.CommandText = query;
                            command.ExecuteNonQuery();

                            for (int x = 0; x < datagridOrders.Rows.Count; x++)
                            {
                                query = "INSERT INTO pastorders (itemquantity, itemname, itemnote, itemstat, itemprice, datetimeissued, receiptid, cashier) VALUES('" + datagridOrders.Rows[x].Cells[1].Value + "','" + datagridOrders.Rows[x].Cells[2].Value + "','" + datagridOrders.Rows[x].Cells[3].Value + "','" + datagridOrders.Rows[x].Cells[4].Value + "','" + datagridOrders.Rows[x].Cells[5].Value + "','" + s + "','" + finalString + "','" + mon.cashier() + "')";
                                command.CommandText = query;
                                command.ExecuteNonQuery();
                            }


                        query = "SELECT total FROM cashier WHERE fname = '" + mon.cashier() + "'";
                        //MessageBox.Show(mon.cashier());
                        //command.CommandText = query;
                        command.CommandText = query;
                        Object temp = command.ExecuteScalar();
                        float total = float.Parse(temp.ToString());
                        total += float.Parse(txtsub.Text.ToString(), System.Globalization.NumberStyles.Currency);

                        try
                        {
                            query = "UPDATE cashier SET total = '" + total.ToString() + "' WHERE fname = '" + mon.cashier() + "'";
                            command.CommandText = query;
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ee) { MessageBox.Show("cashier "+ee.ToString()); }

                        try
                        {
                            query = "DELETE FROM orders WHERE tableid = '" + lbltable.Text.ToString() + "'";
                            command.CommandText = query;
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ee) { MessageBox.Show("orders "+ee.ToString()); }

                        connection.Close();


                        //MessageBox.Show("Transaction Successful");
                        panel5.SendToBack();
                        

                        printDocument1.DefaultPageSettings.PaperSize = new PaperSize("custom", 816, 1056);
                        printPreviewDialog1.Document = printDocument1;
                        printDocument1.Print();
                        //printPreviewDialog1.ShowDialog();

                        btnPay.Enabled = false;
                        btnBillOut.Enabled = false;
                        txtsub.Text = "₱" + " ";
                        txtcash.Text = "";
                        txtChange.Text = "₱" + " ";

                        datagridOrders.Rows.Clear();
                    }
                    catch (Exception ee) {
                        MessageBox.Show(ee.ToString());
                    }
                }
            }
        }

        public Form1 mon
        {
            get
            {
                var parent = Parent;
                while (parent != null && (parent as Form1) == null)
                {
                    parent = parent.Parent;
                }
                return parent as Form1;
            }
        }

        private void combodisc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combodisc.SelectedIndex == 0)
            {
                txtsub.Text = "₱" + (float.Parse(txtsubhidden.Text.Substring(1)) - (float.Parse(txtsubhidden.Text.Substring(1)) * 0.2)).ToString();
                txtsub2.Text = txtsub.Text;
            }
        }

        private void Tables_Load(object sender, EventArgs e)
        {
            for (int i= 1; i < 301; i++)
            {
                Button button = addbutton(i);
                button.Tag = i;
                flowLayoutPanel1.Controls.Add(button);
                button.Click += new EventHandler(this.table1_Click);
            }

        }
        Button addbutton(int i)
        {
            Button l = new Button();
            l.Name = "Table " + i.ToString();
            l.Text = "Table " + i.ToString();
            l.ForeColor = Color.Black;
            l.BackColor = Color.WhiteSmoke;
            l.FlatStyle = FlatStyle.Flat;
            l.FlatAppearance.MouseOverBackColor = Color.DarkGray;
            l.FlatAppearance.BorderSize = 0;
            l.Font = new Font("Fake Receipt", 11);
            l.Width = 110;
            l.Height = 48;
            l.TextAlign = ContentAlignment.MiddleCenter;
            //l.Margin = new Padding(5);

            return l;


        }
    }
}