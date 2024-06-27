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
using WMPLib;

namespace CreateTech
{
    public partial class FoodCategory : Form
    {
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        bool play = false;
        public FoodCategory()
        {
            InitializeComponent();
        }
       

        private void backbtnicon_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to return to the sign out? If enabled, the remember me function will be disabled.", "Warning Question", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                frmHome.saved = 'N';
                StreamWriter sw = new StreamWriter("CreateTech.txt", false);  //Writes to the file
                foreach (User user in frmHome.userList)
                {
                    if (user.Getemail() == frmHome.email)
                    {
                        user.Setsaved('N');
                    }
                    sw.WriteLine($"{user.Getemail()},{user.Getsurname()},{user.Getname()},{user.Getaccountname()},{user.Getpassword()},{user.Getsaved()},{user.Getprof()}");
                }
                sw.Close();
                new frmHome().Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Cancelled successfully, enjoy browsing CreateIT's food categories.", "Abort Confirmed.");
            }
        }

        private void pnlCarbohydrates_Click(object sender, EventArgs e)
        {
          
            new Carbohydrates().Show();
            this.Close();
        }

        private void pnlDairy_Click(object sender, EventArgs e)
        {
          
            new Dairy_Products().Show();
            this.Close();
        }

        private void pnlFruits_Click(object sender, EventArgs e)
        {
           
            new Fruits().Show();
            this.Close();
        }

        private void pnlFats_Click(object sender, EventArgs e)
        {
          
            new FatsandOils().Show();
            this.Close();
        }

        private void pnlProtein_Click(object sender, EventArgs e)
        {
            
            new Protien().Show();
            this.Close();
        }

        private void pnlVegetables_Click(object sender, EventArgs e)
        {
           
            new Vegetables().Show();
            this.Close();
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            lbltime.Text = DateTime.Now.ToLocalTime().ToShortTimeString(); // Changes the time
        }

        private void FoodCategory_Activated(object sender, EventArgs e)
        {
            player.URL = "Requim.mp3"; //GET MORE Music 
            player.controls.stop();
            lblAccountName.Text =frmHome.accountname;
            lblname.Text = frmHome.name;
            lblsurname.Text = frmHome.surname;
            lblemail.Text = frmHome.email;
            if (frmHome.saved == 'Y')
            {
                lblremember.Text = "Yes";

            }
            else
                lblremember.Text = "No";
            Program.SetPro(profilepic,pros);
        }

        private void profilepic_Click(object sender, EventArgs e)
        {
            pnlDashboard.Visible = true;
            pnlDashboard.Enabled= true;
            pnlDashboard.Width = 230;
            pnlDashboard.Height = 583;
            //230, 583 ~ panel dashboard size
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pnlDashboard.Visible = false;
            pnlDashboard.Enabled = false;
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

        private void btnshoppingcart_Click(object sender, EventArgs e)
        {
            new ShoppingCart().Show();
            this.Close();
        }

        private void btnrecipes_Click(object sender, EventArgs e)
        {
            new Recipes().Show();
            this.Close();
        }

        private void btnnewaccount_Click(object sender, EventArgs e)
        {
            new NewAccount().Show();
            this.Close();
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

        private void searchicon_Click(object sender, EventArgs e)
        {
            
            pnlStores.Location = new Point(53, 120);
            pnlStores.Width = 280;
            pnlStores.Height = 115;
            pnlStores.Visible = true;
            pnlStores.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text!=null && txtSearch.Text!="Search Something...")
            {


                switch (cbxStores.SelectedIndex)
                {
                    case 0:
                        {
                            Process.Start($"https://www.checkers.co.za/search/all?q={txtSearch.Text}");
                            break;
                        }
                    case 1:
                        {
                            Process.Start($"https://www.pnp.co.za/search/{txtSearch.Text}");
                            break;
                        }
                    case 2:
                        {
                            Process.Start($"https://www.spar.co.za/SpecialPages/Search.aspx?searchtext={txtSearch.Text}&searchmode=anyword");
                            break;
                        }
                    case 3:
                        {
                            Process.Start($"https://www.shoprite.co.za/search/all?q={txtSearch.Text}");
                            break;
                        }
                    case 4:
                        {
                            Process.Start($"https://www.woolworths.co.za/cat?Ntt={txtSearch.Text}&Dy=1");
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("Please select a food store before proceeding to search🔓.", "DataNotification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                }
               
            }
            else
            {
                MessageBox.Show("Please type in a food item before proceeding to search something🔓.", "DataNotification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSearch.Focus();
            }
            pnlStores.Visible = false;
            pnlStores.Enabled = false;

        }

        private void musicPlayer_Click(object sender, EventArgs e)
        {
            if(!play)
            {
                play = true;
                musicPlayer.Image = Properties.Resources.Stop;
                MessageBox.Show("Now playing Requiem drill remix, composed by Mozart and remixed by Blono.\nEnjoy😎🎧🔓.", "Music Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                player.controls.play();

            }
            else
            {
                play = false;
                musicPlayer.Image = Properties.Resources.Play;
                player.controls.stop();
            }
        }

        private void btnsettings_Click(object sender, EventArgs e)
        {
            new Settings().Show();
            this.Close();
        }
    }
}
