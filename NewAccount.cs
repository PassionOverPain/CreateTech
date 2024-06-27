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

namespace CreateTech
{
    public partial class NewAccount : Form
    {
        private bool enabled = false;
        private bool see = false;
        int prof = -1;
        public NewAccount()
        {
            InitializeComponent();
        }

        private void btnCalender_Click(object sender, EventArgs e)
        {
            if (enabled == false)    // Enables the calender
            {
                calender.Visible = true;
                calender.Enabled = true;
                btnCalender.Text = "Disable Calender";
                enabled = true;
            }
            else                     // Disables the calender
            {
                calender.Visible = false;
                calender.Enabled = false;
                btnCalender.Text = "Enable Calender";
                enabled = false;
            }
        }

        private void calender_ValueChanged(object sender, EventArgs e)
        {
            txtBirthDate.Text = calender.Value.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to return without completing the sign-up process?", "Warning Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
                new frmHome().Show();
            }
            else
            {
                MessageBox.Show("Your account application details are still pending.", "Abort Confirmed");
            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            if(!IsAllLetters(txtName.Text))
            {
                MessageBox.Show("Error, Your Name cannot consist of numbers or other characters.Please include letters only in the name field", "Data Notification");
                txtName.Focus();
                return;
            }
            if (!IsAllLetters(txtSurname.Text))
            {
                MessageBox.Show("Error, Your Surname cannot consist of numbers or other characters.Please include letters only in the name field", "Data Notification");
                txtSurname.Focus();
                return;
            }
            if (!IsEmailValid(txtemail.Text)) // checks if the email is valid
            {
                MessageBox.Show("Error, Your email address is invalid. Please retype a correct email address.", "Data Notification");
                txtSurname.Focus();
                return;
            }
            if (!IsEmailVerified(txtemail.Text))   // The email is not verified.
                return;

            if (txtName.Text == "" || txtSurname.Text == "" || txtpassword.Text == "" || txtemail.Text == "" || txtBirthDate.Text == "")
            {
                MessageBox.Show("Error, Please complete all fields before submitting your account application.", "Warning");
                return;
            }
            else // Password Validation begins here so brace yourself, it's a mess 
            {
                if (txtpassword.TextLength < 8)  // 8 character password
                {
                    MessageBox.Show("Error, Your password is too short.Please have an 8 or more character password.", "Data Notification");
                    txtpassword.Focus();
                    return;
                }

                bool letters = IsAllLetters(txtpassword.Text);
                bool numbers = IsAllNumbers(txtpassword.Text); 
                bool specialchar = IsOnlyLettersAndNumbers(txtpassword.Text);
         
                if (letters)
                {
                    txtpassword.Focus(); // Sets the password text box to being highlighted/ focused on 
                    MessageBox.Show("Error, Your password does not contain numbers. Please include letters,numbers and special characters(@,#,$,%) within your password.", "Data Notification");
                    return;
                }
                else
                    if (numbers)
                {
                    MessageBox.Show("Error, Your password does not contain letters. Please use a password with numbers, special characters(@,#,$,%) as well as letters.", "Data Notification");
                    txtpassword.Focus();
                    return;
                }
                else
                if (specialchar)
                {
                    MessageBox.Show("Error, Your password does not contain special characters. Please use a password with numbers,special characters(@,#,$,%) as well as letters.", "Data Notification");
                    txtpassword.Focus();
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
                //Valid acount creation
               foreach(User user in frmHome.userList)
                {
                 if(user.Getemail() ==txtemail.Text)
                    {
                        MessageBox.Show($"Error, the email: {txtemail.Text} is already registered under an account.Please choose a different email or reset your password.", "Account creation invalid.",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        txtemail.Focus();
                        return;
                    }
                    if(user.Getaccountname() == txtUserName.Text)
                    {
                        MessageBox.Show($"Error, the account name: {txtUserName.Text} is owned by a CreateIT user.Please choose a different account name.", "Account creation invalid.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUserName.Focus();
                        return;
                    }
                }
                if (MessageBox.Show($"Good day {txtName.Text} {txtSurname.Text},before creating your CreateIT account, please find below the terms of conduct:\nCreateIT is a registered branch of CreateTech Incorporated, while all the assets that you shall interact with were integrated into our system, we do not own any third party system or source software such as and not limited to:Recipes,Online Stores,Digital Artwork and third party information.Even though CreateIT's system algorithm is constructed to cater towards you,The User, user data aquirance complies with various privacy acts.\nYour CreateIT account is interlinked with the CreateIT community so please Adhere to standard online policies.If you encounter any glitches,bugs or system failures along with non complying users, please report this to our help centere createtechtm@gmail.com or +27 172 6953.\nCreateTech may suspend or ban any account or user whom violates the terms of conduct,Do you agree to these terms?.", "Terms of Conduct.",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation) == DialogResult.Yes)
                { 
                User newperson = new User(txtemail.Text, txtSurname.Text, txtName.Text, txtUserName.Text, txtpassword.Text, 'N', prof);
                frmHome.userList.Add(newperson);
                MessageBox.Show($"Thank you for signing up {txtName.Text} {txtSurname.Text}, your account: {txtUserName.Text} has been created✔.", "Account creation confirmation.");
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
                    MessageBox.Show($"\aPlease understand that all our customers are required to agree to the terms of conduct before creating an account thus since you do not agree, your account has not been created.Thank you for your time.", "Account creation confirmation.",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        public static bool IsAllLetters(string password)
        {
            foreach(char c in password)   // Checks if the password is only letters 
            {
                if (!Char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsAllNumbers(string password)  // Checks if the password is only numbers
        {
            foreach(char c in password)
            {
                if(!Char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsOnlyLettersAndNumbers(string password)  // Checks if the password has only letters and numbers, no special characters (@,#,$,%)
        {
            foreach (char c in password)
            {
                if (!Char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsEmailValid(string email)  // Checks if the email has only letters and numbers, as well as @ or .
        {
            foreach (char c in email)
            {
                if (!Char.IsLetterOrDigit(c) && c!='@'&&c!='.')
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsEmailVerified(string email)  // Checks if the email has only letters and numbers, as well as @ or .
        {
            int letters = 0;
            int at = 0;
            int period = 0;
            int numbers = 0;
            foreach (char c in email)
            {
                if (Char.IsLetter(c))
                {
                    ++letters;
                }
                if(c =='@')
                {
                    ++at;
                }
                if (c == '.')
                {
                    ++period;
                }
                else
                    ++numbers;
            }
            if(letters==0) // No letters in email
            {
                MessageBox.Show("Error, Your email does not contain letters.Please retype your email correctly.", "Data Notification");
                return false;
            }
            if ((at!=1)||(period<1))  // No period or at, email standards conflict
            {
                MessageBox.Show("Error, Your email is not valid.Please retype your email correctly.", "Data Notification");
                return false;
            }
            return true;
        }

        private void eye_Click(object sender, EventArgs e)
        {
            if (!see) //enable view password feature 
            {
                txtpassword.UseSystemPasswordChar = false;
                eye.Image = Properties.Resources.closeeye;
                see = true;
            }
            else  // conceal the password 
            {
                txtpassword.UseSystemPasswordChar = true;
                eye.Image = Properties.Resources.eye;
                see = false;
            }
        }

        private void NewAccount_MouseMove(object sender, MouseEventArgs e)
        {
            lbltime.Text = DateTime.Now.ToLocalTime().ToShortTimeString(); // Changes the time
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
           txtName.Clear();
           txtSurname.Clear();
           txtpassword.Clear();
           txtemail.Clear();
           txtBirthDate.Clear();
           txtUserName.Clear();
            
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            prof = 1;
            Program.SetPro(profilepic,prof,profilepic);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            prof = 2;
            Program.SetPro(profilepic, prof, profilepic);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            prof = 3;
            Program.SetPro(profilepic, prof, profilepic);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            prof = 4;
            Program.SetPro(profilepic, prof, profilepic);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            prof = 8;
            Program.SetPro(profilepic, prof, profilepic);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            prof = 7;
            Program.SetPro(profilepic, prof, profilepic);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            prof = 6;
            Program.SetPro(profilepic, prof, profilepic);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            prof = 5;
            Program.SetPro(profilepic, prof, profilepic);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to return without completing the sign-up process?", "Warning Question", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                new frmHome().Show();
            }
            else
            {
                MessageBox.Show("Your account application details are still pending.", "Abort Confirmed");
            }
        }
    }
}
