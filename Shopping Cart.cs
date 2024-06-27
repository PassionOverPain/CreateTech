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
using Spire.Doc;
using Spire.Doc.Documents;

namespace CreateTech
{
    public partial class ShoppingCart : Form
    {
        // ArrayList foodData = new ArrayList();
        public ShoppingCart()
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

        private void backbtnicon_Click(object sender, EventArgs e)
        { 
            new FoodCategory().Show();
            this.Close();
        }

        private void Trolley_MouseMove(object sender, MouseEventArgs e)
        {
            lbltime.Text = DateTime.Now.ToLocalTime().ToShortTimeString(); // Changes the time
        }
       
        public  void ReadData()  // Acquiring data to be used by CreateIT
        {
            txtShoppingCart.Clear();
            double total = 0;
            txtShoppingCart.Text = $"Item\tCategory\t\tQuantity\t\tPrice";
            StreamReader sr = new StreamReader("ShoppingCart.txt");
            string line = sr.ReadLine();
            string[] lines;

            while (line != null)
            {
                lines = line.Split('\t');
                total += Double.Parse(lines[3]);
                txtShoppingCart.AppendText($"\n{lines[0]} {lines[1]}\t\t{lines[2]}\t\t{lines[3]:C2}");
               line = sr.ReadLine();
            }
            sr.Close();
            txtShoppingCart.AppendText($"\n===================================");
            txtShoppingCart.AppendText("");
            txtShoppingCart.AppendText($"\nYour Total is currently calculated as : {total:C2}");
        }

        private void ShoppingCart_Load(object sender, EventArgs e)
        {
            ReadData();
        }

        private void profilepic_Click(object sender, EventArgs e)
        {
            pnlDashboard.Visible = true;
            pnlDashboard.Enabled = true;
            pnlDashboard.Width = 232;
            pnlDashboard.Height = 549;
            pnlDashboard.Location = new Point(17, 120);
            //232, 549
        }

        private void txtSuggest_Click(object sender, EventArgs e)
        {
            new Recipes().Show();
            this.Close();
        }

        private void txtEdit_Click(object sender, EventArgs e)
        {
            new Carbohydrates().Show();
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

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you would like to checkout with these current items?", "Data Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Document reciept = new Document();
                Section section = reciept.AddSection();
                Paragraph paragraph = section.AddParagraph();
                Random r = new Random();
                int number = r.Next(0, 9999);
                paragraph.AppendText($@"This is a CreateIT reciept, generated for {frmHome.accountname} on {DateTime.Now.ToString()}.Please contact CreateIT'S help centre at createtechtm@gmail.com if any of the following details do not match your order specifications.");
                for (int x = 0; x < txtShoppingCart.Lines.Count(); x++)
                {
                    paragraph.AppendText($"{txtShoppingCart.Lines[x]}");
                }
                reciept.SaveToFile($"Reciept{number}.docx", FileFormat.Docx);
            }
        }

        private void btnsettings_Click(object sender, EventArgs e)
        {
            new Settings().Show();
            this.Close();
        }
    }
}
