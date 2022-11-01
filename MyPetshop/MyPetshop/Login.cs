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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        public static string Sellername = "";
        public static string connection = System.IO.File.ReadAllText(@"C:\Connection.txt");
        SqlConnection Con = new SqlConnection(connection);


        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            //Billings obj = new Billings();
            //obj.Show();
            //this.Hide();
        }

        private void LoginLbl_Click(object sender, EventArgs e)
        {
            //admin girişi
            if (UnameTb.Text == "" || PassTb.Text == "")
            {
                MessageBox.Show("Enter the Username and Password");
            }
            else
            {
                if (RoleCb.SelectedIndex > -1)
                {


                    if (RoleCb.SelectedItem.ToString() == "ADMIN")
                    {
                        if (UnameTb.Text == "admin" && PassTb.Text == "admin")
                        {
                            Mother home = new Mother();
                            home.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Enter the Correct Admin Id and Password");
                        }
                    }
                    else


                    {
                        //Satıcı girişi
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("Select * from EmployeeTbl where EmpName=@username and EmpPass=@password", Con);
                        SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                        cmd.Parameters.AddWithValue("@username", UnameTb.Text);
                        cmd.Parameters.AddWithValue("@password", PassTb.Text);
                        DataSet ds = new DataSet();
                        adapt.Fill(ds);
                        Con.Close();
                        int count = ds.Tables[0].Rows.Count;
                        if (count == 1)
                        {
                            MessageBox.Show("Login Successful!");
                            Sellername = UnameTb.Text;
                            
                            Customers obj = new Customers();
                            obj.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Login Failed!");
                        }
                    }

                }





            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            UnameTb.Text = "";
            PassTb.Text = "";
        }

        private void Quitlb_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
