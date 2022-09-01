using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1.Forms
{
    public partial class DobArchive : Form
    {
        public DobArchive()
        {
            InitializeComponent();
        }
        SQLEngine sql = new SQLEngine();
        private void pictureBox3_Click(object sender, EventArgs e) => ListForms.OpenForms("MainForm", "MainForm", this, enabled: true);

        private void button1_Click(object sender, EventArgs e)
        {
            sql.MSExecute($"INSERT INTO Архив VALUES ({Data.kodArchive},'{dateTimePicker1.Value:yyyy-MM-dd}',N'{multiColumnComboBox1.Text}');", true);
            ListForms.OpenForms("MainForm", "MainForm", this, enabled: true);
        }

        private void DobArchive_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            e.Graphics.DrawLine(pen, 0, Size.Width - 1, 0, 0);
            e.Graphics.DrawLine(pen, Size.Width - 1, Size.Height - 1, Size.Width - 1, 0);
            e.Graphics.DrawLine(pen, 0, 0, Size.Width - 1, 0);
            e.Graphics.DrawLine(pen, 0, Size.Height - 1, Size.Width - 1, Size.Height - 1);
        }

        private void multiColumnComboBox1_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = true;

        private void DobArchive_VisibleChanged(object sender, EventArgs e)
        {
            stackPanel1.SelectedIndex = Data.StatusDobForm;
        }

        private void yt_Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите данные!");
                return;
            }
            sql.MSExecute($"INSERT INTO Группы VALUES (N'{textBox1.Text}');", true);
            textBox1.Text = null;
            MessageBox.Show("Группа добавлена!");
            ListForms.OpenForms("MainForm", "MainForm", this, enabled: true);
        }
    }
}
