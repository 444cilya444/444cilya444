using CareerGuidance.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CareerGuidance.Forms.PersonalityTrait
{
    public partial class TipMishleniya : Form
    {

        readonly SQLEngine sql = new SQLEngine();
        List<Question> Source = new List<Question>();
        Question first;
        DataTable Table;
        int IndQvest = 0;
        public TipMishleniya()
        {
            InitializeComponent();
        }

        private void TipMishleniya_FormClosing(object sender, FormClosingEventArgs e) => Application.Exit();

        private void TipMishleniya_Load(object sender, EventArgs e)
        {
            egoldsFormStyle1.FormStyle = (Components.EgoldsFormStyle.fStyle)Data.StyleForms;
            Test();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            ListForms.OpenForms("PersonalityTrait", false, this, true);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            yt_Button but = ((yt_Button)sender);
            if (but == yt_Button1)
                sql.Execute($"UPDATE мышление SET Ответ=1 WHERE Код_вопроса=({IndQvest});");
            if (but == yt_Button2)
                sql.Execute($"UPDATE мышление SET Ответ=0 WHERE Код_вопроса=({IndQvest});");
         

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
        string textTest = null;
        public void Test()
        {
            IndQvest = 0;
            Table = sql.RunQuery($"Select * FROM мышление;");

            foreach (DataRow row in Table.Rows)
            {
                string question = row.ItemArray[1].ToString();
                Question q = new Question();
                q.QuestionText = question;
                Source.Add(q);
            }
            OnlyBuf();
        }
        DataTable Ress1, Ress2, Ress3, Ress4, Ress5;

        private void button7_Click(object sender, EventArgs e)
        {
            Data.QestionsTable = sql.RunQuery("Select Описание FROM описаниетестов WHERE Код_описания='3';");
            MessageBox.Show(Data.QestionsTable.Rows[0][0].ToString());
            return;
        }

        private void Res()
        {
            MessageBox.Show("Тест пройден!");
            Ress1 = sql.RunQuery($"Select SUM (Ответ) FROM мышление Where Индекс=1;");
            Ress2 = sql.RunQuery($"Select SUM (Ответ) FROM мышление Where Индекс=2;");
            Ress3 = sql.RunQuery($"Select SUM (Ответ) FROM мышление Where Индекс=3;");
            Ress4 = sql.RunQuery($"Select SUM (Ответ) FROM мышление Where Индекс=4;");
            Ress5 = sql.RunQuery($"Select SUM (Ответ) FROM мышление Where Индекс=5;");
            textTest = "Уровень развития Предметно – действенное мышления: " + $"{Ress1.Rows[0][0]}/8\n"
                + "Уровень развития Абстрактно – символическое мышления: " + $"{Ress2.Rows[0][0]}/8\n"+ 
                "Уровень развития Словесно – логическое мышления: " + $"{Ress3.Rows[0][0]}/8\n"+ 
                "Уровень развития Наглядно – образное мышления: "+
                $"{Ress4.Rows[0][0]}/8\n"+ 
                "Уровень развития Креативности: " + 
                $"{Ress5.Rows[0][0]}/8\n\n"+ 
                "Сумма баллов от 0 до 2 – характеризует низкий уровень развития типа мышления\n" +
                "Сумма баллов от 3 до 5 – характеризует средний уровень развития типа мышления\n" +
                "Сумма баллов от 6 до 8 – характеризует высокий уровень развития типа мышления";
            sql.MSExecute($"INSERT INTO статистика VALUES (N'{textTest}','{DateTime.Now:yyyy-MM-dd HH:mm:ss}',6,'{Data.Kod_polzovatel}')");
        }
        public void OnlyBuf()
        {
            first = Source.First();
            Source.Remove(first);
            label1.Text = first.QuestionText;
            IndQvest++;
            label2.Text = "Прогресс: " + IndQvest.ToString() + "/40";
        }

       
    }
}
