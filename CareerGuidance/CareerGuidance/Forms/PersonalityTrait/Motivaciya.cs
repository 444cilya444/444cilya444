using CareerGuidance.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CareerGuidance.Forms.PersonalityTrait
{
    public partial class Motivaciya : Form
    {
        readonly SQLEngine sql = new SQLEngine();
        List<Question> Source = new List<Question>();
        Question first;
        DataTable Table;
        public Motivaciya()
        {
            InitializeComponent();
        }
        int IndQvest = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            yt_Button but = ((yt_Button)sender);
            if (but == yt_Button1)
                sql.Execute($"UPDATE мотивация SET Ответ=4 WHERE Код_мотивации=({IndQvest});");
            if (but == yt_Button2)
                sql.Execute($"UPDATE мотивация SET  Ответ=3 WHERE Код_мотивации=({IndQvest});");
            if (but == yt_Button3)
                sql.Execute($"UPDATE мотивация SET  Ответ=2 WHERE Код_мотивации=({IndQvest});");
            if (but == yt_Button4)
                sql.Execute($"UPDATE мотивация SET  Ответ=1 WHERE Код_мотивации=({IndQvest});");
            if (but == yt_Button5)
                sql.Execute($"UPDATE мотивация SET  Ответ=0 WHERE Код_мотивации=({IndQvest});");

            if (Source.Count == 0)
            {
                textTest = null;
                Res();
                foreach (Control c in Controls)
                    if (c is yt_Button && c != yt_Button7 && c != yt_Button13)
                        ((yt_Button)c).Enabled = false;
                return;
            }
            OnlyBuf();
        }
        DataTable Ress;
        DataTable Ress2;
        string textTest = null;
        private void Res()
        {
            MessageBox.Show("Тест пройден!");
            Ress = sql.RunQuery($"Select SUM (Ответ) FROM мотивация Where Индекс=1;");
            Ress2 = sql.RunQuery($"Select SUM (Ответ) FROM мотивация Where Индекс=2;");
            textTest += "Уровень развития познавательной мотивации: " + $"{Ress.Rows[0][0]}/60\n"+ "Уровень развития социальной мотивации учебной деятельности: " + $"{Ress2.Rows[0][0]}/60";
            sql.MSExecute($"INSERT INTO статистика VALUES (N'{textTest}','{DateTime.Now:yyyy-MM-dd HH:mm:ss}',3,'{Data.Kod_polzovatel}')");          
        }
        public void OnlyBuf()
        {
            first = Source.First();
            Source.Remove(first);
            label1.Text = first.QuestionText;
            IndQvest++;
            label2.Text = "Прогресс: " + IndQvest.ToString() + "/30";
        }

        private void Motivaciya_Load(object sender, EventArgs e)
        {
            egoldsFormStyle1.FormStyle = (Components.EgoldsFormStyle.fStyle)Data.StyleForms;
            Test();
        }
        public void Test()
        {
            IndQvest = 0;
            Table = sql.RunQuery($"Select * FROM мотивация;");

            foreach (DataRow row in Table.Rows)
            {
                string question = row.ItemArray[1].ToString();
                Question q = new Question();
                q.QuestionText = question;
                Source.Add(q);
            }
            OnlyBuf();
        }

        private void Motivaciya_FormClosing(object sender, FormClosingEventArgs e) => Application.Exit();

        private void button6_Click(object sender, EventArgs e)
        {
            ListForms.OpenForms("PersonalityTrait", false, this, true);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Data.QestionsTable = sql.RunQuery("Select Описание FROM описаниетестов WHERE Код_описания='2';");
            MessageBox.Show(Data.QestionsTable.Rows[0][0].ToString());
            return;
        }
    }
}
