﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {        
            Hide();
            ListForms.OpenForms("Form1", "Form3", this);
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e) =>Application.Exit();
    }
}
