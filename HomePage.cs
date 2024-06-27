using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;




namespace CreateTech
{
    public partial class frmHome : Form
    {
       
        int attempt = 0;
        int seconds = 0;
        int wait = 0;
        int otp = 0;

        private bool see = false;
        public bool login = false;
        public static ArrayList userList = new ArrayList(); // The array list that contains all current users
        public static string email;   // Used to identify which acocunt is logged in currently
        public static string surname; 
        public static string name;
        public static string accountname;
        public static string password;
        public static char saved;
        public static User user;
        public static int prof;
        public frmHome()
        {
            InitializeComponent();
           
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            
            ReadData();  //Acquires the user data from the respective file
            if (login)
            {
                
                new FoodCategory().Show();
                this.Close();
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (validateUser())  // Credentials are correct 
            {
                if(cbxRemember.Checked)
                {
                   if(MessageBox.Show("As much as CreateIT adores our customers, Please be aware that using the Remember Me function will auto-log you in everytime you open the app on your device. This will leave your account vulnerable to unauthorised access if your device is not in your control. You can turn off this feature any time. Would you like to proceed?", "Sensitive Data Warning", MessageBoxButtons.YesNo)==DialogResult.Yes)
                    {
                        GetUser(txtemail.Text).Setsaved('Y');
                        this.Hide();
                        email = txtemail.Text;
                        password = txtpassword.Text;
                        accountname = GetUser(txtemail.Text).Getaccountname();
                        name = GetUser(txtemail.Text).Getname();
                        surname = GetUser(txtemail.Text).Getsurname();
                        saved = GetUser(txtemail.Text).Getsaved();
                        user = GetUser(txtemail.Text);
                        MessageBox.Show($"Greetings {accountname}, Please enjoy your browsing today😊.", "Welcome Message.");
                        txtemail.Clear();
                        txtpassword.Clear(); // ANNNND sensitive Data Gone 😀... well atleast here 
                        new FoodCategory().Show();
                        return;
                    }
                   else
                    {
                        cbxRemember.Checked = false;
                        MessageBox.Show("Revert complete. Do not worry, your data is our priority😎.", "Abort Confirmed");
                        return;
                    }
                 
                }
                Program.email = txtemail.Text;
                email = txtemail.Text;
                password = txtpassword.Text;
                accountname = GetUser(txtemail.Text).Getaccountname();
                name = GetUser(txtemail.Text).Getname();
                surname = GetUser(txtemail.Text).Getsurname();
                saved = GetUser(txtemail.Text).Getsaved();
                user = GetUser(txtemail.Text);
                prof = GetUser(txtemail.Text).Getprof();
                MessageBox.Show($"Greetings {GetUser(txtemail.Text).Getaccountname()}, Please enjoy your browsing today😊.", "Welcome Message.");
                txtemail.Clear();
                txtpassword.Clear(); // ANNNND sensitive Data Gone 😀... well atleast here ANYWAY
                new FoodCategory().Show();
                this.Close();
            }
            else       // Credentials are incorrect
            {
                Console.Beep();
                MessageBox.Show("Invalid Login Credentials.Please Try Again.","Warning.",MessageBoxButtons.OKCancel);
            }
               
          
        }
        private bool validateUser() // User Authentication
        {
            bool password = false;
            bool email = false;
            here:
            if(txtemail.Text==null || txtpassword==null)
            {
                Console.Beep();
                MessageBox.Show("Error,Please fill in the required fields","Incomple Submission.",MessageBoxButtons.OK,MessageBoxIcon.Error);
                goto here;
            }
            else
            {
                foreach( User user in userList)
                {
                    if (txtemail.Text == user.Getemail())
                    {
                        email = true;

                    }
                    if (((txtpassword.Text == user.Getpassword())) && ((txtemail.Text == user.Getemail())))
                    {
                        return true;
                    }
                    
                    if (email &&!password)  // User email is valid, attempt to access password
                    {
                        attempt+=1;
                        if (attempt>= 5)
                        {
                            btnSignIn.Enabled = false;
                            btnCreateAccount.Enabled = false;
                            btnForgotPass.Enabled = false;
                            wait += 60;
                            seconds = wait;
                            lbltimer.Visible = true;
                            failtime.Start();
                            return false;  
                        }
                        return false;
                    }
                   
                }
                return false;
            }
        }
        public void ReadData()  // Acquiring data to be used by CreateIT
        {

            StreamReader sr = new StreamReader("CreateTech.txt");
            string line = sr.ReadLine();
            string[] lines;
            while (line != null)
            {
                lines = line.Split(',');
                string email = lines[0];
                string surname = lines[1];
                string name = lines[2];
                string accountname = lines[3];
                string password = lines[4];
                char saved = char.Parse(lines[5]);
                int prof = int.Parse(lines[6]);
                Object user = new User(email, surname, name, accountname, password,saved,prof);
                userList.Add(user);
                if(saved=='Y') // This current user is logged in
                {
                    frmHome.email = email;
                    frmHome.name = name;
                    frmHome.password = password;
                    frmHome.saved = saved;
                    frmHome.prof = prof;
                    frmHome.accountname = accountname;
                    frmHome.surname = surname;
                    frmHome.user = (User)user;
                    login = true; // CreateIT will login with this email
                }
               line = sr.ReadLine();
            }
            sr.Close();
        }
        public User GetUser(string wanted)   // A method that is used to complete functionality related to the logged-in user
        {
            foreach(User user in userList)
            {
                if(wanted == user.Getemail())
                {
                    return user;
                }
            }
            return null;
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

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            
            new NewAccount().Show();
            this.Hide();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("CreateTech.txt",false);  // The false is used to rewrite the data in the textfile 
            foreach(User user in userList)
            {
            sw.WriteLine($"{user.Getemail()},{user.Getsurname()},{user.Getname()},{user.Getaccountname()},{user.Getpassword()},{user.Getsaved()},{user.Getprof()}");
            }
            sw.Close();
            this.Close();
        }

        private void pnlBackground_MouseMove(object sender, MouseEventArgs e)
        {
            lbltime.Text = DateTime.Now.ToLocalTime().ToShortTimeString(); // Changes the time
        }

        private void btnForgotPass_Click(object sender, EventArgs e)
        {
            if (GetUser(txtemail.Text) == null)
            {
                MessageBox.Show($"The Account registered under {txtemail.Text} does not exist in CreateIT'S database.Please correct your email or create an account with us.", "Incomple Submission.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtemail.Focus();   
                return;
            }
            else
            {
                Random r = new Random();
                otp = r.Next(0, 9999);
                Program.SendEmail("CreateIT Password Reset Request", txtemail.Text, $"<h1>Good Day {GetUser(txtemail.Text).Getname()}\nWe have recently recieved a request to reset your account, {GetUser(txtemail.Text).Getaccountname()}'s password at {DateTime.Now.ToLocalTime().ToLongTimeString()}.\nThe one time pin is: {otp}.\nIf you did not request this action please contact us immediately at createtechtm@gmail.com</h1>", "The Password Request email has been successfully sent to your email account.\nPlease Login with the unqiue one time pin within 3 hours.");
                GetUser(txtemail.Text).Setpassword(otp.ToString());

                StreamWriter sw = new StreamWriter("CreateTech.txt", false);  // The false is used to rewrite the data in the textfile 
                foreach (User user in userList)
                {
                    sw.WriteLine($"{user.Getemail()},{user.Getsurname()},{user.Getname()},{user.Getaccountname()},{user.Getpassword()},{user.Getsaved()}");
                }
            }
        }
        

        private void failtime_Tick(object sender, EventArgs e)
        {
           
            seconds -= 1;
            lbltimer.Text =$"Please wait for: {seconds.ToString()} seconds.";
            if(seconds<=0)
            {
                attempt = 0;
                btnSignIn.Enabled = true;
                btnCreateAccount.Enabled = true;
                btnForgotPass.Enabled = true;
                lbltimer.Visible = false;
                failtime.Stop();
            }
        }

       
    }
    public class User  // Contains data about each current user
    {
        string email;
        string surname;
        string name;
        string accountname;
        string password;
        char saved;   // Used to check if the user needs to login next time or not
        int prof;
        public User(string email, string surname, string name, string accountname, string password, char saved, int prof)

        {
            this.email = email;
            this.surname = surname;
            this.name = name;
            this.accountname = accountname;
            this.password = password;
            this.saved = saved;
            this.prof = prof;   
        }
        public string Getemail()
        {
            return email;
        }
        public string Getsurname()
        {
            return surname;
        }
        public string Getname()
        {
            return name;
        }
        public string Getaccountname()
        {
            return accountname;
        }
        public string Getpassword()
        {
            return password;
        }
        public char Getsaved()
        {
            return saved;
        }
        public int Getprof()
        {
            return prof;
        }

        public void Setemail(string email)
        {
            this.email = email;
        }
        public void Setsurname(string surname)
        {
            this.surname = surname;
        }
        public void Setname(string name)
        {
            this.name = name;
        }
        public void Setaccountname(string accountname)
        {
            this.accountname = accountname;
        }
        public void Setpassword(string password) // sensitive method
        {
            this.password = password;
        }
        public void Setsaved(char saved) //sensitive method
        {
            this.saved = saved;
        }
        public void Setprof(int prof)
        { this.prof = prof;}
        
    }
}
