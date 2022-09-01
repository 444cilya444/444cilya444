using CareerGuidance.Model;
using System;
using System.Windows.Forms;

namespace CareerGuidance
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            egoldsFormStyle1.MaxButtonActivated = false;
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e) => Application.Exit();

        private void button1_Click(object sender, EventArgs e)
        {
            yt_Button But = (yt_Button)sender;
            if (But == button1)
                ListForms.OpenForms("TestForm", false, this, true);
            if (But == button2)
                ListForms.OpenForms("StatsForm", false, this, true);                      
            if (But == button5)
                ListForms.OpenForms("ProfOrent", false, this, true);
            if (But == button6)
                ListForms.OpenForms("PersonalityTrait", false, this, true);
            if (But == yt_Button1)
                ListForms.OpenForms("StartMenu", false, this, true);

        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            egoldsFormStyle1.FormStyle = (Components.EgoldsFormStyle.fStyle)Data.StyleForms;
        }
    }
}
