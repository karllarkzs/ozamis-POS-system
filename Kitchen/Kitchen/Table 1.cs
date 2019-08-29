using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using MySql.Data.MySqlClient;
using System.Media;

namespace Kitchen
{
    public partial class Table_1 : UserControl
    {

        //string myConnectionString = "server=192.168.15.6; uid=root; pwd=; database=menu";
        //string myConnectionString = "server=localhost; uid=root; pwd=; database=menu";
        string myConnectionString = "server=192.168.0.105; uid=roota; pwd=; database=menu";
        public Table_1()
        {
            InitializeComponent();
        }

        private void Table_1_Load(object sender, EventArgs e)
        {
           
            loadGrid();
            

        }

       

        delegate void SetTextCallback(string text);
        private void SetText(string text)
        {

            if (this.lblorder.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.lblorder.Text = text;
            }
        }


        public void zee()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                string query = "SELECT count FROM ordercount";
                command.CommandText = query;
                Object temp = command.ExecuteScalar();
                SetText((int.Parse(temp.ToString()) - 1).ToString());
            }
            catch (Exception ee) {
                //MessageBox.Show(ee.ToString());
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex != 4)
            {
                this.dataGridView1.ClearSelection();
            }
        }

        public void loadGrid()
        {
            try
            {

                

                zee();
                mon.populateQueue(label1.Text.ToString(), lblorder.Text.ToString());
            //MessageBox.Show(lblorder.Text.ToString());


            try_again:

                Thread.Sleep(1000);
                MySqlConnection connection = new MySqlConnection(myConnectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                string query = "SELECT * FROM orders WHERE tableid = '"+label1.Text.ToString()+"' AND orderid = '"+lblorder.Text.ToString()+"'";
                //MessageBox.Show(label1.Text.ToString()+"  = "+ lblorder.Text.ToString());
                command.CommandText = query;
                //Object temp = command.ExecuteScalar();
                //labelq.Text = temp.ToString();
                try { command.ExecuteNonQuery(); } catch (Exception ee) {
                    Console.WriteLine("Failed retrieval. Trying again...");
                    dataGridView1.Rows.Clear();
                    connection.Close();
                    goto try_again;
                }
                connection.Close();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, myConnectionString))
                {
                    dataGridView1.DataBindings.Clear();
                    dataGridView1.Rows.Clear();
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);


                    try
                    {
                        for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                        {
                            //MessageBox.Show(ds.Tables[0].Rows[x].ItemArray[8].ToString());
                            if (ds.Tables[0].Rows[x].ItemArray[6].ToString() == "Served" || ds.Tables[0].Rows[x].ItemArray[6].ToString() == "Cancelled") { }
                            else if ((ds.Tables[0].Rows[x].ItemArray[6].ToString() == "prep" || ds.Tables[0].Rows[x].ItemArray[6].ToString() == "Prep") && ds.Tables[0].Rows[x].ItemArray[8].ToString() == "Pica 1" || ds.Tables[0].Rows[x].ItemArray[8].ToString() == "Breakfast" || ds.Tables[0].Rows[x].ItemArray[8].ToString() == "Entrees" || ds.Tables[0].Rows[x].ItemArray[8].ToString() == "Noodles" || ds.Tables[0].Rows[x].ItemArray[8].ToString() == "Pica1" || ds.Tables[0].Rows[x].ItemArray[8].ToString() == "Pizza" || ds.Tables[0].Rows[x].ItemArray[8].ToString() == "Sizzlers" || ds.Tables[0].Rows[x].ItemArray[8].ToString() == "Specialty Rice")
                            {
                                if (ds.Tables[0].Rows[x].ItemArray[3].ToString().Contains("*"))
                                {
                                    if (this.InvokeRequired)
                                    {

                                        Minitable mini = new Minitable();
                                        mini.settext(label1.Text.ToString());

                                        mon.flowLayoutPanel2.Controls.Add(mini);
                                        mini.insert(ds.Tables[0].Rows[x].ItemArray[2].ToString(), ds.Tables[0].Rows[x].ItemArray[4].ToString(), ds.Tables[0].Rows[x].ItemArray[3].ToString(), ds.Tables[0].Rows[x].ItemArray[5].ToString(), ds.Tables[0].Rows[x].ItemArray[6].ToString(), "", "", ds.Tables[0].Rows[x].ItemArray[0].ToString(), ds.Tables[0].Rows[x].ItemArray[1].ToString());

                                    }
                                    else
                                    {

                                        Minitable mini = new Minitable();
                                        mini.settext(label1.Text.ToString());
                                        mon.flowLayoutPanel2.Controls.Add(mini);
                                        mini.insert(ds.Tables[0].Rows[x].ItemArray[2].ToString(), ds.Tables[0].Rows[x].ItemArray[4].ToString(), ds.Tables[0].Rows[x].ItemArray[3].ToString(), ds.Tables[0].Rows[x].ItemArray[5].ToString(), ds.Tables[0].Rows[x].ItemArray[6].ToString(), "", "", ds.Tables[0].Rows[x].ItemArray[0].ToString(), ds.Tables[0].Rows[x].ItemArray[1].ToString());

                                    }
                                    //mon.addcount(ds.Tables[0].Rows[x].ItemArray[3].ToString().Replace("*", ""), ds.Tables[0].Rows[x].ItemArray[4].ToString());
                                    //  MessageBox.Show(orderid.Text.ToString());
                                    SoundPlayer sd = new SoundPlayer("Add.wav");
                                    sd.Play();


                                }

                                else
                                {
                                    dataGridView1.Rows.Add(ds.Tables[0].Rows[x].ItemArray[2], ds.Tables[0].Rows[x].ItemArray[4], ds.Tables[0].Rows[x].ItemArray[3], ds.Tables[0].Rows[x].ItemArray[5], ds.Tables[0].Rows[x].ItemArray[6], "", "", ds.Tables[0].Rows[x].ItemArray[0], ds.Tables[0].Rows[x].ItemArray[1]);
                                    Console.WriteLine(x.ToString());
                                    if ((ds.Tables[0].Rows[x].ItemArray[6].ToString() == "prep")) { mon.addcount(ds.Tables[0].Rows[x].ItemArray[3].ToString(), ds.Tables[0].Rows[x].ItemArray[4].ToString()); }
                                }
                            }
                        }
                        if (dataGridView1.Rows.Count != 0) {

                            SoundPlayer sd = new SoundPlayer("Untitled.wav");
                            sd.Play();
                        }
                    }
                    catch (Exception x)
                    {
                        Console.WriteLine(x.ToString());
                        dataGridView1.Rows.Clear();
                        goto try_again;
                    }
                    if (dataGridView1.Rows.Count == 0) { mon.serve(label1.Text.ToString(), lblorder.Text.ToString()); this.Parent.Controls.Remove(this); }
                }
            }
            catch (Exception ee) {
                //MessageBox.Show(ee.ToString());
            }

            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.Connection = connection;

           

            if (e.ColumnIndex == 5) {

                if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "prep")
                {
                    mon.subcount(dataGridView1.CurrentRow.Cells[2].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                }

                dataGridView1.CurrentRow.Cells[4].Value = "Cancelled";
                string query = "UPDATE orders SET itemstat ='Cancelled' WHERE id = '" + dataGridView1.CurrentRow.Cells[7].Value.ToString()+ "'";
                command.CommandText = query;
                command.ExecuteNonQuery();

                //query = "UPDATE tables SET itemstat ='Cancelled'  WHERE orderid = '" + dataGridView1.CurrentRow.Cells[8].Value.ToString() + "' AND id = '"+ dataGridView1.CurrentRow.Cells[7].Value.ToString() + "'";
                //command.CommandText = query;
                //command.ExecuteNonQuery();
                //connection.Close();
            }

            if (e.ColumnIndex == 6)
            {

                if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "prep")
                {
                    mon.subcount(dataGridView1.CurrentRow.Cells[2].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                }

                dataGridView1.CurrentRow.Cells[4].Value = "Served";
                string query = "UPDATE orders SET itemstat ='Served' WHERE id = '" + dataGridView1.CurrentRow.Cells[7].Value.ToString() + "'";
                command.CommandText = query;
                command.ExecuteNonQuery();

                //query = "UPDATE tables SET itemstat ='Ready'  WHERE orderid = '" + dataGridView1.CurrentRow.Cells[8].Value.ToString() + "' AND tableid = '" + label1.Text.ToString() + "'";
                //command.CommandText = query;
                //command.ExecuteNonQuery();
                //connection.Close();
            }
            
        }

        public void settext(string test)
        {
            this.label1.Text = test;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                MySqlConnection connection = new MySqlConnection(myConnectionString);
                
                MySqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                panel2.BringToFront();
                connection.Open();
                for (int x = 0; x < dataGridView1.Rows.Count; x++)
                {
                    //MessageBox.Show(dataGridView1.Rows.Count.ToString());
                    if (dataGridView1.Rows[x].Cells[4].Value.ToString() != "Cancelled")
                    {
                        string query = "UPDATE orders SET itemstat ='Served' WHERE id = '" + dataGridView1.Rows[x].Cells[7].Value.ToString() + "'";
                        command.CommandText = query;
                        command.ExecuteNonQuery();

                        if (dataGridView1.Rows[x].Cells[4].Value.ToString() == "prep")
                        {
                            mon.subcount(dataGridView1.Rows[x].Cells[2].Value.ToString(), dataGridView1.Rows[x].Cells[1].Value.ToString());
                        }

                        //query = "UPDATE tables SET itemstat ='Served'  WHERE orderid = '" + dataGridView1.Rows[x].Cells[8].Value.ToString() + "' AND tableid = '" + label1.Text.ToString() + "' AND ";
                        //command.CommandText = query;
                        //command.ExecuteNonQuery();
                    }
                    
                }connection.Close();
                mon.serve(label1.Text.ToString(), lblorder.Text.ToString());
                this.Parent.Controls.Remove(this);
               
            }
            catch (Exception ee) { }
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
    }
}
