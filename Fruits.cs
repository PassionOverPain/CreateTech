using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateTech
{
    public partial class Fruits : Form
    {
        public Fruits()
        {
            InitializeComponent();
        }

        private void backbtnicon_Click(object sender, EventArgs e)
        {
            new FoodCategory().Show();
            this.Close();
        }

        private void pnlBackground_MouseHover(object sender, EventArgs e)
        {
            lbltime.Text = DateTime.Now.ToLocalTime().ToShortTimeString(); // Changes the time
        }

        private void profilepic_Click(object sender, EventArgs e)
        {
            pnlDashboard.Visible = true;
            pnlDashboard.Enabled = true;
            pnlDashboard.Width = 230;
            pnlDashboard.Height = 583;
            //230, 583 ~ panel dashboard size
        }
    }
}
