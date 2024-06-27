using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Net.Mail;

namespace CreateTech
{
    internal static class Program
    {
    
        public static string email;   // Used to identify which acocunt is logged in currently
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CreateIT());
        }
        public static void SendEmail(string Subject, string Recipient, string Message, string output)
        {

            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("createtechtm@gmail.com");
                    mail.To.Add(Recipient);
                    mail.Subject = Subject;
                    mail.Body = Message;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential("createtechtm@gmail.com", "mjmj woeo ogxb llui"); //Sensitive Data
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                        MessageBox.Show($"{output}", $"{Subject}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}.", "Mail Error.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public static void SetPro(PictureBox pro,PictureBox pros)
        {
           switch(frmHome.prof)
            {
                case 0:
                    {
                        pro.Image = Properties.Resources.Car;
                        break;
                    }
                case 1:
                    {
                        pro.Image = Properties.Resources.bird;
                        break;
                    }
                case 2:
                    {
                        pro.Image = Properties.Resources.cat;
                        break;
                    }
                case 3:
                    {
                        pro.Image = Properties.Resources.dragon;
                        break;
                    }
                case 4:
                    {
                        pro.Image = Properties.Resources.thatguy;
                        break;
                    }
                case 5:
                    {
                        pro.Image = Properties.Resources.girl;
                        break;
                    }
                case 6:
                    {
                        pro.Image = Properties.Resources.guy;
                        break;

                    }
                case 7:
                    {
                        pro.Image = Properties.Resources.granny;
                        break;
                    }
                case 8:
                    {
                        pro.Image = Properties.Resources.grandpa;
                        break;
                    }
            }
            pros.Image = pro.Image;
        }
        public static void SetPro(PictureBox pro,int prof,PictureBox pros)
        {
            switch (prof)
            {
                case 0:
                    {
                        pro.Image = Properties.Resources.Car;
                        break;
                    }
                case 1:
                    {
                        pro.Image = Properties.Resources.bird;
                        break;
                    }
                case 2:
                    {
                        pro.Image = Properties.Resources.cat;
                        break;
                    }
                case 3:
                    {
                        pro.Image = Properties.Resources.dragon;
                        break;
                    }
                case 4:
                    {
                        pro.Image = Properties.Resources.thatguy;
                        break;
                    }
                case 5:
                    {
                        pro.Image = Properties.Resources.girl;
                        break;
                    }
                case 6:
                    {
                        pro.Image = Properties.Resources.guy;
                        break;

                    }
                case 7:
                    {
                        pro.Image = Properties.Resources.granny;
                        break;
                    }
                case 8:
                    {
                        pro.Image = Properties.Resources.grandpa;
                        break;
                    }
            }
            pros.Image = pro.Image;
        }


    }

}

   

