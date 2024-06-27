using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace CreateTech
{
    public partial class Carbohydrates : Form
    {
        ArrayList foods = new ArrayList();
        public Carbohydrates()
        {
            InitializeComponent();
            FoodList.ReadData(foods);
            lblAccountName.Text = frmHome.accountname;
            lblname.Text = frmHome.name;
            lblsurname.Text = frmHome.surname;
            lblemail.Text = frmHome.email;
            if (frmHome.saved == 'Y')
            {
                lblremember.Text = "Yes";

            }
            else
                lblremember.Text = "No";
            Program.SetPro(profilepic, pros);
        }

        private void backbtnicon_Click(object sender, EventArgs e)
        {
            new FoodCategory().Show();
            this.Close();
           

        }

        private void pnlBackground_MouseMove(object sender, MouseEventArgs e)
        {
            lbltime.Text = DateTime.Now.ToLocalTime().ToShortTimeString(); // Changes the time
        }

        private void pnlBread_Click(object sender, EventArgs e)
        {
           FoodList.Purchase(FoodList.GetFood("Bread",foods),foods); 
        }

        private void pnlCereal_Click(object sender, EventArgs e)
        {
            FoodList.Purchase(FoodList.GetFood("Cereal", foods), foods);
        }

        private void pnlFlour_Click(object sender, EventArgs e)
        {
            FoodList.Purchase(FoodList.GetFood("Flour", foods), foods);
        }

        private void pnlPasta_Click(object sender, EventArgs e)
        {
            FoodList.Purchase(FoodList.GetFood("Pasta", foods), foods);
        }

        private void pnlNuts_Click(object sender, EventArgs e)
        {
            FoodList.Purchase(FoodList.GetFood("Nuts", foods), foods);
        }

        private void profilepic_Click(object sender, EventArgs e)
        {
            pnlDashboard.Visible = true;
            pnlDashboard.Enabled = true;
            pnlDashboard.Width = 234;
            pnlDashboard.Height = 570;
            pnlDashboard.Location = new Point(57, 76);
            //234, 570
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            pnlDashboard.Visible = false;
            pnlDashboard.Enabled = false;
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you sure you want to exit CreateIT ?", "Exit Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Exit Aborted, Please enjoy browsering CreateIT's catalogue", "Exit Confirmation");
            }
        }

       

        private void btnRemember_Click(object sender, EventArgs e)
        {
            if (frmHome.saved == 'Y')
            {
                frmHome.user.Setsaved('N');
                frmHome.saved = 'N';
                lblremember.Text = "No";
            }
            else
            {
                if ((MessageBox.Show("As much as CreateIT adores our customers, Please be aware that using the Remember Me function will auto-log you in everytime you open the app on your device. This will leave your account vulnerable to unauthorised access if your device is not in your control. You can turn off this feature any time. Would you like to proceed?", "Sensitive Data Warning", MessageBoxButtons.YesNo) == DialogResult.Yes))
                {
                    frmHome.user.Setsaved('Y');
                    frmHome.saved = 'Y';
                    lblremember.Text = "Yes";
                }
                else
                {
                    MessageBox.Show("Remember me functionality aborted🔓.", "Abort Confirmation❌");
                }
            }
        }

        private void btnnewaccount_Click(object sender, EventArgs e)
        {
            new NewAccount().Show();
            this.Close();
        }

        private void btnrecipes_Click(object sender, EventArgs e)
        {
            new Recipes().Show();
            this.Close();
        }

        private void btnshoppingcart_Click(object sender, EventArgs e)
        {
            new ShoppingCart().Show();
            this.Close();
        }

        private void btnexit_Click_1(object sender, EventArgs e)
        {
            pnlDashboard.Visible = false;
            pnlDashboard.Enabled = false;
        }
    }
}
