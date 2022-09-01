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
    public partial class Reg : Form
    {
        SQLEngine sql = new SQLEngine();
        public Reg()
        {
            InitializeComponent();
        }
        private void pictureBox3_Click(object sender, EventArgs e) => ListForms.OpenForms("Aut", "", this);
        private void Reg_Load(object sender, EventArgs e)
        {
            DataTable comboTable = sql.MSRunQuery($"Select * FROM Статус");
            multiColumnComboBox1.DataSource = comboTable;
            multiColumnComboBox1.DisplayMember = " Наименование";
            multiColumnComboBox1.ValueMember = "Код_статуса";
        }

        private void yt_Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            DataTable log = sql.MSRunQuery($"SELECT * FROM Преподаватели WHERE Логин = N'{textBox2.Text}'");
            if (log.Rows.Count != 0)
            {
                MessageBox.Show("Логин уже занят!");
                return;
            }
            string[] words = textBox1.Text.Split(' ');
            sql.MSExecute($"INSERT INTO Преподаватели VALUES ({multiColumnComboBox1.SelectedValue},N'{words[0]}',N'{words[1]}',N'{words[2]}',N'{textBox2.Text}',N'{textBox3.Text}');", true);
            MessageBox.Show("Регистрация прошла успешно!");
            ListForms.OpenForms("Menus", "Menus", this, enabled: true);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            int lenght1 = textBox1.Text.Where(x => x == ' ').Count();
            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || char.IsNumber(c))
                e.Handled = true;
            if (c != 8)
            {
                if (textBox1.Text.Length == 0 || textBox1.Text.EndsWith(" "))
                    e.KeyChar = char.Parse(c.ToString().ToUpper());

                if (c == ' ')
                    if (textBox1.Text.Length == 0 || lenght1 > 1 || textBox1.Text.EndsWith(" "))
                        e.Handled = true;
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

        private void Reg_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            e.Graphics.DrawLine(pen, 0, Size.Width - 1, 0, 0);
            e.Graphics.DrawLine(pen, Size.Width - 1, Size.Height - 1, Size.Width - 1, 0);
            e.Graphics.DrawLine(pen, 0, 0, Size.Width - 1, 0);
            e.Graphics.DrawLine(pen, 0, Size.Height - 1, Size.Width - 1, Size.Height - 1);
        }
    }
}
