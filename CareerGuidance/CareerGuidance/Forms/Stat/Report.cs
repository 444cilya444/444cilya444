using CareerGuidance.Model;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace CareerGuidance.Forms
{
    public partial class Report : Form
    {
        readonly SQLEngine sql = new SQLEngine();
        public Report()
        {
            InitializeComponent();
        }
        DataTable report, nameTest;
        private void button1_Click(object sender, EventArgs e)
        {

            bool flag = true;
            for (int i = 1; i <= 7; i++)
            {
                EgoldsToggleSwitch RTB = (Controls.Find("checkBox" + i, true).FirstOrDefault() as EgoldsToggleSwitch);
                if (RTB.Checked == true)
                    flag = false;
            }
            if (flag)
            {
                MessageBox.Show("   Выберете минимум 1 тест для создания отчета", "   Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() != DialogResult.OK)
                return;
            for (int i = 1; i <= 7; i++)
            {
                EgoldsToggleSwitch RTB = (Controls.Find("checkBox" + i, true).FirstOrDefault() as EgoldsToggleSwitch);
                if (RTB.Checked == false)
                    continue;
                string res;
                report = sql.MSRunQuery($"SELECT Текст_теста,Дата_ответа FROM статистика WHERE Код_пользователя=('{Data.Kod_polzovatelReport}') AND Индекс_теста = ('{i}');");
                if (report.Rows.Count != 0)
                    res = report.Rows[0][0].ToString();
                else
                    res = "Нет данных, тест не пройден";

                RichTextBox rt = new RichTextBox() { Name = i.ToString(), Visible = false, Text = res, Width = 400 };
                Controls.Add(rt);
                rt.Height = (rt.GetLineFromCharIndex(rt.Text.Length) + 2) * rt.Font.Height + rt.Margin.Vertical;
                nameTest = sql.RunQuery($"SELECT Название FROM описаниетестов WHERE Код_описания=('{i}')", true);
                GetControlScreenshot(rt, $@"{FBD.SelectedPath}/{nameTest.Rows[0][0]}.png");
                Controls.RemoveByKey(i.ToString());
            }
            DialogResult result = MessageBox.Show($"   Отчет создан, перейти к его расположению?",
            "   Отчет",
            MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Process.Start(FBD.SelectedPath.ToString());
            }
            Hide();
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

        private void yt_Button2_Click(object sender, EventArgs e) => Hide();
    }
}
