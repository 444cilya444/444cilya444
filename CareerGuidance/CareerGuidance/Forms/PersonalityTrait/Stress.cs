using CareerGuidance.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CareerGuidance.Forms.PersonalityTrait
{
    public partial class Stress : Form
    {
        readonly SQLEngine sql = new SQLEngine();
        List<Question> Source = new List<Question>();
        Question first;
        DataTable Table;
        public Stress()
        {
            InitializeComponent();
        }
        private void Stress_Load(object sender, EventArgs e)
        {
            egoldsFormStyle1.FormStyle = (Components.EgoldsFormStyle.fStyle)Data.StyleForms;
            Test();
        }
        private void Stress_FormClosing(object sender, FormClosingEventArgs e) => Application.Exit();
        int IndQvest = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            yt_Button but = ((yt_Button)sender);
            if (but == yt_Button1)
                sql.Execute($"UPDATE стресс SET Ответ=1 WHERE Код_вопроса=({IndQvest});");
            if (but == yt_Button2)
                sql.Execute($"UPDATE стресс SET  Ответ=2 WHERE Код_вопроса=({IndQvest});");
            if (but == yt_Button3)
                sql.Execute($"UPDATE стресс SET  Ответ=3 WHERE Код_вопроса=({IndQvest});");
            if (but == yt_Button4)
                sql.Execute($"UPDATE стресс SET  Ответ=4 WHERE Код_вопроса=({IndQvest});");
            if (but == yt_Button5)
                sql.Execute($"UPDATE стресс SET  Ответ=5 WHERE Код_вопроса=({IndQvest});");

            if (Source.Count == 0)
            {
                textTest = null;
                Res();
                foreach (Control c in Controls)
                    if (c is yt_Button button && c != yt_Button7 && c != yt_Button13)
                        button.Enabled = false;
                return;
            }
            OnlyBuf();
        }
        public void Test()
        {
            IndQvest = 0;
            Table = sql.RunQuery($"Select * FROM стресс;");

            foreach (DataRow row in Table.Rows)
            {
                string question = row.ItemArray[1].ToString();
                Question q = new Question();
                q.QuestionText = question;
                Source.Add(q);
            }
            OnlyBuf();
        }
        DataTable Ress;
        string textTest = null;
        private void Res()
        {
            MessageBox.Show("Тест пройден!");
            Ress = sql.RunQuery($"Select SUM (Ответ) FROM стресс;");
            int Bal = Convert.ToInt32(Ress.Rows[0][0]);
            Bal -= 20;
            if (Bal >= 0 && Bal <= 15)
                Ress = sql.RunQuery($"Select Характеристика FROM стресс WHERE Код_вопроса='1';");
            if (Bal > 15 && Bal < 50)
                Ress = sql.RunQuery($"Select Характеристика FROM стресс WHERE Код_вопроса='2';");
            if (Bal >= 50)
                Ress = sql.RunQuery($"Select Характеристика FROM стресс WHERE Код_вопроса='3';");

            textTest += "Вы набрали " + $"{Bal} " + $"{Ress.Rows[0][0]}";
            sql.MSExecute($"INSERT INTO статистика VALUES (N'{textTest}','{DateTime.Now:yyyy-MM-dd HH:mm:ss}',7,'{Data.Kod_polzovatel}')");
        }
        public void OnlyBuf()
        {
            first = Source.First();
            Source.Remove(first);
            label1.Text = first.QuestionText;
            IndQvest++;
            label2.Text = "Прогресс: " + IndQvest.ToString() + "/20";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ListForms.OpenForms("PersonalityTrait", false, this, true);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Data.QestionsTable = sql.RunQuery("Select Описание FROM описаниетестов WHERE Код_описания='4';");
            MessageBox.Show(Data.QestionsTable.Rows[0][0].ToString());
            return;
        }
    }
}
