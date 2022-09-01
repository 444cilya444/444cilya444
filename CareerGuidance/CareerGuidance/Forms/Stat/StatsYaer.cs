using CareerGuidance.Model;
using System;
using System.Data;
using System.Windows.Forms;

namespace CareerGuidance.Forms.Stat
{
    public partial class StatsYaer : Form
    {
        readonly SQLEngine sql = new SQLEngine();
        public StatsYaer()
        {
            InitializeComponent();
        }

        private void StatsYaer_Load(object sender, EventArgs e)
        {
            egoldsFormStyle1.FormStyle = (Components.EgoldsFormStyle.fStyle)Data.StyleForms;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListForms.OpenForms("StatsForm", false, this, true);
        }

        private void StatsYaer_FormClosing(object sender, FormClosingEventArgs e) => Application.Exit();
        DataTable aut;
        void UpTable(string year)
        {
            aut = sql.MSRunQuery($"Select * FROM StatostYear WHERE Год = '{year}';");
            if (aut.Rows.Count == 0)
            {
                MessageBox.Show("Статистика по году не найдена!");
                return;
            }
            LB24.Text = aut.Rows[0][1].ToString();
            int ind = 0;
            for (int j = 2; j < 7; j++)
            {
                for (int i = 0; i <= 5; i++)
                {
                    if (ind > 23)
                        break;
                    (Controls[$"LB{ind}"] as Label).Text = aut.Rows[i][j].ToString();
                    ind++;
                }
            }
        }

        private void yt_Button2_Click(object sender, EventArgs e)
        {
            UpTable(textBox3.Text);
            for (int i = 0; i < 25; i++)
            {
                (Controls[$"T{i}"] as TextBox).Visible = false;
                (Controls[$"LB{i}"] as Label).Visible = true;
                (Controls[$"LB{i}"] as Label).BringToFront();
            }
        }

        private void yt_Button1_Click(object sender, EventArgs e)
        {
            if (!T0.Visible)
            {
                for (int i = 0; i < 25; i++)
                {
                    (Controls[$"LB{i}"] as Label).Visible = false;
                    (Controls[$"T{i}"] as TextBox).Visible = true;
                    (Controls[$"T{i}"] as TextBox).BringToFront();
                }
                return;
            }
            bool checkTB = true;
            for (int i = 0; i < 25; i++)
            {
                if (string.IsNullOrEmpty((Controls[$"T{i}"] as TextBox).Text))
                {
                    checkTB = false;
                }
            }
            if (!checkTB)
            {
                MessageBox.Show("Заполните таблицу!");
                return;
            }
            aut = sql.MSRunQuery($"Select * FROM StatostYear WHERE Год = '{T24.Text}';");
            if (aut.Rows.Count != 0)
            {
                MessageBox.Show("Статистика данного года уже существует!");
                return;
            }
            if (T24.Text.Length < 4)
            {
                MessageBox.Show("Карретно введите год");
                return;
            }
            int t0 = 0, t1 = 1, t2 = 2, t3 = 3;
            for (int i = 1; i < 7; i++)
            {
                sql.MSExecute($"INSERT INTO StatostYear VALUES ('{(Controls[$"T24"] as TextBox).Text}','{(Controls[$"T{t0}"] as TextBox).Text}','{(Controls[$"T{t1}"] as TextBox).Text}','{(Controls[$"T{t2}"] as TextBox).Text}','{(Controls[$"T{t3}"] as TextBox).Text}',{i})", true);
                t0 += 4; t1 += 4; t2 += 4; t3 += 4;

            }
            MessageBox.Show("Статистика обновлена!");
        }
        private void T24_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) & (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void T2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) & (e.KeyChar != ',') & (e.KeyChar != (char)Keys.Back))
                e.Handled = true;
        }
    }
}
