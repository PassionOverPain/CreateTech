﻿using System;
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
    public partial class FatsandOils : Form
    {
        public FatsandOils()
        {
            InitializeComponent();
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
    }
}
