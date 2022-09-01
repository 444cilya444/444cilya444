using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsApp1.Forms
{
    public partial class Menus : Form
    {
        public Menus()
        {
            InitializeComponent();
        }
        private void Menus_Load(object sender, EventArgs e)
        {
            ListForms.RegistrForm(this);
        }
        private void Menus_EnabledChanged(object sender, EventArgs e)
        {
            if (Data.Pravo == 1)
            {
                yt_Button4.Visible = true;
                yt_Button5.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
            }
            else
            {
                yt_Button4.Visible = false;
                yt_Button5.Visible = false;
                pictureBox7.Visible = false;
                pictureBox8.Visible = false;
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ListForms.OpenForms("Aut", "Menus", this, enabled: true, hide:false);
        }

        private void pictureBox3_Click(object sender, EventArgs e) => Application.Exit();

        private void Menus_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            e.Graphics.DrawLine(pen, 0, Size.Width - 1, 0, 0);
            e.Graphics.DrawLine(pen, Size.Width - 1, Size.Height - 1, Size.Width - 1, 0);
            e.Graphics.DrawLine(pen, 0, 0, Size.Width - 1, 0);
            e.Graphics.DrawLine(pen, 0, Size.Height - 1, Size.Width - 1, Size.Height - 1);
        }

        private void yt_Button1_Click(object sender, EventArgs e)
        {
            yt_Button bt = (sender as yt_Button);
            if (bt == yt_Button1)
            {
                Data.Status = 0;
                Data.StatusText = bt.Text;
                ListForms.OpenForms("MainForm", "", this);
            }
            if (bt == yt_Button2)
            {
                Data.Status = 1;
                Data.StatusText = bt.Text;
                ListForms.OpenForms("MainForm", "", this);
            }
            if (bt == yt_Button3)
            {
                Data.Status = 2;
                Data.StatusText = bt.Text;
                ListForms.OpenForms("MainForm", "", this);
            }
            if (bt == yt_Button4)
            {
                Data.Status = 3;
                Data.StatusText = bt.Text;
                ListForms.OpenForms("MainForm", "", this);
            }
            if (bt == yt_Button5)
            {
                Data.Status = 4;
                Data.StatusText = bt.Text;
                ListForms.OpenForms("MainForm", "", this);
            }
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

    }
}
