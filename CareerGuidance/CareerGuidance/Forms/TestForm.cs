using CareerGuidance.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace CareerGuidance
{
    public partial class TestForm : Form
    {
        readonly SQLEngine sql = new SQLEngine();
        Question first;
        int IndQvest = 0;
        public TestForm()
        {
            InitializeComponent();
            egoldsFormStyle1.MaxButtonActivated = false;
        }
        List<Question> Source = new List<Question>();
        private void Form1_FormClosed(object sender, FormClosedEventArgs e) => Application.Exit();
        private void Form1_Load(object sender, EventArgs e)
        {
            egoldsFormStyle1.FormStyle = (Components.EgoldsFormStyle.fStyle)Data.StyleForms;
            TableClear();
            DataTable Table = sql.RunQuery($"Select * FROM Вопросы;");
            foreach (DataRow row in Table.Rows)
            {
                string question = row.ItemArray[1].ToString();
                Question q = new Question();
                q.QuestionText = question;
                Source.Add(q);
            }
            OnlyBuf();
        }
        public void TableClear()
        {
            sql.Execute($"UPDATE Вопросы SET Балл=NULL;");
        }
        DataTable Ress, Ress2;
        public void Res()
        {
            MessageBox.Show("Тест пройден!");
            string ress = null;
            if (Source.Count == 0)
            {
                for (int i = 1; i < 7; i++)
                {
                    Ress = sql.RunQuery($"Select SUM (Балл) FROM Вопросы WHERE Код_группы = '{i}';");
                    Ress2 = sql.RunQuery($"Select Код_специальности,Специальность,Описание FROM Специальности WHERE Код_группы = '{i}';");
                    ress += Ress2.Rows[0][0].ToString() + " == " + Ress.Rows[0][0].ToString()+"/16" + "\n\n" + "Расшифровка специальности:\n" + Ress2.Rows[0][1].ToString() + "\n\n" + "Описание:\n" + Ress2.Rows[0][2].ToString() + "\n----------------------------------\n";
                }
                sql.MSExecute($"INSERT INTO статистика VALUES (N'{ress}','{DateTime.Now:yyyy-MM-dd HH:mm:ss}',4,'{Data.Kod_polzovatel}')");
                return;
            }
        }
        public void OnlyBuf()
        {
            first = Source.First();
            Source.Remove(first);

            label1.Text = first.QuestionText;
            IndQvest++;
            label2.Text = "Прогресс: " + IndQvest.ToString() + "/56";
        }

        private void ButtonOnlyBuf_Click(object sender, EventArgs e)
        {
           
            string s = ((yt_Button)sender).Text;
            if (s == "Да")
                sql.Execute($"UPDATE Вопросы SET Балл=Балл_за_Да WHERE Код_вопроса=({IndQvest});");
            if (s == "Сомневаюсь")
                sql.Execute($"UPDATE Вопросы SET Балл=Сомневаюсь WHERE Код_вопроса=({IndQvest});");

            if (Source.Count == 0)
            {
                Res();
                foreach (Control c in Controls)
                    if (c is yt_Button && c != yt_Button4 && c != yt_Button5 && c != yt_Button13)
                        ((yt_Button)c).Enabled = false;
                return;
            }
            OnlyBuf();
        }

        private void yt_Button13_Click(object sender, EventArgs e)
        {
            Data.QestionsTable = sql.RunQuery("Select Описание FROM описаниетестов WHERE Код_описания='5';");
            MessageBox.Show(Data.QestionsTable.Rows[0][0].ToString());
            return;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (((yt_Button)sender) == yt_Button5)
                ListForms.OpenForms("MainMenu", false, this, true);
            if (((yt_Button)sender) == yt_Button4)
                ListForms.OpenForms("MenegereLogick", false, this, true);
        }

    }
}
