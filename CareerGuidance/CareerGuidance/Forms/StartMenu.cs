using CareerGuidance.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CareerGuidance.Forms
{
    public partial class StartMenu : Form
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        private void StrartMenu_Load(object sender, EventArgs e)
        {
            egoldsFormStyle1.FormStyle = (Components.EgoldsFormStyle.fStyle)Data.StyleForms;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            yt_Button But = (yt_Button)sender;
            if (But == button6) 
            {
                Data.Info = true;
                ListForms.OpenForms("Info", false, this, true);
            }
             
            if (But == button5)
                ListForms.OpenForms("StatsForm", false, this, true);
            if (But == button1)
                ListForms.OpenForms("MainMenu", false, this, true);
            if (But == yt_Button1)
            {
                Data.Info = false;
                ListForms.OpenForms("Info", false, this, true);
            }            
            if (But == yt_Button2)
            {
                DialogResult result = MessageBox.Show($"    Выйти из учетной записи?",
                 "  Выход",
                 MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    ListForms.OpenForms("Aut", false, this, true);
                }
            }              
        }

        private void StartMenu_FormClosing(object sender, FormClosingEventArgs e) => Application.Exit();
    }
}
