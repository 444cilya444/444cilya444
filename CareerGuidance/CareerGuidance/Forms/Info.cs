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
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
        }

        private void Info_Load(object sender, EventArgs e)
        {
            egoldsFormStyle1.FormStyle = (Components.EgoldsFormStyle.fStyle)Data.StyleForms;
        }

        private void yt_Button1_Click(object sender, EventArgs e)
        {
            ListForms.OpenForms("StartMenu", false, this, true);
        }

        private void Info_VisibleChanged(object sender, EventArgs e)
        {
            if (Data.Info)
            {
                label1.Visible = true;
                label2.Visible = false;
                Width = 1144;
                Height = 392;
                yt_Button1.Location = new Point(12,333);
                CenterToScreen();

            }
            else
            {
                label1.Visible = false;
                label2.Visible = true;                         
                Width = 790;
                Height = 150;
                yt_Button1.Location = new Point(12, 110);
                CenterToScreen();
            }
        }

        private void Info_FormClosing(object sender, FormClosingEventArgs e)=>Application.Exit();
    }
}
