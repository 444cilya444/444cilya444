using CareerGuidance.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CareerGuidance
{
    public partial class MenegereLogick : Form
    {
        public MenegereLogick()
        {
            InitializeComponent();
            egoldsFormStyle1.MaxButtonActivated = false;
        }
        private void MenegereLogick_Load(object sender, EventArgs e)
        {
            egoldsFormStyle1.FormStyle = (Components.EgoldsFormStyle.fStyle)Data.StyleForms;
            Test();
        }

        readonly SQLEngine sql = new SQLEngine();
        List<Question> Source = new List<Question>();

        List<Question> AnswerA = new List<Question>();
        List<Question> AnswerB = new List<Question>();
        List<Question> AnswerV = new List<Question>();

        Question first;
        Question firstA;
        Question firstB;
        Question firstV;
        DataTable Table;
        int IndQvest = 0;

        private void button2_Click(object sender, EventArgs e)
        {
            string s = ((yt_Button)sender).Text;
            if (s == "А")
                sql.Execute($"UPDATE МенеджерЛоГ SET Балл=Балл_А WHERE Код_вопроса=({IndQvest});");
            if (s == "Б")
                sql.Execute($"UPDATE МенеджерЛоГ SET Балл=Балл_Б WHERE Код_вопроса=({IndQvest});");
            if (s == "В")
                sql.Execute($"UPDATE МенеджерЛоГ SET Балл=Балл_В WHERE Код_вопроса=({IndQvest});");


            if (Source.Count == 0)
            {
                Res();
                foreach (Control c in Controls)
                    if (c is yt_Button && c != button4 && c != yt_Button13)
                        ((yt_Button)c).Enabled = false;
                return;
            }
            OnlyBuf();
        }
        DataTable Ress;
        private void Res()
        {
            MessageBox.Show("Тест пройден!");
            Ress = sql.RunQuery($"Select SUM (Балл) FROM МенеджерЛоГ;");
            int Bal = Convert.ToInt32(Ress.Rows[0][0]);
            if (Bal >= 80)
                Ress = sql.RunQuery($"Select Характеристика FROM МенеджерЛоГ WHERE Код_вопроса='1';");
            if (Bal <= 79 && Bal >= 45)
                Ress = sql.RunQuery($"Select Характеристика FROM МенеджерЛоГ WHERE Код_вопроса='2';");
            if (Bal <= 44)
                Ress = sql.RunQuery($"Select Характеристика FROM МенеджерЛоГ WHERE Код_вопроса='3';");
            sql.MSExecute($"INSERT INTO статистика VALUES (N'{Ress.Rows[0][0]}','{DateTime.Now:yyyy-MM-dd HH:mm:ss}',5,'{Data.Kod_polzovatel}')");
        }
        public void Test()
        {
            IndQvest = 0;
            Table = sql.RunQuery($"Select * FROM МенеджерЛоГ;");

            foreach (DataRow row in Table.Rows)
            {
                string question = row.ItemArray[1].ToString();
                Question q = new Question();
                q.QuestionText = question;
                Source.Add(q);
                //Топорный код
                string answerA = row.ItemArray[3].ToString();
                Question a = new Question();
                a.AnswerA = answerA;
                AnswerA.Add(a);

                string answerB = row.ItemArray[4].ToString();
                Question b = new Question();
                b.AnswerB = answerB;
                AnswerB.Add(b);

                string answerV = row.ItemArray[5].ToString();
                Question v = new Question();
                v.AnswerV = answerV;
                AnswerV.Add(v);

            }
            OnlyBuf();
        }
        public void OnlyBuf()
        {
            first = Source.First();
            Source.Remove(first);
            label1.Text = first.QuestionText;
            //Топорный код
            firstA = AnswerA.First();
            AnswerA.Remove(firstA);
            label10.Text = firstA.AnswerA;

            firstB = AnswerB.First();
            AnswerB.Remove(firstB);
            label11.Text = firstB.AnswerB;

            firstV = AnswerV.First();
            AnswerV.Remove(firstV);
            label12.Text = firstV.AnswerV;

            IndQvest++;
            label2.Text = "Прогресс: " + IndQvest.ToString() + "/10";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (((yt_Button)sender) == button4)
                ListForms.OpenForms("MainMenu", false, this, true);
        }

        private void MenegereLogick_FormClosing(object sender, FormClosingEventArgs e) => Application.Exit();

        private void yt_Button13_Click(object sender, EventArgs e)
        {
            Data.QestionsTable = sql.RunQuery("Select Описание FROM описаниетестов WHERE Код_описания='6';");
            MessageBox.Show(Data.QestionsTable.Rows[0][0].ToString());
            return;
        }
    }
}
