using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Project_Car_Rental
{
    public partial class Car_Registration : Form
    {
        public Car_Registration()
        {
            InitializeComponent();
            Autono();
            load();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-V3EPJ8I; Initial Catalog= CarRental;Trusted_Connection=True");
        SqlCommand cmd;
        SqlDataReader dr;
        string proid;
        string sql;
        bool Mode = true;
        string id;

        public void Autono()
        {   // połączenie i wczytanie danych z bazy
            sql = "select RegNo from CarRegistration order by RegNo desc";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int id = int.Parse(dr[0].ToString()) + 1;
                proid = id.ToString("00000");
            }
            else if (Convert.IsDBNull(dr))
            {
                proid = ("00000");
            }
            else
            {
                proid = ("00000");
            }
            
            txtregno.Text = proid.ToString();

            con.Close();

        }

        public void load()
        {   //załadowanie danych z bazy danych
            sql = "select * from CarRegistration";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3]);


            }

            con.Close();
        }

        public void getid(String id)
        {   //pobranie danych z bazy danych
            sql = "select * from CarRegistration where regno = '" + id + "' ";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtregno.Text = dr[0].ToString();
                txtbrand.Text = dr[1].ToString();
                txtmodel.Text = dr[2].ToString();
                txtavl.Text = dr[3].ToString();

            }
            con.Close();
        }




        private void Car_Registration_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {   //przycisk Add, przy wpisaniu danych, zostaną zapisane lub zaaktaulizowane w bazie danych 
            string regno = txtregno.Text;
            string brand = txtbrand.Text;
            string model = txtmodel.Text;
            string available = txtavl.SelectedItem.ToString();

            id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            if (Mode == true)
            {
                sql = "insert into CarRegistration(RegNo,Brand,Model,Available)values(@RegNo,@Brand,@Model,@Available)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@regno", regno);
                cmd.Parameters.AddWithValue("@Brand", brand);
                cmd.Parameters.AddWithValue("@Model", model);
                cmd.Parameters.AddWithValue("@Available", available);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Added");

                txtbrand.Clear();
                txtmodel.Clear();
                txtavl.Items.Clear();
                txtbrand.Focus();
                

            }
            else

            {
                sql = "update CarRegistration set brand= @Brand, model=@Model, available=@Available where regno = @regno";
                con.Open();
                cmd = new SqlCommand(sql, con);
                
                cmd.Parameters.AddWithValue("@Brand", brand);
                cmd.Parameters.AddWithValue("@Model", model);
                cmd.Parameters.AddWithValue("@Available", available);
                cmd.Parameters.AddWithValue("@regno", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated");
                txtregno.Enabled = true;
                Mode = true;

                txtbrand.Clear();
                txtmodel.Clear();
                txtavl.Items.Clear();
                txtbrand.Focus();
                


            }

            con.Close();


        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {   //tabelka, funkcjonalność przycisków Edit i Delete
            if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0)
            {

                Mode = false;
                txtregno.Enabled = false;
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

        private void button2_Click(object sender, EventArgs e)
        {   //odświeżenie danych, wczytanie metod, wyczyszczenie textboxów
            load();
            Autono();
            txtbrand.Clear();
            txtmodel.Clear();
            txtbrand.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {   //zamknięcie okienka
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
