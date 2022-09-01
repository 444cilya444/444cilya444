using CareerGuidance.Model;
using System;
using System.Windows.Forms;

namespace CareerGuidance.Forms.PersonalityTrait
{
    public partial class PersonalityTrait : Form
    {
        public PersonalityTrait()
        {
            InitializeComponent();
        }
        private void PersonalityTrait_FormClosing(object sender, FormClosingEventArgs e) => Application.Exit();
        private void PersonalityTrait_Load(object sender, EventArgs e)
        {
            egoldsFormStyle1.FormStyle = (Components.EgoldsFormStyle.fStyle)Data.StyleForms;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            yt_Button but = (yt_Button)sender;
            if (but == yt_Button1)            
                ListForms.OpenForms("Stress", false, this, true);            
            if (but == yt_Button2)            
                ListForms.OpenForms("TipMishleniya", false, this, true);            
            if (but == yt_Button3)            
                ListForms.OpenForms("Motivaciya", false, this, true);           
            if (but == yt_Button4)           
                ListForms.OpenForms("MainMenu", false, this, true);
        }    
    }
}
