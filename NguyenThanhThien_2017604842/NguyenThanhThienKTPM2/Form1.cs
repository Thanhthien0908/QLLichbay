using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace HoangdinhthienKTPM2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        string connString = ConfigurationManager.ConnectionStrings["lnet"].ToString();

        private void chuyenmau()
        {
            
            for(int i=0; i<dataGridView2.Rows.Count - 1;i++)
            {
                if (dataGridView2.Rows[i].Cells[9].Value.ToString() == "Cancel Flight")
                 {
                     dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red;                   
                  }
            }
        }

        private void HienthiDL()
        {
            conn = new SqlConnection(connString);
            conn.Open();
            string sqlSELECT = "SELECT * from show;";
            SqlCommand cmd = new SqlCommand(sqlSELECT, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);

            dataGridView2.DataSource = dt;
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                if (dataGridView2.Rows[i].Cells[9].Value.ToString() == "Cancel Flight")
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
            conn.Close();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            HienthiDL();
            Kn();
            Kn1();
            chuyenmau();
            
        }
        private void Kn()
        {
            conn = new SqlConnection(connString);
            conn.Open();
            string sqlSELECT = "SELECT distinct DepartureAirportID FROM Routers; ";
            SqlCommand cmd = new SqlCommand(sqlSELECT, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            comboBox4.DataSource = dt;
            comboBox4.DisplayMember = "DepartureAirportID";
            comboBox4.Text = "";
            conn.Close();
        
        }
        
        private void Kn1()
        {
            conn = new SqlConnection(connString);
            conn.Open();
            string sqlSELECT = "SELECT distinct ArrivalAirportID FROM Routers; ";
            SqlCommand cmd = new SqlCommand(sqlSELECT, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            comboBox5.DataSource = dt;
            comboBox5.DisplayMember = "ArrivalAirportID";
            
            comboBox5.Text = "";
            conn.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sqlSELECT;
            string sqlSELECT3;
            string sqlSELECT4;
            string sqlSELECT6;
            string sqlSELECT5;
            string sqlSELECT7;
            string sqlSELECT8;
            string sqlSELECT2;
            if(comboBox4.Text != "" && comboBox4.Text==comboBox5.Text)
            {
                MessageBox.Show("Nhập lại nơi đi và đến");
            }
            if (textBox4.Text == "")
            {   
                if(comboBox5.Text == "" && comboBox4.Text=="")
                {
                    if (comboBox6.Text == "Economy Price")
                    { sqlSELECT8 = "Select * from show where Date=@date order by EconomyPrice ASC;"; }
                    else if(comboBox6.Text=="Confirm")
                    { sqlSELECT8 = "Select * from show where Date=@date and Confirmed='Confirm Flight' "; }
                    else { sqlSELECT8 = "Select * from show where Date=@date order by Date,Times;"; }
                    SqlCommand cmd8 = new SqlCommand(sqlSELECT8, conn);
                    
                    cmd8.Parameters.AddWithValue("date", dateTimePicker1.Text);
                    SqlDataReader dr8 = cmd8.ExecuteReader();
                    DataTable dt8 = new DataTable();
                    dt8.Load(dr8);
                    dataGridView2.DataSource = dt8;
                }
                else if (comboBox5.Text == "")
                {
                    if (comboBox6.Text == "Economy Price")
                    { sqlSELECT3 = "Select * from show where Date=@date and DepartureAirportID =@From order by EconomyPrice ASC;"; }
                    else if (comboBox6.Text == "Confirm")
                    { sqlSELECT3 = "Select * from show where Date=@date and DepartureAirportID =@From and Confirmed='Confirm Flight';"; }
                    else { sqlSELECT3 = "Select * from show where Date=@date and DepartureAirportID =@From order by Date,Times;"; }
                    SqlCommand cmd3 = new SqlCommand(sqlSELECT3, conn);
                    cmd3.Parameters.AddWithValue("From", comboBox4.Text);
                    cmd3.Parameters.AddWithValue("date", dateTimePicker1.Text);
                    SqlDataReader dr3 = cmd3.ExecuteReader();
                    DataTable dt3 = new DataTable();
                    dt3.Load(dr3);
                    dataGridView2.DataSource = dt3;
                }
                else if (comboBox4.Text == "")
                {
                    if (comboBox6.Text == "Economy Price")
                    {
                        sqlSELECT4 = "Select * from show where Date=@date and ArrivalAirportID =@To order by EconomyPrice ASC;";
                    }
                    else if (comboBox6.Text == "Confirm")
                    { sqlSELECT4 = "Select * from show where Date=@date and ArrivalAirportID =@To and Confirmed='Confirm Flight';"; }
                    else { sqlSELECT4 = "Select * from show where Date=@date and ArrivalAirportID =@To order by Date,Times;"; }

                    SqlCommand cmd4 = new SqlCommand(sqlSELECT4, conn);
                    cmd4.Parameters.AddWithValue("date", dateTimePicker1.Text);
                    cmd4.Parameters.AddWithValue("To", comboBox5.Text);
                    SqlDataReader dr4 = cmd4.ExecuteReader();
                    DataTable dt4 = new DataTable();
                    dt4.Load(dr4);
                    dataGridView2.DataSource = dt4;
                }
                else
                {
                    if (comboBox6.Text == "Economy Price")
                    {
                        sqlSELECT = "Select * from show where Date=@date and DepartureAirportID =@From and ArrivalAirportID =@To order by EconomyPrice ASC;";
                    }
                    else if (comboBox6.Text == "Confirm")
                    { sqlSELECT = "Select * from show where Date=@date and DepartureAirportID =@From and ArrivalAirportID =@To  and Confirmed='Confirm Flight';"; }
                    else
                    {
                        sqlSELECT = "Select * from show where Date=@date and DepartureAirportID =@From and ArrivalAirportID =@To order by Date,Times;";
                    }
                    SqlCommand cmd = new SqlCommand(sqlSELECT, conn);
                    cmd.Parameters.AddWithValue("date", dateTimePicker1.Text);
                    cmd.Parameters.AddWithValue("From", comboBox4.Text);
                    cmd.Parameters.AddWithValue("To", comboBox5.Text);
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView2.DataSource = dt;
                }
            }
            else
            {
                if (comboBox5.Text == "" && comboBox4.Text == "")
                {
                    if (comboBox6.Text == "Economy Price")
                    { sqlSELECT2 = "Select * from show whereFlightNumber=@nb and  Date=@date order by EconomyPrice ASC;"; }
                    else if (comboBox6.Text == "Confirm")
                    { sqlSELECT2 = "Select * from show where FlightNumber=@nb and Date=@date  and Confirmed='Confirm Flight';"; }
                    else { sqlSELECT2 = "Select * from show where FlightNumber=@nb and Date=@date order by Date,Times;"; }
                    SqlCommand cmd2 = new SqlCommand(sqlSELECT2, conn);
                    cmd2.Parameters.AddWithValue("nb", textBox4.Text);
                    cmd2.Parameters.AddWithValue("date", dateTimePicker1.Text);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    DataTable dt2 = new DataTable();
                    dt2.Load(dr2);
                    dataGridView2.DataSource = dt2;
                }
                else if (comboBox5.Text == "")
                {
                    if (comboBox6.Text == "Economy Price")
                    { sqlSELECT5 = "Select * from show where FlightNumber=@nb and Date=@date and DepartureAirportID =@From order by EconomyPrice ASC;"; }
                    else if (comboBox6.Text == "Confirm")
                    { sqlSELECT5 = "Select * from show where FlightNumber=@nb and Date=@date and DepartureAirportID =@From and Confirmed='Confirm Flight' ;"; }
                    else { sqlSELECT5 = "Select * from show where FlightNumber=@nb and Date=@date and DepartureAirportID =@From order by Date,Times;"; }
                    SqlCommand cmd5 = new SqlCommand(sqlSELECT5, conn);
                    cmd5.Parameters.AddWithValue("From", comboBox4.Text);
                    cmd5.Parameters.AddWithValue("date", dateTimePicker1.Text);
                    cmd5.Parameters.AddWithValue("nb", textBox4.Text);
                    SqlDataReader dr5 = cmd5.ExecuteReader();
                    DataTable dt5 = new DataTable();
                    dt5.Load(dr5);
                    dataGridView2.DataSource = dt5;
                }
                else if (comboBox4.Text == "")
                {
                    if (comboBox6.Text == "Economy Price")
                    {
                        sqlSELECT6 = "Select * from show where FlightNumber=@nb and Date=@date and ArrivalAirportID =@To order by EconomyPrice ASC;";
                    }
                    else if (comboBox6.Text == "Confirm")
                    { sqlSELECT6 = "Select * from show where  FlightNumber=@nb and Date=@date and ArrivalAirportID =@To and Confirmed='Confirm Flight' ;"; }
                    else { sqlSELECT6 = "Select * from show where  FlightNumber=@nb and Date=@date and ArrivalAirportID =@To order by Date,Times;"; }

                    SqlCommand cmd6 = new SqlCommand(sqlSELECT6, conn);
                    cmd6.Parameters.AddWithValue("date", dateTimePicker1.Text);
                    cmd6.Parameters.AddWithValue("To", comboBox5.Text);
                    cmd6.Parameters.AddWithValue("nb", textBox4.Text);
                    SqlDataReader dr6 = cmd6.ExecuteReader();
                    DataTable dt6 = new DataTable();
                    dt6.Load(dr6);
                    dataGridView2.DataSource = dt6;
                }
                else
                {
                    if (comboBox6.Text == "Economy Price")
                    {
                        sqlSELECT7 = "Select * from show where FlightNumber=@nb and Date=@date and DepartureAirportID =@From and ArrivalAirportID =@To order by EconomyPrice ASC;";
                    }
                    else if (comboBox6.Text == "Confirm")
                    {
                        sqlSELECT7 = "Select * from show where  FlightNumber=@nb  and Date=@date and DepartureAirportID =@From and ArrivalAirportID =@To and Confirmed='Confirm Flight';";
                    }
                    else
                    {
                        sqlSELECT7 = "Select * from show where  FlightNumber=@nb  and Date=@date and DepartureAirportID =@From and ArrivalAirportID =@To order by Date,Times;";
                    }
                    SqlCommand cmd7 = new SqlCommand(sqlSELECT7, conn);
                    cmd7.Parameters.AddWithValue("date", dateTimePicker1.Text);
                    cmd7.Parameters.AddWithValue("From", comboBox4.Text);
                    cmd7.Parameters.AddWithValue("To", comboBox5.Text);
                    cmd7.Parameters.AddWithValue("nb", textBox4.Text);
                    SqlDataReader dr7 = cmd7.ExecuteReader();
                    DataTable dt7 = new DataTable();
                    dt7.Load(dr7);
                    dataGridView2.DataSource = dt7;
                }

            }

            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                if (dataGridView2.Rows[i].Cells[9].Value.ToString() == "Cancel Flight")
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
            conn.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open(); conn = new SqlConnection(connString);
            conn.Open();
            string sqlUPDATE = "update show SET Confirmed=@cf WHERE FlightNumber=@nb and DepartureAirportID =@From and ArrivalAirportID =@To";
            SqlCommand cmd = new SqlCommand(sqlUPDATE, conn);
            cmd.Parameters.AddWithValue("From", dataGridView2.CurrentRow.Cells[2].Value);
            cmd.Parameters.AddWithValue("To", dataGridView2.CurrentRow.Cells[3].Value);
            cmd.Parameters.AddWithValue("nb", dataGridView2.CurrentRow.Cells[4].Value);
            cmd.Parameters.AddWithValue("cf", "Cancel Flight");

            cmd.ExecuteNonQuery();
            HienthiDL();
            conn.Close();

        }
        //public string term;
        private void button4_Click(object sender, EventArgs e)
        {
            var selectedROW = dataGridView2.CurrentRow;
            Form2 form = new Form2(selectedROW);
            form.ShowDialog();
            //form.fr = comboBox4.Text;
            //form.to = comboBox5.Text;
            //form.nb = textBox4.Text;
            
            HienthiDL();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            HienthiDL();
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                if (dataGridView2.Rows[i].Cells[9].Value.ToString() == "Cancel Flight")
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            conn.Open(); conn = new SqlConnection(connString);
            conn.Open();
            string sqlUPDATE = "update show SET Confirmed=@cf WHERE FlightNumber=@nb and DepartureAirportID =@From and ArrivalAirportID =@To";
            SqlCommand cmd = new SqlCommand(sqlUPDATE, conn);
            cmd.Parameters.AddWithValue("From", comboBox4.Text);
            cmd.Parameters.AddWithValue("To", comboBox5.Text);
            cmd.Parameters.AddWithValue("nb", textBox4.Text);
            cmd.Parameters.AddWithValue("cf", "Confirm Flight");

            cmd.ExecuteNonQuery();
            HienthiDL();
            conn.Close();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r;
            r = e.RowIndex;
            if (dataGridView2.Rows[r].Cells[9].Value.ToString() == "Cancel Flight")
            {

                btnCofirm.Visible = true;
                button3.Visible = false;

            }
            else
            {
                btnCofirm.Visible = false;
                button3.Visible = true;
            }
        }

        private void btnCofirm_Click(object sender, EventArgs e)
        {
            conn.Open(); conn = new SqlConnection(connString);
            conn.Open();
            string sqlUPDATE = "update show SET Confirmed=@cf WHERE FlightNumber=@nb and DepartureAirportID =@From and ArrivalAirportID =@To";
            SqlCommand cmd = new SqlCommand(sqlUPDATE, conn);
            cmd.Parameters.AddWithValue("From", dataGridView2.CurrentRow.Cells[2].Value);
            cmd.Parameters.AddWithValue("To", dataGridView2.CurrentRow.Cells[3].Value);
            cmd.Parameters.AddWithValue("nb", dataGridView2.CurrentRow.Cells[4].Value);
            cmd.Parameters.AddWithValue("cf", "Confirm Flight");

            cmd.ExecuteNonQuery();
            HienthiDL();
            conn.Close();
            
            
            
        }

        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // int x = comboBox4.SelectedIndex;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.ShowDialog();
            HienthiDL();
        }
    }
}
