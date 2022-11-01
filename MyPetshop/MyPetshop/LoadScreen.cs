using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPetshop
{
    public partial class LoadScreen : Form
    {
        public LoadScreen()
        {
            

            InitializeComponent();
            timer2.Start();

        }
        int startP = 0;
        private void LoadScreen_Load(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void PercentageLbl_Click(object sender, EventArgs e)
        {

        }

        private void MyProgress_progressChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
                      
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            string[] w = new string[4];
            
            w[0] = "For the Best of Your Pets";
            w[1] = "One Roof Many Things";
            w[2] = "Get an Experienced Care for Your Pet";
            w[3] = "A Safe Haven for Pets";
            
            if (MyProgress.Value == 0)
            {
                label3.Text = w[0];
            }
            else if (MyProgress.Value == 25)
            {
                label3.Text = w[1];
            }
            else if (MyProgress.Value == 45)
            {
                label3.Text = w[2];
            }
            else if (MyProgress.Value == 75)
            {
                label3.Text = w[3];
            }
            
            startP += 1;
            if(MyProgress.Value < 100)
            {
                MyProgress.Value = startP;
                PercentageLbl.Text = startP + "%";
            }
           
            if (MyProgress.Value == 100)
            {
                MyProgress.Value = 0;
                Login obj = new Login();
                obj.Show();
                this.Hide();
                timer2.Stop();

            }
        }
    }
}
