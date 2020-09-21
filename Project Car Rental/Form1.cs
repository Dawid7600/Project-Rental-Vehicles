using System;
using System.Windows.Forms;

namespace Project_Car_Rental
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {   // dane do logowanie Admin:123, 
            string username = txtusername.Text;
            string password = txtpassword.Text;

            if (username.Equals("Admin") && password.Equals("123"))
            {

                Main m = new Main();
                this.Hide();
                m.Show();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {   //zamknięcie okienka
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
