using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Forms
{
    public partial class Aut : Form
    {
        SQLEngine sql = new SQLEngine();
        public Aut()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable aut;
            aut = sql.MSRunQuery($"Select Код_препода,Код_статуса FROM Преподаватели WHERE Логин = ('{textBox1.Text.Trim()}') AND Пароль = ('{textBox2.Text.Trim()}');");
            if (aut.Rows.Count == 0)
            {
                textBox2.Text = null;
                MessageBox.Show("Неверно введен логин или пароль");
                return;
            }
            Data.Kod_polzovatel = int.Parse(aut.Rows[0][0].ToString());
            Data.Pravo = int.Parse(aut.Rows[0][1].ToString());
            textBox1.Text = null;
            textBox2.Text = null;
            Data.AutStat = true;
            ListForms.OpenForms("Menus", "Menus", this, enabled: true);
        }

        private void Aut_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            e.Graphics.DrawLine(pen, 0, Size.Width - 1, 0, 0);
            e.Graphics.DrawLine(pen, Size.Width - 1, Size.Height - 1, Size.Width - 1, 0);
            e.Graphics.DrawLine(pen, 0, 0, Size.Width - 1, 0);
            e.Graphics.DrawLine(pen, 0, Size.Height - 1, Size.Width - 1, Size.Height - 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListForms.OpenForms("Reg", "", this);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ListForms.OpenForms("Menus", "Menus", this, enabled: true);
        }

        private void Aut_VisibleChanged(object sender, EventArgs e)
        {
            if(Data.Pravo == 1 )           
                yt_Button2.Visible = true;            
            else            
                yt_Button2.Visible = false;
            
            if (!Data.AutStat)
                stackPanel1.SelectedIndex = 0;
            else
            {
                stackPanel1.SelectedIndex = 1;
                DataTable prof = sql.MSRunQuery($"SELECT Фамилия,Имя,Отчество,Наименование FROM Статус,Преподаватели WHERE Код_препода = {Data.Kod_polzovatel} AND Преподаватели.Код_статуса = Статус.Код_статуса;", true);
                label2.Text = prof.Rows[0][0].ToString();
                label7.Text = prof.Rows[0][1].ToString();
                label8.Text = prof.Rows[0][2].ToString();
                label9.Text = prof.Rows[0][3].ToString();
            }

        }

        private void yt_Button3_Click(object sender, EventArgs e)
        {
            Data.AutStat = false;
            Data.Pravo = 0;
            ListForms.OpenForms("Menus", "Menus", this, enabled: true);
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
