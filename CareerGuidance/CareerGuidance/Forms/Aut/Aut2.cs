using CareerGuidance.Model;
using System;
using System.Data;
using System.Windows.Forms;

namespace CareerGuidance.Forms
{
    public partial class Aut2 : Form
    {
        readonly SQLEngine sql = new SQLEngine();
        public Aut2()
        {
            InitializeComponent();
        }
        DataTable aut;
        private void button1_Click(object sender, EventArgs e)
        {
            enter();
        }

        private void yt_Button2_Click(object sender, EventArgs e) => Hide();

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == (char)Keys.Enter;
        }
        void enter()
        {
            aut = sql.MSRunQuery($"Select Код_пользователя,Код_права FROM пользователи WHERE Логин = N'{textBox1.Text.Trim()}' AND Пароль = N'{textBox2.Text.Trim()}';");
            if (aut.Rows.Count == 0)
            {
                MessageBox.Show("Неверно введен логин или пароль");
                return;
            }
            if (int.Parse(aut.Rows[0][0].ToString()) == 1)
            {
                MessageBox.Show("Неверно введен логин или пароль");
                return;
            }
            Data.Admin = true;
            textBox1.Text = null;
            textBox2.Text = null;
            Hide();
        }
        private void Aut2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                enter();
            }
        }
    }
}
