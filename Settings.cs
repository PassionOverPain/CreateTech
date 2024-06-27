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
using System.Net;
using System.Net.Mail;
using static System.Net.WebRequestMethods;

namespace CreateTech
{
    public partial class Settings : Form
    {
        int prof = -1;
        bool sent = false;
        public Settings()
        {
            InitializeComponent();
            lblAccountName.Text = frmHome.accountname;
            lblname.Text = frmHome.name;
            lblsurname.Text = frmHome.surname;
            lblemail.Text = frmHome.email;
             ////////
            txtaccountname.Text = frmHome.accountname;
            txtName.Text = frmHome.name;
            txtSurname.Text = frmHome.surname;
            txtemail.Text = frmHome.email;

            if (frmHome.saved == 'Y')
            {
                lblremember.Text = "Yes";

            }
            else
                lblremember.Text = "No";
            Program.SetPro(profilepic, pros);
        }

        private void profilepic_Click(object sender, EventArgs e)
        {
            pnlDashboard.Visible = true;
            pnlDashboard.Enabled = true;
            pnlDashboard.Width = 214;
            pnlDashboard.Height = 533;
            pnlDashboard.Location = new Point(54, 109);
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

        private void backbtn_Click(object sender, EventArgs e)
        {
            new FoodCategory().Show();
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtSurname.Clear();
            txtpassword.Clear();
            txtemail.Clear();
            txtaccountname.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to return without changing your settings?", "Warning Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                new FoodCategory().Show();
            }
            else
            {
                MessageBox.Show("Your account details are still pending.", "Abort Confirmed");
            }
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            prof = 1;
            Program.SetPro(profilepic, prof, profilepic);
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            prof = 2;
            Program.SetPro(profilepic, prof, profilepic);
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            prof = 3;
            Program.SetPro(profilepic, prof, profilepic);
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            prof = 4;
            Program.SetPro(profilepic, prof, profilepic);
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            prof = 5;
            Program.SetPro(profilepic, prof, profilepic);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            prof = 6;
            Program.SetPro(profilepic, prof, profilepic);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            prof = 7;
            Program.SetPro(profilepic, prof, profilepic);
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            prof = 8;
            Program.SetPro(profilepic, prof, profilepic);
        }

        private void Settings_MouseMove(object sender, MouseEventArgs e)
        {
            lbltime.Text = DateTime.Now.ToLocalTime().ToShortTimeString(); // Changes the time
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            foreach (User user in frmHome.userList)
            {
                if (user.Getemail() == txtemail.Text)
                {
                    MessageBox.Show($"Error, the email: {txtemail.Text} is already registered under an account.Please choose a different email or reset your password.", "Account creation invalid.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtemail.Focus();
                    return;
                }
                if (user.Getaccountname() == txtaccountname.Text)
                {
                    MessageBox.Show($"Error, the account name: {txtaccountname.Text} is owned by a CreateIT user.Please choose a different account name.", "Account creation invalid.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtaccountname.Focus();
                    return;
                }
            }
            if (txtpassword.Text != frmHome.password)
            {
                MessageBox.Show("Error,Your password is incorrect.Please fill in the correct password.If you forgot your password, reset it at the the login page.", "Data Notification",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtpassword.Focus();
                return;
            }
           
            if (!NewAccount.IsAllLetters(txtName.Text))
            {
                MessageBox.Show("Error, Your Name cannot consist of numbers or other characters.Please include letters only in the name field", "Data Notification",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtName.Focus();
                return;
            }
            if (!NewAccount.IsAllLetters(txtSurname.Text))
            {
                MessageBox.Show("Error, Your Surname cannot consist of numbers or other characters.Please include letters only in the name field", "Data Notification");
                txtSurname.Focus();
                return;
            }
            if (!NewAccount.IsEmailValid(txtemail.Text)) // checks if the email is valid
            {
                MessageBox.Show("Error, Your email address is invalid. Please retype a correct email address.", "Data Notification");
                txtSurname.Focus();
                return;
            }
            if (!NewAccount.IsEmailVerified(txtemail.Text))   // The email is not verified.
                return;

            if (txtName.Text == "" || txtSurname.Text == "" || txtpassword.Text == "" || txtemail.Text == "" || txtaccountname.Text == "")
            {
                MessageBox.Show("Error, Please complete all fields before saving your information.", "Incomplete Fields Notification",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            else // Password Validation begins here so brace yourself, it's a mess 
            {
                if (txtNewPassword.TextLength < 8)  // 8 character password
                {
                    MessageBox.Show("Error, Your password is too short.Please have an 8 or more character password.", "Data Notification");
                    txtpassword.Focus();
                    return;
                }

                bool letters = NewAccount.IsAllLetters(txtpassword.Text);
                bool numbers = NewAccount.IsAllNumbers(txtpassword.Text);
                bool specialchar = NewAccount.IsOnlyLettersAndNumbers(txtpassword.Text);

                if (letters)
                {
                    txtNewPassword.Focus(); // Sets the password text box to being highlighted/ focused on 
                    MessageBox.Show("Error, Your password does not contain numbers. Please include letters,numbers and special characters(@,#,$,%) within your password.", "Data Notification");
                    return;
                }
                else
                    if (numbers)
                {
                    MessageBox.Show("Error, Your password does not contain letters. Please use a password with numbers, special characters(@,#,$,%) as well as letters.", "Data Notification");
                    txtNewPassword.Focus();
                    return;
                }
                else
                if (specialchar)
                {
                    MessageBox.Show("Error, Your password does not contain special characters. Please use a password with numbers,special characters(@,#,$,%) as well as letters.", "Data Notification");
                    txtNewPassword.Focus();
                    return;
                }

            }
            if (prof < 0)
            {

                MessageBox.Show("Error, Please Select a profile picture before you proceed", "Data Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                profilepic.Focus();
                return;
            }
            else
            {
                //Valid account settings change
                if (!sent)
                {
                    Program.SendEmail("CreateIT Account Settings Change.", txtemail.Text, $"<h1>Good Day {lblname.Text}.                 \nThere has been some changes made to your CreateIT account, {lblAccountName.Text} recently  at {DateTime.Now.ToLocalTime().ToLongTimeString()}..\nIf you did not request this action please contact us immediately at createtechtm@gmail.com</h1>", "An email verification has been sent to you.\nChanges successfully configured.");
                }
                else
                {
                    MessageBox.Show("The changes to your account have been successfully saved.✔", "Data Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                frmHome.user.Setemail(txtemail.Text);
                   frmHome.user.Setsurname(txtSurname.Text);
                   frmHome.user.Setname(txtName.Text);
                   frmHome.user.Setaccountname(txtaccountname.Text);
                   frmHome.user.Setpassword(txtNewPassword.Text);
                   frmHome.user.Setemail(txtemail.Text);
                   

                   StreamWriter sw = new StreamWriter("CreateTech.txt", false);  //Writes to the file
                    foreach (User user in frmHome.userList)
                    {
                      
                      
                        if (user.Getemail() == frmHome.email)
                        {
                        if (cbxRemember.Checked)
                        {
                            user.Setsaved('Y');
                        }
                        else
                            user.Setsaved('N');
                    }
                      
                        sw.WriteLine($"{user.Getemail()},{user.Getsurname()},{user.Getname()},{user.Getaccountname()},{user.Getpassword()},{user.Getsaved()},{user.Getprof()}");
                    }
                    sw.Close();
               
            }
        }
    }
}
