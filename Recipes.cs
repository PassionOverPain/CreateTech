using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
namespace CreateTech
{
    public partial class Recipes : Form
    {
        public Recipes()
        {
            InitializeComponent();
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

        private void backbtn_Click(object sender, EventArgs e)
        {
            this.Close();
            new ShoppingCart().Show();
        }

        private void Recipes_MouseMove(object sender, MouseEventArgs e)
        {
            lbltime.Text = DateTime.Now.ToLocalTime().ToShortTimeString(); // Changes the time
        }

        private void profilepic_Click(object sender, EventArgs e)
        {
            pnlDashboard.Visible = true;
            pnlDashboard.Enabled = true;
            pnlDashboard.Width = 234;
            pnlDashboard.Height = 570;
            pnlDashboard.Location = new Point(55, 108);
            //234, 570
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

        private void btnexit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit CreateIT?", "Data Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {


                StreamWriter sw = new StreamWriter("CreateTech.txt", false);  // The false is used to rewrite the data in the textfile 
                foreach (User user in frmHome.userList)
                {
                    sw.WriteLine($"{user.Getemail()},{user.Getsurname()},{user.Getname()},{user.Getaccountname()},{user.Getpassword()},{user.Getsaved()},{user.Getprof()}");
                }
                sw.Close();
                this.Close();
            }
            else
            {
                return;  // Why is this here ? idk good question ¯\_(ツ)_/¯
            }
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            pnlDashboard.Visible = false;
            pnlDashboard.Enabled = false;
        }

       

        private void btnSeek_Click(object sender, EventArgs e)
        {
            if(txtKeywords.Text =="")
            {
                MessageBox.Show("Please enter some ingredients before you proceed.", "Data Notification.", MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtKeywords.Focus();
                return;
            }
            
            Process.Start($"https://foodloversmarket.co.za/recipes/?kw={txtKeywords.Text}&rcat={cbxCategory.Text}&rcn={cbxCuisine.Text}&rcm={cbxCooking.Text}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch(cbxFoods.SelectedIndex)
            {
                case 0:
                    {
                        Process.Start("https://foodloversmarket.co.za/recipes/braai-smores/");
                        break;
                    }
                case 1:
                    {
                        Process.Start("https://foodloversmarket.co.za/recipes/crispy-banana-and-strawberry-puffs/");
                        break;
                    }
                case 2:
                    {
                        Process.Start("https://foodloversmarket.co.za/recipes/classic-lasagne/");
                        break;
                    }
                case 3:
                    {
                        Process.Start("https://foodloversmarket.co.za/recipes/pancake-pasta/");
                        break;
                    }
                case 4:
                    {
                        pnlRecipes.Width = 328;
                        pnlRecipes.Height = 391;
                        pnlRecipes.Visible = true;
                        break;
                    }
              
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pnlRecipes.Visible=false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Recipe Has been successfully saved to CreateIT'S Food Catalogue😋.\nThank you for your contribution.", "Data Notification.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnsettings_Click(object sender, EventArgs e)
        {
            new Settings().Show();
            this.Close();
        }
    }
}
