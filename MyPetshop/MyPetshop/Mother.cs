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

namespace MyPetshop
{
    public partial class Mother : Form
    {
        public Mother()
        {
            InitializeComponent();
            CountDogs();
            CountCats();
            CountBirds();
            Finance();
        }

        

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            Billings obj = new Billings();
            obj.Show();
            this.Hide();
        }
        public static string connection = System.IO.File.ReadAllText(@"C:\Connection.txt");
        SqlConnection Con = new SqlConnection(connection);
        private void CountDogs()
        {
            string Cat = "Dog";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ProductTbl where Prcat='" + Cat + "'",Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DogsLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountBirds()
        {
            string Cat = "Bird";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ProductTbl where Prcat='" + Cat + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BirdsLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountCats()
        {
            string Cat = "Cat";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ProductTbl where Prcat='" + Cat + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CatsLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void Finance()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Sum(Amt) from BillTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            FinanceLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void label6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }


        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void panel7_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
        private void label4_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
            obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
            obj.Show();
            this.Hide();
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
            obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Products obj = new Products();
            obj.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Products obj = new Products();
            obj.Show();
            this.Hide();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            Products obj = new Products();
            obj.Show();
            this.Hide();
        }


        private void label5_Click(object sender, EventArgs e)
        {
            Billings obj = new Billings();
            obj.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Billings obj = new Billings();
            obj.Show();
            this.Hide();
        }

        private void Mother_Load(object sender, EventArgs e)
        {

        }
    }
}
