using System;
using System.Windows.Forms;

namespace Project_Car_Rental
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {   //tworzenie obiektu
            Car_Registration c = new Car_Registration();
            c.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {   //tworzenie obiektu
            Customer c = new Customer();
            c.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {   //tworzenie obiektu
            Rental r = new Rental();
            r.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {   //tworzenie obiektu
            ReturnCar re = new ReturnCar();
            re.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {   //zamknięcie okienkta
            this.Close();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
