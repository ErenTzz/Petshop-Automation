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
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
            DisplayProduct();
        }
        public static string connection = System.IO.File.ReadAllText(@"C:\Connection.txt");
        SqlConnection Con = new SqlConnection(connection);
        private void DisplayProduct()
        {
            Con.Open();
            string query = "Select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Clear()
        {
            PrNameTb.Text = "";
            CatCb.SelectedIndex = 0;
            QtyTb.Text = "";
            PriceTb.Text = "";
        }
        int Key = 0;
        private void SaveBtn_Click(object sender, EventArgs e)
                {
                    if (PrNameTb.Text == "" || CatCb.SelectedIndex == -1 || QtyTb.Text == "" || PriceTb.Text == "")
                    {
                        MessageBox.Show("Missing Info");
                    }
                    else
                    {
                        try
                        {
                            Con.Open();
                            SqlCommand cmd = new SqlCommand("insert into ProductTbl (PrName,PrCat,PrQty,PrPrice) values (@PN,@PC,@PQ,@PP)", Con);
                            cmd.Parameters.AddWithValue("@PN", PrNameTb.Text);
                            cmd.Parameters.AddWithValue("@PC", CatCb.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@PQ", QtyTb.Text);
                            cmd.Parameters.AddWithValue("@PP", PriceTb.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Product Added");
                            Con.Close();
                            DisplayProduct();
                            Clear();
                        }
                        catch
                        {

                    MessageBox.Show("Please Write Correctly");

                    Con.Close();


                }
            }
                }
        private void label3_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
            obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
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

        private void label2_Click(object sender, EventArgs e)
        {
            Mother obj = new Mother();
            obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Mother obj = new Mother();
            obj.Show();
            this.Hide();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            Mother obj = new Mother();
            obj.Show();
            this.Hide();
        }

        private void Products_Load(object sender, EventArgs e)
        {

        }

        private void ProductDGV_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            PrNameTb.Text = ProductDGV.SelectedRows[0].Cells[1].Value.ToString();
            CatCb.Text = ProductDGV.SelectedRows[0].Cells[2].Value.ToString();
            QtyTb.Text = ProductDGV.SelectedRows[0].Cells[3].Value.ToString();
            PriceTb.Text = ProductDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (PrNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ProductDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Product");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from ProductTbl where PrId = @PrKey", Con);
                    cmd.Parameters.AddWithValue("@PrKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted");
                    Con.Close();
                    DisplayProduct();
                    Clear();
                }
                catch
                {

                    MessageBox.Show("Please Write Correctly");

                    Con.Close();

                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (PrNameTb.Text == "" || CatCb.SelectedIndex == -1 || QtyTb.Text == "" || PriceTb.Text == "")
            {
                MessageBox.Show("Missing Info");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update ProductTbl set PrName=@PN,PrCat=@PC,PrQty=@PQ,PrPrice=@PP where PrId=@PrKey", Con);
                    cmd.Parameters.AddWithValue("@PN", PrNameTb.Text);
                    cmd.Parameters.AddWithValue("@PC", CatCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PQ", QtyTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PriceTb.Text);
                    cmd.Parameters.AddWithValue("@PrKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Edited");
                    Con.Close();
                    DisplayProduct();
                    Clear();
                }
                catch
                {
                    MessageBox.Show("Please Write Correctly");
                    
                    Con.Close();

                }
            }
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

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            Billings obj = new Billings();
            obj.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
