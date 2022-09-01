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

namespace CareerGuidance.Forms.Stat
{
    public partial class StatsForm : Form
    {
        public StatsForm()
        {
            InitializeComponent();
        }

        private void StatsForm_Load(object sender, EventArgs e)
        {
            egoldsFormStyle1.FormStyle = (Components.EgoldsFormStyle.fStyle)Data.StyleForms;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            yt_Button But = (yt_Button)sender;
            if (But == button1)
                ListForms.OpenForms("Stats", false, this, true);
            if (But == button2)
                ListForms.OpenForms("StatsYaer", false, this, true);
            if (But == button3)
                ListForms.OpenForms("StartMenu", false, this, true);
         
        }

        private void StatsForm_FormClosing(object sender, FormClosingEventArgs e) => Application.Exit();
    }
}
