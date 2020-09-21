using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Project_Car_Rental
{
    public partial class ReturnCar : Form
    {
        public ReturnCar()
        {
            InitializeComponent();
            Autono();
            load();
        }
        // nawiązanie połączenia z bazą danych
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-V3EPJ8I; Initial Catalog= CarRental;Trusted_Connection=True");
        SqlCommand cmd;
        SqlDataReader dr;
        string proid;
        string sql;

        bool Mode = true;
        string id;

        public void Autono()
        {   // połączenie i wczytanie danych z bazy
            sql = "select id from ReturnCar order by id desc ";
            cmd = new SqlCommand(sql,con);
            con.Open();
            dr = cmd.ExecuteReader();

            if(dr.Read())
            {
                int id = int.Parse(dr[0].ToString()) + 1;
                proid = id.ToString("00000");
            }
            else if(Convert.IsDBNull(dr))
            {
                proid = ("00000");
            }
            else
            {
                proid = ("00000");
            }

            txtcarid.Text = proid.ToString();

            con.Close();

        }
        public void load()
        {   //wczytanie wszystkich danych z tabeli ReturnCar
            sql = "select * from ReturnCar";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);


            }

            con.Close();
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {


                cmd = new SqlCommand("select car_id,cust_id,custname,fee,date,due,DATEDIFF(dd,due,GETDATE()) as elap from Rental where car_id = '" + txtcarid.Text + "'", con);
                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtcustid.Text = dr["cust_id"].ToString();
                    txtdate.Text = dr["due"].ToString();


                    string elap = dr["elap"].ToString();

                    int elapped = int.Parse(elap);

                    txtelp.Text = (elap);

                    if (elapped > 0)
                    {

                        int fine = elapped * 100;

                        txtfine.Text = fine.ToString();

                    }
                    else
                    {
                        txtfine.Text = "0";
                        txtfine.Text = "0";
                    }


                    con.Close();

                }
               

            }
            con.Close();
        }

        public void getid(String id)
        { //pobranie danych z bazy z tabeli ReturnCar
            sql = "select * from ReturnCar where id = '" + id + "' ";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtcarid.Text = dr[0].ToString();
                txtcustid.Text = dr[1].ToString();
                txtdate.Text = dr[2].ToString();
                txtelp.Text = dr[3].ToString();
                txtfine.Text = dr[4].ToString();

            }
            con.Close();
        }




        private void ReturnCar_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {   //przycisk Add, przy wpisaniu danych, zostaną zapisane w bazie danych
            string carid = txtcarid.Text;
            string custid = txtcustid.Text;
            string date = txtdate.Text;
            string elp = txtelp.Text;
            string fine = txtfine.Text;

            if (Mode == true) { 

            sql = "insert into ReturnCar(car_id,cust_id,date,elp,fine)values(@car_id,@cust_id,@date,@elp,@fine)";
            con.Open();
            cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@car_id", carid);
            cmd.Parameters.AddWithValue("@cust_id", custid);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@elp", elp);
            cmd.Parameters.AddWithValue("@fine", fine);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Added");

            }
            else
            {

            }

            con.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {   //odświeżenie danych, wczytanie metod, wyczyszczenie textboxów
            Autono();
            load();
            txtcarid.Clear();
            txtcustid.Clear();
            txtdate.Clear();
            txtelp.Clear();
            txtfine.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {   //tabelka, funkcjonalność przycisków Edit i Delete
            if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0)
            {

                Mode = false;
                txtcarid.Enabled = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getid(id);
            }

            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                sql = "delete from CarRegistration where regno = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted");
                con.Close();
            }
        }
    }
}
