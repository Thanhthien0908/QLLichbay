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
using System.Configuration;
using System.IO;

namespace HoangdinhthienKTPM2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
           
        }
        SqlConnection conn;
        string connString = ConfigurationManager.ConnectionStrings["lnet"].ToString();
    

        private bool Kiemtratrung(string FlightNumber, string Date)
        {
            string query = "Select * from  Schedules  where FlightNumber=@FlightNumber and Date=@Date";
            conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("FlightNumber", FlightNumber);
            cmd.Parameters.AddWithValue("Date", Date);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            conn.Close();
            return dt.Rows.Count == 0;


        }
        int trung = 0, thanh_cong = 0, missing = 0;

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            openFileDialog1.FileName = "Chọn một file";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                button1.Enabled = true;
                textBox1.Text = openFileDialog1.FileName;
            }
            else
            {
                button1.Enabled = false;
                textBox1.Text = "";
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string date, time, aircrapt_id, route_id, flight_number, e_price, confirm;
            var allLines = File.ReadAllLines(openFileDialog1.FileName);
            foreach (var line in allLines)
            {
                string[] strS = line.Split(',');
                for (int i = 0; i < strS.Length; i++)
                {
                    date = strS[1];
                    time = strS[2];
                    aircrapt_id = strS[3];
                    route_id = strS[4];//getRoutesID(strS[4]), strS[5]);
                    flight_number = strS[5];
                    e_price = strS[6];
                    confirm = strS[7];
                    if (strS[i] == "" || route_id == null)
                    {
                        missing++;
                        break;
                    }
                    else
                    {
                        string query = null;
                        if (strS[0] == "ADD")
                        {
                            if (!Kiemtratrung(flight_number, date))
                            {
                                trung++;
                                break;
                            }
                            else
                            {
                                query = "insert into Schedules values('" + date + "','" + time + "','" + aircrapt_id + "','" + route_id + "'," + flight_number + ",'" + e_price + "','" + confirm + "')";

                            }


                        }
                        else if(Kiemtratrung(flight_number, date))
                        {
                            missing++;
                            break;
                        }
                        else
                        {
                            query = "UPDATE Schedules SET Times ='" + time + "', AircraftID = '" + aircrapt_id + "',RouteID = '" + route_id + "',EconomyPrice = '" + e_price + "',Confirmed = '" + confirm + "' WHERE Date = '" + date + "' AND FlightNumber = '" + flight_number + "'";
                        }
                        conn = new SqlConnection(connString);
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            thanh_cong++;
                        }
                        else
                        {
                            missing++;
                        }
                        break;
                    }
                }
            }
            label5.Text = thanh_cong.ToString();
            label9.Text = trung.ToString();
            label10.Text = missing.ToString();
        }

    }
}

