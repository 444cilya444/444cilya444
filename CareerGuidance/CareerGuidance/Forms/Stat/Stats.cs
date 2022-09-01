using CareerGuidance.Model;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace CareerGuidance.Forms
{
    public partial class Stats : Form
    {
        SQLEngine sql = new SQLEngine();
        public Stats()
        {
            InitializeComponent();
            egoldsFormStyle1.MaxButtonActivated = false;
            PravaCheck();
        }
        DataTable StatProfel;
        private void Stats_Load(object sender, EventArgs e)
        {
            egoldsFormStyle1.FormStyle = (Components.EgoldsFormStyle.fStyle)Data.StyleForms;
        }
        DataTable Test, Report;
        void UpdateTestTable(string Kod)
        {
            PravaCheck();
            StatProfel = sql.MSRunQuery($"SELECT Фамилия,Имя,Отчество,Дата_рождения,Код_права,Код_пользователя FROM пользователи WHERE Код_пользователя='{Kod}';");
            if (StatProfel.Rows.Count == 0)
            {
                MessageBox.Show("Абитуриент с таким кодом не существует");
                return;
            }
            for (int i = 1; i <= 4; i++)
            {

                if (int.Parse(StatProfel.Rows[0][4].ToString()) == 2)
                    groupBox8.Text = "Учитель " + $"({StatProfel.Rows[0][5]})";
                else
                    groupBox8.Text = "Абитуриент " + $"({StatProfel.Rows[0][5]})";
                (Controls.Find("label" + i, true).FirstOrDefault() as Label).Text = StatProfel.Rows[0][i - 1].ToString();
                label4.Text = $"{StatProfel.Rows[0][3]:dd.MM.yyyy}";

            }
            for (int i = 1; i <= 7; i++)
            {
                Test = sql.MSRunQuery($"SELECT Текст_теста,Дата_ответа FROM статистика WHERE Код_пользователя=('{Kod}') AND Индекс_теста = ('{i}');");
                RichTextBox RTB = (Controls.Find("richTextBox" + i, true).FirstOrDefault() as RichTextBox);
                if (Test.Rows.Count != 0)
                    RTB.Text = Test.Rows[0][0].ToString();
                else
                    RTB.Text = "Нет данных, тест не пройден";
            }
        }
        private void Stats_FormClosing(object sender, FormClosingEventArgs e) => Application.Exit();


        private void yt_Button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            ListForms.OpenForms("StatsForm", false, this, true);
        }
        private void yt_Button1_Click(object sender, EventArgs e)
        {
            Test = sql.MSRunQuery($"SELECT Фамилия,Имя FROM пользователи WHERE Код_пользователя=('{Data.Kod_polzovatelReport}');");

            DialogResult result = MessageBox.Show($"     Удалить все результаты тестов связанные с {Test.Rows[0][0]} {Test.Rows[0][1]}?",
            "   Удаление данных!",
            MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                sql.MSExecute($"DELETE FROM статистика WHERE Код_пользователя = ('{Data.Kod_polzovatelReport}')");
                UpdateTestTable(Data.Kod_polzovatelReport.ToString());
                Data.Kod_polzovatelReport = Data.Kod_polzovatel.ToString();
            }
        }
        private void Stats_VisibleChanged(object sender, EventArgs e)
        {
            Data.Kod_polzovatelReport = Data.Kod_polzovatel.ToString();
            dataGridView1.DataSource = sql.MSRunQuery($"SELECT Код_пользователя AS Код,Фамилия,Имя,Отчество,Дата_рождения FROM пользователи");
            UpdateTestTable(Data.Kod_polzovatel.ToString());
        }

        private void yt_Button2_Click(object sender, EventArgs e)
        {
            string selWhere = null;
            if (textBox1.Text != "")
            {
                selWhere += $"Код_пользователя = '{textBox1.Text}'";
            }
            if (textBox2.Text != "")
            {
                if (selWhere != null)
                    selWhere += $" AND Фамилия = N'{ListForms.capSentences(textBox2.Text.Trim())}'";
                else
                    selWhere += $"Фамилия = N'{ListForms.capSentences(textBox2.Text.Trim())}'";
            }
            if (textBox3.Text != "")
            {
                if (selWhere != null)
                    selWhere += $" AND Имя = N'{ListForms.capSentences(textBox3.Text.Trim())}'";
                else
                    selWhere += $"Имя = N'{ListForms.capSentences(textBox3.Text.Trim())}'";
            }
            if (textBox4.Text != "")
            {
                if (selWhere != null)
                    selWhere += $" AND Отчество = N'{ListForms.capSentences(textBox4.Text.Trim())}'";
                else
                    selWhere += $"Отчество = N'{ListForms.capSentences(textBox4.Text.Trim())}'";
            }
            if (checkBox1.Checked == true)
            {
                if (selWhere != null)
                    selWhere += $" AND ([Дата_рождения] BETWEEN '{dateTimePicker1.Value:yyyy-MM-dd}' AND '{dateTimePicker2.Value:yyyy-MM-dd}')";
                else
                    selWhere += $"([Дата_рождения] BETWEEN '{dateTimePicker1.Value:yyyy-MM-dd}' AND '{dateTimePicker2.Value:yyyy-MM-dd}')";
            }
            if (selWhere == null)
                return;
            dataGridView1.DataSource = sql.MSRunQuery($"SELECT Код_пользователя AS Код,Фамилия,Имя,Отчество,Дата_рождения FROM пользователи WHERE {selWhere};");
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void yt_Button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            UpdateTestTable(Data.Kod_polzovatel.ToString());
            dataGridView1.DataSource = sql.MSRunQuery($"SELECT Код_пользователя AS Код,Фамилия,Имя,Отчество,Дата_рождения FROM пользователи");
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Data.Kod_polzovatelReport = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Test = sql.MSRunQuery($"SELECT Фамилия,Имя FROM пользователи WHERE Код_пользователя=('{Data.Kod_polzovatelReport}');");
            DialogResult result = MessageBox.Show($"   Просмотреть результаты тестов {Test.Rows[0][0]} {Test.Rows[0][1]}?",
            "   Просмотр данных",
            MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                UpdateTestTable(Data.Kod_polzovatelReport);
        }
        void PravaCheck()
        {
            if (Data.Prava == 2)//Admin
            {
                Width = 1236;
                CenterToScreen();
                groupBox1.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                yt_Button8.Visible = true;
                yt_Button9.Visible = true;
                yt_Button10.Visible = true;
                dataGridView1.Visible = true;
            }
            else//!Admin
            {
                Width = 765;
                CenterToScreen();
                groupBox1.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                yt_Button8.Visible = false;
                yt_Button9.Visible = false;
                yt_Button10.Visible = false;
                dataGridView1.Visible = false;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {

        }

        private void yt_Button4_Click(object sender, EventArgs e)
        {
            if (Data.Kod_polzovatelReport == null)
                Data.Kod_polzovatelReport = Data.Kod_polzovatel.ToString();

            Report = sql.MSRunQuery($"SELECT Фамилия,Имя FROM пользователи WHERE Код_пользователя=('{Data.Kod_polzovatelReport}');");
            DialogResult result = MessageBox.Show($"   Создать отчет тестов {Report.Rows[0][0]} {Report.Rows[0][1]}?",
            "   Отчет",
            MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ListForms.OpenForms("Report", true, this, false);
            }
        }

        private void textBox1_KeyPress_2(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == (char)Keys.Enter;
        }

        public Bitmap GetControlScreenshot(Control control, string filePath)
        {
            //ресайзим контрол до возможного максимума перед скриншотом
            Size szCurrent = control.Size;
            control.AutoSize = true;

            Bitmap bmp = new Bitmap(control.Width, control.Height);//создаем картинку нужных размеров
            control.DrawToBitmap(bmp, control.ClientRectangle);//копируем изображение нужного контрола в bmp

            //возвращаем размер контрола назад
            control.AutoSize = false;
            control.Size = szCurrent;
            bmp.Save(filePath, ImageFormat.Png);
            return bmp;
        }

    }
}
