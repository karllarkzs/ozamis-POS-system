using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;

namespace Kitchen
{
    public partial class Form1 : Form
    {
       //string myConnectionString = "server=192.168.15.6; uid=root; pwd=; database=menu";
        //string myConnectionString = "server=localhost; uid=root; pwd=; database=menu";
        
///        //string myConnectionString = "server=192.168.15.6; uid=root; pwd=; database=menu";
        string myConnectionString = "server=localhost; uid=root; pwd=; database=menu";
        public Form1()
        {
            InitializeComponent();
            txtuser.Focus();

        }

 //string myConnectionString = "server=localhost; uid=root; pwd=; database=menu";
        string myConnectionString = "server=192.168.0.105; uid=roota; pwd=; database=menu";
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Control.CheckForIllegalCrossThreadCalls = false;
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            Thread thread = new Thread(hue);
            //Thread thread2 = new Thread(huehue);
            thread.IsBackground = true;
            //thread2.IsBackground = true;
            thread.Start();
            //thread2.Start();

        }

        public void populateQueue(string table, string id)
        {
            dataGridViewQueue.Rows.Add(table, id);
        }

        delegate void SetTextCallback(string text);
        private void SetText(string text)
        {

            if (this.labelq.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelq.Text = text;
            }
        }
       delegate void SetTextCallback2(string text);
 delegate void SetTextCallback2(string text);

        private void SetText2(string text)
        {

            if (this.orderid.InvokeRequired)
            {
                SetTextCallback2 d = new SetTextCallback2(SetText2);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.orderid.Text = text;
            }
        }

        public void hue()
        {
            int x = 1;
            while (true)
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(myConnectionString);
                    try { connection.Open(); } catch (Exception ee) { MessageBox.Show("Could not connect to database form1. Retrying.");}
                    MySqlCommand command = connection.CreateCommand();
                    command.Connection = connection;
                    string query = "SELECT count, tableid FROM ordercount WHERE id = '1'";
                    command.CommandText = query;
                    MySqlDataReader temp = command.ExecuteReader();
                    temp.Read();
                    SetText2(temp[0].ToString());
                    SetText(temp[1].ToString());
                    connection.Close();

                    
                    Console.WriteLine("Thread restarted "+x+" times");
                    x++;
                }

                catch (Exception ee) { Console.WriteLine("listener failed."+ee.ToString()); }

                Thread.Sleep(200);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Exit App?", "Kitchen Monitor says:", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.ExitThread();
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void labelq_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void addcount(string item, string count) {
            bool found = false;
            if (datagridcount.Rows.Count == 0)
            {
                
                datagridcount.Rows.Add(item, count);
                goto noyeet;
            }
            else if (datagridcount.Rows.Count != 0)
            {

                for (int x = 0; x < datagridcount.Rows.Count; x++) {
                    if (datagridcount.Rows[x].Cells[0].Value.ToString() == item)
                    {
                        //MessageBox.Show(datagridcount.Rows[x].Cells[0].ToString());
                        datagridcount.Rows[x].Cells[1].Value = (int.Parse(datagridcount.Rows[x].Cells[1].Value.ToString()) + int.Parse(count)).ToString();
                        found = true;
                        goto noyeet;
                        //MessageBox.Show(datagridcount.Rows[x].Cells[1].Value.ToString() + " stuff " + count);

                    }
                    else if (x == (datagridcount.Rows.Count - 1)) { goto yeet; }

                }
            }

        yeet:
            datagridcount.Rows.Add(item, count);
        noyeet:
            datagridcount.Sort(datagridcount.Columns[1], ListSortDirection.Descending);


        }

        public void subcount(string item, string count)
        {
          if (datagridcount.Rows.Count != 0)
            {

                for (int x = 0; x < datagridcount.Rows.Count; x++)
                {
                    if (datagridcount.Rows[x].Cells[0].Value.ToString() == item)
                    {
                        //MessageBox.Show(datagridcount.Rows[x].Cells[0].ToString());
                        datagridcount.Rows[x].Cells[1].Value = (int.Parse(datagridcount.Rows[x].Cells[1].Value.ToString()) - int.Parse(count)).ToString();
                        if (datagridcount.Rows[x].Cells[1].Value.ToString() == "0") {
                            datagridcount.Rows.RemoveAt(x);
                        }
                        goto noyeet;
                    }
                    //else if (x == (datagridcount.Rows.Count - 1)) { goto yeet; }

                }
            }
        noyeet:;
            datagridcount.Sort(datagridcount.Columns[1], ListSortDirection.Descending);


        }


        public void serve(string table, string id)
        {
            try
            {
                int x = 0;
                while (true)
                {
                    if (dataGridViewQueue.Rows[x].Cells[0].Value.ToString() == table && dataGridViewQueue.Rows[x].Cells[1].Value.ToString() == id) { dataGridViewQueue.Rows.RemoveAt(x); break; }
                    else { x++; }
                }
            }
            catch (Exception ee) { }
        }

        private void orderid_TextChanged(object sender, EventArgs e)
        {
            if (orderid.Text == "1") { }
            else
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(myConnectionString);
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.Connection = connection;
                    string query = "SELECT tableid FROM ordercount";
                    command.CommandText = query;
                    Object temp = command.ExecuteScalar();
                    SetText(temp.ToString());
                }
                catch (Exception ee)
                {
                    //MessageBox.Show(ee.ToString());
                }
                if (this.InvokeRequired)
                {

                    Table_1 table = new Table_1();
                    table.settext(labelq.Text.ToString());

                    this.flowLayoutPanel1.Controls.Add(table);
                }
                else
                {

                    Table_1 table = new Table_1();
                    table.settext(labelq.Text.ToString());
                    this.flowLayoutPanel1.Controls.Add(table);
                    //  MessageBox.Show(orderid.Text.ToString());
                    Console.WriteLine("helo");



                }
            }
           

        }

        private void datagridcount_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            datagridcount.Sort(datagridcount.Columns[1], ListSortDirection.Descending);
        }
    }
}
