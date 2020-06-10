using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;

namespace HoangdinhthienKTPM2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public string fr;
        public string to;
        public string nb;
        SqlConnection conn;
        string connString = ConfigurationManager.ConnectionStrings["lnet"].ToString();
        private DataGridViewRow _row;
        public Form2(DataGridViewRow row)
        {
            InitializeComponent();
            _row = row;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            label4.Text = _row.Cells[2].Value.ToString();
            label5.Text = _row.Cells[3].Value.ToString();
            label6.Text = _row.Cells[5].Value.ToString();
        }
        
      /*  private void Kn()
        {
            conn = new SqlConnection(connString);
            conn.Open();
            string sqlSELECT = "SELECT distinct DepartureAirportID FROM Routers; ";
            SqlCommand cmd = new SqlCommand(sqlSELECT, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            label1.Tag = dt;
            label1.Text = "DepartureAirportID";
            conn.Close();

        }  */
        
        private void label4_Click(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(connString);
            conn.Open();
            string sqlUPDATE = "update show SET Date=@date, Times=@time,EconomyPrice=@ep WHERE DepartureAirportID =@From and ArrivalAirportID =@To";
            SqlCommand cmd = new SqlCommand(sqlUPDATE, conn);
            cmd.Parameters.AddWithValue("From", label4.Text);
            cmd.Parameters.AddWithValue("To", label5.Text);
            cmd.Parameters.AddWithValue("date", dateTimePicker1.Text);
            
            try
            {
                cmd.Parameters.AddWithValue("time", DateTime.Parse(textBox1.Text));
            }
            catch
            {
                MessageBox.Show("Thoi gian ko hop le");
                return;
            }
            cmd.Parameters.AddWithValue("ep", textBox2.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Da cap nhat");
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
