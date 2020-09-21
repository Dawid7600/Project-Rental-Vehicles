using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Project_Car_Rental
{
    public partial class Rental : Form
    {
        public Rental()
        {
            InitializeComponent();
            carload();
            rentalload();
        }
        // nawiązanie połączenia z bazą danych
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-V3EPJ8I; Initial Catalog= CarRental;Trusted_Connection=True");
        SqlCommand cmd;
        SqlCommand cmd1;
        SqlDataReader dr;
        string sql;
        string sql1;


        public void carload()
        {   //wybranie wszystkich danych z tabeli CarRegistration, połączenie z bazą
            cmd = new SqlCommand("select * from CarRegistration", con);
            con.Open();
            dr = cmd.ExecuteReader();


            while (dr.Read())
            {
                txtcarid.Items.Add(dr["RegNo"].ToString());
            }

            con.Close();

        }

        public void rentalload()
        {   //wczytanie danych z bazy z tabeli Rental
            sql = "select * from Rental";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6]);


            }

            con.Close();
        }




        private void Rental_Load(object sender, EventArgs e)
        {

        }

        private void txtcarid_SelectedIndexChanged(object sender, EventArgs e)
        {
            //wczytanie wszystkich danych z tabeli CarRegistration, nawiązanie połączenia z bazą
            //jeżeli pojazd jest dostępny, można wpisywać dane, jeżeli nie, jest to zablokowane
            cmd = new SqlCommand("select * from CarRegistration where RegNo = '" + txtcarid.Text + " '", con);
            con.Open();
            dr = cmd.ExecuteReader();


            if (dr.Read())
            {
                string aval;


                aval = dr["Available"].ToString();

                label9.Text = aval;


                if (aval == "No")
                {
                    txtcustid.Enabled = false;
                    txtcustname.Enabled = false;
                    txtfee.Enabled = false;
                    txtdate.Enabled = false;
                    txtdue.Enabled = false;

                }
                else
                {
                    txtcustid.Enabled = true;
                    txtcustname.Enabled = true;
                    txtfee.Enabled = true;
                    txtdate.Enabled = true;
                    txtdue.Enabled = true;





                }

            }
            else
            {
                label9.Text = "Car Not Available.";
            }

            con.Close();




        }

        private void txtcustid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmd = new SqlCommand("select * from Customer where CustomerID = '" + txtcustid.Text + " '", con);
                con.Open();
                dr = cmd.ExecuteReader();


                if (dr.Read())
                {
                    txtcustname.Text = dr["CustomerName"].ToString();

                }
                else
                {
                    MessageBox.Show("Customer ID Not Found");
                }

                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            {
                //przycisk Add, przy wpisaniu danych, zostaną zapisane lub zaaktaulizowane w bazie danych
                string carid = txtcarid.SelectedItem.ToString();
                string custid = txtcustid.Text;
                string custname = txtcustname.Text;
                string fee = txtfee.Text;
                string date = txtdate.Value.Date.ToString("yyyy-MM-dd");
                string due = txtdue.Value.Date.ToString("yyyy-MM-dd");


                sql = "insert into Rental(car_id,cust_id,custname,fee,date,due)values(@car_id,@cust_id,@custname,@fee,@date,@due)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@car_id", carid);
                cmd.Parameters.AddWithValue("@cust_id", custid);
                cmd.Parameters.AddWithValue("@custname", custname);
                cmd.Parameters.AddWithValue("@fee", fee);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@due", due);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Added");

                sql1 = "update CarRegistration set Available = 'No' where RegNo = @RegNo ";
                cmd1 = new SqlCommand(sql1, con);
                cmd1.Parameters.AddWithValue("@RegNo", carid);
                cmd1.ExecuteNonQuery();
                MessageBox.Show("Record Added");
            };

            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {   //odświeżenie danych, wczytanie metod, wyczyszczenie textboxów
            carload();
            rentalload();
            txtcarid.Focus();
            txtcustid.Clear();
            txtcustname.Clear();
            txtfee.Clear();


        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
