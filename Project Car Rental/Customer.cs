using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Project_Car_Rental
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
            Autono();
            customerload();
        }
        //nawiązanie połączenia z bazą danych
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-V3EPJ8I; Initial Catalog= CarRental;Trusted_Connection=True");
        SqlCommand cmd;
        SqlDataReader dr;
        string proid;
        string sql;
        bool Mode = true;
        string id;

        public void Autono()
        {  
            // połączenie i wczytanie danych z bazy
            sql = "select CustomerID from Customer order by CustomerID desc";
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
                proid = ("00001");
            }
            else
            {
                proid = ("00001");
            }

            txtid.Text = proid.ToString();

            con.Close();

        }

        public void customerload()
        {   //wczytanie danych z bazy z tabelki Customer
            sql = "select * from Customer";
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
        {   //pobranie danych z bazy z tabeli customer
            sql = "select * from Customer where CustomerID = '" + id + "' ";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtid.Text = dr[0].ToString();
                txtname.Text = dr[1].ToString();
                txtaddress.Text = dr[2].ToString();
                txtmobile.Text = dr[3].ToString();

            }
            con.Close();
        }



        private void button1_Click(object sender, EventArgs e)
        {   //przycisk Add, przy wpisaniu danych, zostaną zapisane lub zaaktaulizowane w bazie danych
            string custid = txtid.Text;
            string custname = txtname.Text;
            string address = txtaddress.Text;
            string mobile = txtmobile.Text;

           id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            if (Mode == true)
            {
                sql = "insert into Customer(CustomerID,CustomerName,Address,Mobile)values(@CustomerID,@CustomerName,@Address,@Mobile)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@CustomerID", custid);
                cmd.Parameters.AddWithValue("@CustomerName", custname);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Mobile", mobile);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Added");

                txtname.Clear();
                txtaddress.Clear();
                txtmobile.Clear();
                txtname.Focus();
                

            }
            else

            {
                sql = "update Customer set CustomerName=@CustomerName, Address=@Address, Mobile=@Mobile where CustomerID = @CustomerID";
                con.Open();
                cmd = new SqlCommand(sql, con);

               cmd.Parameters.AddWithValue("@CustomerName", custname);
               cmd.Parameters.AddWithValue("@Address", address);
               cmd.Parameters.AddWithValue("@Mobile", mobile);
               cmd.Parameters.AddWithValue("@CustomerID", id);
               cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated");
               txtid.Enabled = true;
                Mode = true;

                txtname.Clear();
                txtaddress.Clear();
                txtmobile.Clear();
                txtname.Focus();

               



            }

            con.Close();
        }

        private void Customer_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //tabelka, funkcjonalność przycisków Edit i Delete
            if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0)
            {

                Mode = false;
                txtid.Enabled = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getid(id);
            }

            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                sql = "delete from Customer where CustomerID = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted");
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {   //odświeżenie danych, wczytanie metod, wyczyszczenie textboxów
            customerload();
            Autono();
            txtname.Clear();
            txtaddress.Clear();
            txtmobile.Clear();
            txtname.Focus();

        }

        private void button2_Click(object sender, EventArgs e)
        {   //zamknięcie okienka
            this.Close();
        }
    }
}
