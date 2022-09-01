using CareerGuidance.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace CareerGuidance.Forms
{
    public partial class ProfOrent : Form
    {
        readonly SQLEngine sql = new SQLEngine();
        public ProfOrent()
        {
            InitializeComponent();
            egoldsFormStyle1.MaxButtonActivated = false;
        }
        List<Question> Source = new List<Question>();
        Question first;
        DataTable Table;
        DataTable countTable;
        DataTable ExitTable;
        DataTable NameTable;
        private void ProfOrent_Load(object sender, EventArgs e)
        {
            egoldsFormStyle1.FormStyle = (Components.EgoldsFormStyle.fStyle)Data.StyleForms;
            TableClear();
            Test(false);
        }
        int IndQvest = 0;
        public void OnlyBuf()
        {
            first = Source.First();
            Source.Remove(first);
            label1.Text = first.QuestionText;
            if (!_flag)
                countTable = sql.RunQuery("SELECT COUNT (Код_вопроса) FROM ПрофОТЦ");
            if (_flag)
                countTable = sql.RunQuery("SELECT COUNT (Код_вопроса) FROM ПрофОТЦ2");

            IndQvest++;
            label2.Text = "Прогресс: " + IndQvest.ToString() + "/" + countTable.Rows[0][0];
        }
        bool _flag;

        private void button2_Click(object sender, EventArgs e)
        {
            string s = ((yt_Button)sender).Text;

            if (_flag)
            {
                sql.Execute($"UPDATE ПрофОТЦ2 SET Ответ=({s}) WHERE Код_вопроса=({IndQvest});");
            }
            else
            {
                sql.Execute($"UPDATE ПрофОТЦ SET Ответ=({s}) WHERE Код_вопроса=({IndQvest});");
            }
            if (Source.Count == 0)
            {
                foreach (Control c in Controls)
                    if (c is yt_Button && c != button4 && c != yt_Button13 && c != yt_Button14)
                        ((yt_Button)c).Enabled = false;
                if (!_flag)
                {
                    Exit();
                }
                else
                {
                    ExitOnly();
                }
                return;
            }
            OnlyBuf();
        }

        private void ExitOnly()
        {
            MessageBox.Show("Тест пройден!");
            textTest = null;
            countTable = sql.RunQuery("SELECT COUNT (Код_вопроса) FROM ПрофОТЦ2");
            for (int i = 1; i < Convert.ToInt32(countTable.Rows[0][0]) + 1; i++)
            {
                NameTable = sql.RunQuery($"Select Вопрос,Ответ,Порог FROM ПрофОТЦ2 WHERE Код_вопроса = ('{i}');");
                textTest += NameTable.Rows[0][0] + " ==  " + NameTable.Rows[0][1] + "\n";
            }
            sql.MSExecute($"INSERT INTO статистика VALUES (N'{textTest}','{DateTime.Now:yyyy-MM-dd HH:mm:ss}',2,'{Data.Kod_polzovatel}')");
        }
        string textTest = null;
        private void Exit()
        {
            MessageBox.Show("Тест пройден!");
            textTest = null;
            for (int i = 1; i < Convert.ToInt32(countTable.Rows[0][0]) + 1; i++)
            {
                NameTable = sql.RunQuery($"Select Имя_ключа,Порог FROM КлючПроф WHERE Код_ключа = ('{i}');");
                ExitTable = sql.RunQuery($"Select SUM (Ответ) " +
                    $"FROM ПрофОТЦ " +
                    $"WHERE Ключ1 = ('{NameTable.Rows[0][0]}') " +
                    $"OR Ключ2=('{NameTable.Rows[0][0]}') " +
                    $"OR Ключ3=('{NameTable.Rows[0][0]}') " +
                    $"OR Ключ4=('{NameTable.Rows[0][0]}') " +
                    $"OR Ключ5=('{NameTable.Rows[0][0]}') " +
                    $"OR Ключ6=('{NameTable.Rows[0][0]}') " +
                    $"OR Ключ7=('{NameTable.Rows[0][0]}') " +
                    $"OR Ключ8=('{NameTable.Rows[0][0]}') " +
                    $"OR Ключ9=('{NameTable.Rows[0][0]}') " +
                    $"OR Ключ10=('{NameTable.Rows[0][0]}') " +
                    $"OR Ключ11=('{NameTable.Rows[0][0]}') " +
                    $"OR Ключ12=('{NameTable.Rows[0][0]}') " +
                    $"OR Ключ13=('{NameTable.Rows[0][0]}') " +
                    $"OR Ключ14=('{NameTable.Rows[0][0]}') " +
                    $"OR Ключ15=('{NameTable.Rows[0][0]}') " +
                    $"OR Ключ16=('{NameTable.Rows[0][0]}') " +
                    $"OR Ключ17=('{NameTable.Rows[0][0]}') " +
                    $"OR Ключ18=('{NameTable.Rows[0][0]}');");            
                textTest +=NameTable.Rows[0][0] + " == " + ExitTable.Rows[0][0] + "\n";
            }
            sql.MSExecute($"INSERT INTO статистика VALUES (N'{textTest}','{DateTime.Now:yyyy-MM-dd HH:mm:ss}',1,'{Data.Kod_polzovatel}')");
        }
        public void TableClear()
        {
            sql.Execute($"UPDATE ПрофОТЦ SET Ответ=NULL;");
            sql.Execute($"UPDATE ПрофОТЦ2 SET Ответ=NULL;");
        }
        private void button11_Click(object sender, EventArgs e)
        {
            Source.Clear();
            if (((yt_Button)sender).Text == "2я часть")
            {
                ((yt_Button)sender).Text = "1я часть";
                Test(true);
            }
            else
            {
                ((yt_Button)sender).Text = "2я часть";
                Test(false);
            }
            foreach (Control c in Controls)
                if (c is yt_Button)
                    ((yt_Button)c).Enabled = true;
        }
        public void Test(bool flag)
        {
            IndQvest = 0;
            if (!flag)
                Table = sql.RunQuery($"Select * FROM ПрофОТЦ;");
            else
                Table = sql.RunQuery($"Select * FROM ПрофОТЦ2;");
            _flag = flag;
            foreach (DataRow row in Table.Rows)
            {
                string question = row.ItemArray[1].ToString();
                Question q = new Question();
                q.QuestionText = question;
                Source.Add(q);
            }
            OnlyBuf();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ListForms.OpenForms("MainMenu", false, this, true);
        }

        private void ProfOrent_FormClosing(object sender, FormClosingEventArgs e) => Application.Exit();

        private void yt_Button13_Click(object sender, EventArgs e)
        {
            DataTable rrr = sql.RunQuery("SELECT Описание FROM описаниетестов WHERE Код_описания='1'");
            MessageBox.Show(rrr.Rows[0][0].ToString(),"Справка");
        }
    }
}
