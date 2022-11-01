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
    public partial class Billings : Form
    {
        public Billings()
        {
            InitializeComponent();
            EmpNameLbl.Text = Login.Sellername;
            GetCustomers();
            DisplayProduct();
            DisplayTransactions();
            GetBasketList();
        }


        int selProductId = 0, selProductAmount = 0, GrdTotal = 0, Key = 0, Stock = 0;

        public static string connection = System.IO.File.ReadAllText(@"C:\Connection.txt");
        SqlConnection Con = new SqlConnection(connection);
        public static string Sellername = "";

        

        private void GetBasketList()
        {


            Con.Open();
            string query = "Select BId,ProdId[ProdId],BName[ProdName],StockA[Stock],SellA[SellAmt],BTotal[Total] from BasketTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            
            BillDGV.DataSource = ds.Tables[0];
            Con.Close();

            

        }
       
        

        private void GetCustomers()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select CustId from CustomerTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustId", typeof(int));
            dt.Load(Rdr);
            CustIdCb.ValueMember = "CustId";
            CustIdCb.DataSource = dt;
            Con.Close();
        }

        private void DisplayProduct()
        {
            Con.Open();
            string query = "Select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void DisplayTransactions()
        {
            Con.Open();
            string query = "Select * from BillTbl ORDER BY BNum DESC";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TransactionsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void GetCustName()
        {
            Con.Open();
            string query = "Select * from CustomerTbl where CustId='" + CustIdCb.SelectedValue.ToString()+ "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                CustNameTb.Text = dr["CustName"].ToString();
            }
            Con.Close();
        }
        public int NewQty = 0;
        public void UpdateStock()
        {
            try
            {
                int NewQty = Stock - Convert.ToInt32(ProductQ.Text);
                Con.Open();
                SqlCommand cmd = new SqlCommand("Update ProductTbl set PrQty=@PQ where PrId=@PKey", Con);
                cmd.Parameters.AddWithValue("@PQ", NewQty);
                cmd.Parameters.AddWithValue("@PKey", Key);

                //MessageBox.Show("Product Updated");

                cmd.ExecuteNonQuery();
                Con.Close();
                DisplayProduct();
                //Clear();
                
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
        }


        private void AddBillBtn_Click(object sender, EventArgs e)

        {
            String value = ProductQ.Text;
            Boolean control = true;
            Char tmp;
            for (int i = 0; i < ProductQ.Text.Length; i++)
            {
                tmp = value[i];
                if (Char.IsDigit(tmp))
                {
                    
                }
                else control = false;
            }
            if (control)
            {
                
            
            if (ProductQ.Text == "" || Convert.ToInt32(ProductQ.Text) > Stock)
            {
                MessageBox.Show("No Enough In House");
            }
            else if (ProductQ.Text == "" || Key == 0)
            {
                MessageBox.Show("Missing Info");
            }
            else if (ProductQ.Text == "")
            {

            }
            else
            {
                int total = Convert.ToInt32(ProductQ.Text) * Convert.ToInt32(PrPriceTb.Text);
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into BasketTbl (ProdId,SellA,StockA,BName,BTotal) values(@PI,@SellA,@StockA,@BN,@BT)", Con);
                cmd.Parameters.AddWithValue("@PI", selProductId);
                cmd.Parameters.AddWithValue("@SellA", ProductQ.Text);
                cmd.Parameters.AddWithValue("@StockA", selProductAmount);
                cmd.Parameters.AddWithValue("@BN", PrNameTb.Text);
                cmd.Parameters.AddWithValue("@BT", total);
                cmd.ExecuteNonQuery();
                Con.Close();

                GrdTotal += total;

                TLlb.Text = GrdTotal.ToString() + " TL";

                GetBasketList();
                // UpdateStock();
                Reset();
            }
        }
            else { MessageBox.Show("Please Write Correclty"); }
        }






        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void Billings_Load(object sender, EventArgs e)
        {
            EmpNameLbl.Text = Login.Sellername;
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void CustIdCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CustIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCustName();
        }
        

        

        private void BillsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void InsertBill()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into BillTbl (BDate,CustId,CustName,EmpName,Amt) values(@BD,@CI,@CN,@EN,@Am)", Con);
                cmd.Parameters.AddWithValue("@BD", DateTime.Today.Date);
                cmd.Parameters.AddWithValue("@CI", CustIdCb.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                cmd.Parameters.AddWithValue("@EN", EmpNameLbl.Text);
                cmd.Parameters.AddWithValue("@Am", GrdTotal);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Bill Saved");
                Con.Close();
                DisplayTransactions();
                //Clear();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
        }
        //string prodname;
        private void PrintBtn_Click(object sender, EventArgs e)
        {
           

            Con.Open();
            for (int i =0; i< BillDGV.Rows.Count-1; i++)
            {
                int prodId = Convert.ToInt32(BillDGV.Rows[i].Cells[1].Value);
                int sellAmount = Convert.ToInt32(BillDGV.Rows[i].Cells[4].Value);

                int prodAmonut = Convert.ToInt32(BillDGV.Rows[i].Cells[3].Value);

                var newAmount = prodAmonut - sellAmount;
              

                SqlCommand cmd = new SqlCommand("update ProductTbl set PrQty="+ newAmount + ""+ " where PrId="+ prodId, Con);
               
                cmd.ExecuteNonQuery();
              



            }
            Con.Close();

            InsertBill();

            BasketClear();

            DisplayProduct();

            //printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            //if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    printDocument1.Print();
            //}

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        //int prodid, prodqty, prodprice, tottal, pos = 60;

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
            BasketClear();


        }

        private void TransactionsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
            BasketClear();

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           //e.Graphics.DrawString("My Petshop", new Font("Unispace", 15, FontStyle.Bold), Brushes.Red, new Point(80));
           //e.Graphics.DrawString("ID  PRODUCT PRICE QUANTITY TOTAL", new Font("Unispace", 10, FontStyle.Bold), Brushes.Red, new Point(26,40));
           //foreach (DataGridViewRow row in BillDGV.Rows)
           //{
           // //prodid = Convert.ToInt32(row.Cells[""].Value);
           // prodname = "" + row.Cells[""].Value;
           // prodprice = Convert.ToInt32(row.Cells[""].Value);
           // prodqty = Convert.ToInt32(row.Cells[""].Value);
           // tottal = Convert.ToInt32(row.Cells[""].Value);
           //
           //    //e.Graphics.DrawString(""+ prodid, new Font("Unispace", 8, FontStyle.Bold), Brushes.Blue, new Point(26,pos));
           //    e.Graphics.DrawString(""+ prodname, new Font("Unispace", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
           //    e.Graphics.DrawString(""+ prodprice, new Font("Unispace", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
           //    e.Graphics.DrawString(""+ prodqty, new Font("Unispace", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
           //    e.Graphics.DrawString(""+ tottal, new Font("Unispace", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
           //    pos = pos + 20;
           //
           //
           //}
           //e.Graphics.DrawString("Grand Total: Tl" + GrdTotal, new Font("Unispace", 12, FontStyle.Bold), Brushes.Crimson, new Point(50,pos+50));
           //e.Graphics.DrawString("*********MyPetshop**********" , new Font("Unispace", 8, FontStyle.Bold), Brushes.Crimson, new Point(10, pos+85));
           //BillDGV.Rows.Clear();
           //BillDGV.Refresh();
           //pos = 100;
           //GrdTotal = 0;
           //n = 0;
            



        }

        private void Reset()
        {
            PrNameTb.Text = "";
            PrPriceTb.Text = "";
            ProductQ.Text = "";
            Stock = 0;
            Key = 0;
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

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

        private void BasketClearBtn_Click(object sender, EventArgs e)
        {
            BasketClear();


        }
       private void  BasketClear() {
            Con.Open();
            SqlCommand cmd = new SqlCommand("delete from BasketTbl", Con);


            GrdTotal =0;
            TLlb.Text =GrdTotal+" TL";







            cmd.ExecuteNonQuery();
            Con.Close();
            

            GetBasketList();
        }

        private void TLlb_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
        }

        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("ERENPETSHOP", new Font("Unispace", 25, FontStyle.Bold), Brushes.Red, new Point(230));
            e.Graphics.DrawString("Bill ID:" + TransactionsDGV.SelectedRows[0].Cells[0].Value.ToString(), new Font("Unispace", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 70));
            e.Graphics.DrawString("Seller Name:" + TransactionsDGV.SelectedRows[0].Cells[4].Value.ToString(), new Font("Unispace", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 100));
            e.Graphics.DrawString("Date:" + TransactionsDGV.SelectedRows[0].Cells[1].Value.ToString(), new Font("Unispace", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 130));
            e.Graphics.DrawString("Total Amount:" + TransactionsDGV.SelectedRows[0].Cells[5].Value.ToString(), new Font("Unispace", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 160));
            e.Graphics.DrawString("My Petshop", new Font("Unispace", 20, FontStyle.Italic), Brushes.Red, new Point(270, 230));
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void Quitlb_Click(object sender, EventArgs e)
        {
            Application.Exit();
            BasketClear();

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            BasketClear();

        }

        private void panel5_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void EmpNameLbl_Click(object sender, EventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {

        }
        private void ProductsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selProductId= Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[0].Value);
            selProductAmount = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[3].Value);
            PrNameTb.Text = ProductsDGV.SelectedRows[0].Cells[1].Value.ToString();

            //Catcb.Text = ProductsDGV.SelectedRows[0].Cells[2].Value.ToString();
            Stock = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[3].Value.ToString());
            PrPriceTb.Text = ProductsDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (PrNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[3].Value.ToString());
            }


        }
    }

}
