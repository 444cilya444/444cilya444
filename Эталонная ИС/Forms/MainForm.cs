using Microsoft.Office.Interop.Word;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Forms
{
    public partial class MainForm : Form
    {
        SQLEngine sql = new SQLEngine();
        public MainForm()
        {
            InitializeComponent();
        }
        string s = AppDomain.CurrentDomain.BaseDirectory;

        private void MainForm_EnabledChanged(object sender, EventArgs e)
        {
            if (Enabled)
                UpData();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists("Сироты"))
                Directory.CreateDirectory("Сироты");
            if (!Directory.Exists("Архив"))
                Directory.CreateDirectory("Архив");
            if (!Directory.Exists("Инвалиды"))
                Directory.CreateDirectory("Инвалиды");
            if (!Directory.Exists("Поведение, результаты диагностики"))
                Directory.CreateDirectory("Поведение, результаты диагностики");

            for (int i = 2018; i <= 2100; i++)
            {
                comboBox1.Items.Add(i);
                comboBox2.Items.Add(i);
            }

            ToolTip t = new ToolTip();
            t.SetToolTip(pictureBox9, "Подсказка для TextBox");
            t.SetToolTip(pictureBox9, "Подсказка для TextBox");
            t.SetToolTip(pictureBox9, "Подсказка для TextBox");
            t.SetToolTip(pictureBox9, "Подсказка для TextBox");
            t.SetToolTip(pictureBox9, "Подсказка для TextBox");
            t.SetToolTip(pictureBox9, "Подсказка для TextBox");
            t.SetToolTip(pictureBox9, "Подсказка для TextBox");
            t.SetToolTip(pictureBox7, "Редактировать документ");
            t.SetToolTip(pictureBox5, "Удалить запись");
            t.SetToolTip(pictureBox1, "Импортировать файл");
            t.SetToolTip(pictureBox4, "Добавить запись");
            t.SetToolTip(pictureBox9, "Назад");

            UpData();
            multiColumnComboBox3.SelectedIndex = 0;
            multiColumnComboBox4.SelectedIndex = 0;
            multiColumnComboBox5.SelectedIndex = 0;

            multiColumnComboBox7.SelectedIndex = 0;
            multiColumnComboBox6.SelectedIndex = 0;
            multiColumnComboBox1.SelectedIndex = 0;

            multiColumnComboBox11.SelectedIndex = 0;
            multiColumnComboBox10.SelectedIndex = 0;
            multiColumnComboBox9.SelectedIndex = 0;
        }
        private void MainForm_VisibleChanged(object sender, EventArgs e)
        {
            Data.kodArchive = -1;
            label1.Text = Data.StatusText;
            stackPanel1.SelectedIndex = Data.Status;
            UpData();
        }
        private void pictureBox2_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) => System.Windows.Forms.Application.Exit();
        private void pictureBox3_Click(object sender, EventArgs e) => System.Windows.Forms.Application.Exit();
        private void multiColumnComboBox3_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = true;
        private void pictureBox9_Click(object sender, EventArgs e) => ListForms.OpenForms("Menus", "", this);
        public void UpData()
        {
            if (Data.Status == 0)
                upSup(multiColumnComboBox2, dataGridView1, 1);
            if (Data.Status == 1)
                upSup(multiColumnComboBox8, dataGridView2, 2);
            if (Data.Status == 2)
                upSup(multiColumnComboBox12, dataGridView3, 3);
            if (Data.Status == 3)
            {
                dataGridView4.DataSource = sql.MSRunQuery("SELECT Код_обучающегося,CONCAT(Фамилия,' ',Имя,' ',Отчество) AS ФИО,Группа,(Статус) AS 'Основание постановки',Семестр,YEAR(Дата_постановки) AS Год FROM Обучающиеся,Группы,Категории WHERE Обучающиеся.Код_группы = Группы.Код_группы AND Обучающиеся.Код_категории = Категории.Код_категории AND NOT EXISTS(SELECT Код_обучающегося FROM Архив WHERE Код_обучающегося = Обучающиеся.Код_обучающегося) ORDER BY Статус DESC;", true);
                dataGridView4.Columns[0].Visible = false;
            }
            if (Data.Status == 4)
            {
                dataGridView5.DataSource = sql.MSRunQuery("SELECT Код_архива,CONCAT(Фамилия,' ',Имя,' ',Отчество) AS ФИО,(Дата_рождения) AS 'Дата рождения',Группа,(Статус) AS 'Основание постановки',(Дата_снятия) AS 'Дата снятия с учета',(Причина_снятия) AS 'Причина снятия' FROM Обучающиеся,Группы,Категории,Архив WHERE Архив.Код_обучающегося = Обучающиеся.Код_обучающегося AND Обучающиеся.Код_группы = Группы.Код_группы AND Обучающиеся.Код_категории = Категории.Код_категории ORDER BY Статус DESC;");
                dataGridView5.Columns[0].Visible = false;
            }
        }


        void upSup(MultiColumnComboBox mCir, DataGridView dt, int ind, string where = "")
        {
            dt.DataSource = sql.MSRunQuery($"SELECT Код_обучающегося,CONCAT(Фамилия,' ',Имя,' ',Отчество) AS ФИО,(Дата_рождения) AS 'Дата рождения',Группа,(Статус) AS 'Основание постановки',(Дата_постановки) AS 'Дата постановки на учет',Семестр FROM Обучающиеся,Группы,Категории WHERE Обучающиеся.Код_категории = {ind} AND Обучающиеся.Код_группы = Группы.Код_группы AND Обучающиеся.Код_категории = Категории.Код_категории  {where} AND NOT EXISTS(SELECT Код_обучающегося FROM Архив WHERE Код_обучающегося = Обучающиеся.Код_обучающегося);", true);
            dt.Columns[0].Visible = false;

            System.Data.DataTable comboTable = sql.MSRunQuery($"Select Код_группы,Группа FROM Группы");
            mCir.DataSource = comboTable;
            mCir.DisplayMember = "Группа";
            mCir.ValueMember = "Код_группы";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] words = textBox1.Text.Split(' ');
            if (words.Length != 3)
            {
                MessageBox.Show("Проверьте правильность заполнения данных!");
                return;
            }
            textBox1.Clear();
            sql.MSExecute($"INSERT INTO Обучающиеся VALUES ({Data.Status + 1},{multiColumnComboBox2.SelectedValue},N'{words[0]}',N'{words[1]}',N'{words[2]}','{dateTimePicker1.Value:yyyy-MM-dd}','{dateTimePicker2.Value:yyyy-MM-dd}',N'{multiColumnComboBox3.Text}');", true);
            UpData();
        }
        private void pictureBox18_Click(object sender, EventArgs e)
        {
            string[] words = textBox4.Text.Split(' ');
            if (words.Length != 3)
            {
                MessageBox.Show("Проверьте правильность заполнения данных!");
                return;
            }
            textBox4.Clear();
            sql.MSExecute($"INSERT INTO Обучающиеся VALUES ({Data.Status + 1},{multiColumnComboBox8.SelectedValue},N'{words[0]}',N'{words[1]}',N'{words[2]}','{dateTimePicker1.Value:yyyy-MM-dd}','{DateTime.Now:yyyy-MM-dd}',N'{multiColumnComboBox7.Text}');", true);
            UpData();
        }
        private void pictureBox33_Click(object sender, EventArgs e)
        {
            string[] words = textBox6.Text.Split(' ');
            if (words.Length != 3)
            {
                MessageBox.Show("Проверьте правильность заполнения данных!");
                return;
            }
            textBox6.Clear();
            sql.MSExecute($"INSERT INTO Обучающиеся VALUES ({Data.Status + 1},{multiColumnComboBox12.SelectedValue},N'{words[0]}',N'{words[1]}',N'{words[2]}','{dateTimePicker1.Value:yyyy-MM-dd}','{DateTime.Now:yyyy-MM-dd}',N'{multiColumnComboBox11.Text}');", true);
            UpData();
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            int lenght1 = (sender as TextBox).Text.Where(x => x == ' ').Count();
            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || char.IsNumber(c))
                e.Handled = true;
            if (c != 8)
            {
                if ((sender as TextBox).Text.Length == 0 || (sender as TextBox).Text.EndsWith(" "))
                    e.KeyChar = char.Parse(c.ToString().ToUpper());

                if (c == ' ')
                    if ((sender as TextBox).Text.Length == 0 || lenght1 > 1 || (sender as TextBox).Text.EndsWith(" "))
                        e.Handled = true;
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (Data.kodArchive == -1)
                return;
            Data.StatusDobForm = 0;
            ListForms.OpenForms("DobArchive", "MainForm", this, enabled: true, hide: false);
        }
        string[] mData = new string[9];
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Data.kodArchive = Convert.ToInt32((sender as DataGridView).CurrentRow.Cells[0].Value);
            mData[0] = (sender as DataGridView).CurrentRow.Cells[1].Value.ToString();
            mData[1] = Convert.ToDateTime((sender as DataGridView).CurrentRow.Cells[2].Value).ToString("dd.MM.yyyy");
            mData[2] = (sender as DataGridView).CurrentRow.Cells[3].Value.ToString();
            mData[3] = (sender as DataGridView).CurrentRow.Cells[4].Value.ToString();
            mData[4] = Convert.ToDateTime((sender as DataGridView).CurrentRow.Cells[5].Value).ToString("dd.MM.yyyy");
            if ((sender as DataGridView) == dataGridView1)
            {
                mData[5] = "";
                mData[6] = "";
                mData[7] = multiColumnComboBox4.Text;
                mData[8] = multiColumnComboBox5.Text;
            }
            if ((sender as DataGridView) == dataGridView2)
            {
                mData[5] = "";
                mData[6] = "";
                mData[7] = multiColumnComboBox6.Text;
                mData[8] = multiColumnComboBox1.Text;
            }
            if ((sender as DataGridView) == dataGridView3)
            {
                mData[5] = "";
                mData[6] = "";
                mData[7] = multiColumnComboBox10.Text;
                mData[8] = multiColumnComboBox9.Text;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (Data.kodArchive == -1)
            {
                MessageBox.Show("Выберите запись из списка!");
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Удалить запись", "Удаление записи", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
                return;
            sql.MSExecute($"DELETE FROM Обучающиеся WHERE Код_обучающегося = {Data.kodArchive}");
            Data.kodArchive = -1;
            UpData();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Excel excel = new Excel();
            excel.inputExcel();
            UpData();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }



        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            e.Graphics.DrawLine(pen, 0, Size.Width - 1, 0, 0);
            e.Graphics.DrawLine(pen, Size.Width - 1, Size.Height - 1, Size.Width - 1, 0);
            e.Graphics.DrawLine(pen, 0, 0, Size.Width - 1, 0);
            e.Graphics.DrawLine(pen, 0, Size.Height - 1, Size.Width - 1, Size.Height - 1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string selWhere = null;
            TextBox t = sender as TextBox;
            string[] words = t.Text.Split(' ');
            if (words.Length == 1)
            {
                selWhere += $" AND Фамилия LIKE N'{words[0].Trim()}%'";
            }
            if (words.Length == 2)
            {
                selWhere += $" AND Фамилия LIKE N'{words[0].Trim()}%'";
                selWhere += $" AND Имя LIKE N'{words[1].Trim()}%'";

            }
            if (words.Length == 3)
            {
                selWhere += $" AND Фамилия LIKE N'{words[0].Trim()}%'";
                selWhere += $" AND Имя LIKE N'{words[1].Trim()}%'";
                selWhere += $" AND Отчество LIKE N'{words[2].Trim()}%'";
            }
            if (Data.Status == 0)
                upSup(multiColumnComboBox2, dataGridView1, 1, where: selWhere);
            if (Data.Status == 1)
                upSup(multiColumnComboBox8, dataGridView2, 2, where: selWhere);
            if (Data.Status == 2)
                upSup(multiColumnComboBox12, dataGridView3, 3, where: selWhere);
        }

        private void dateTimePicker7_ValueChanged(object sender, EventArgs e)
        {
            string selWhere = null;
            selWhere += $" AND ([Дата_постановки] BETWEEN '{(sender as DateTimePicker).Value:yyyy-MM-dd}' AND '{(sender as DateTimePicker).Value:yyyy-MM-dd}')";
            if (Data.Status == 0)
                upSup(multiColumnComboBox2, dataGridView1, 1, where: selWhere);
            if (Data.Status == 1)
                upSup(multiColumnComboBox8, dataGridView2, 2, where: selWhere);
            if (Data.Status == 2)
                upSup(multiColumnComboBox12, dataGridView3, 3, where: selWhere);
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            string selWhere = null;
            selWhere += $"AND year(Дата_постановки) = year('{(sender as ComboBox).Text}')";
            if (Data.Status == 3)
            {
                dataGridView4.DataSource = sql.MSRunQuery($"SELECT Код_обучающегося,CONCAT(Фамилия,' ',Имя,' ',Отчество) AS ФИО,Группа,(Статус) AS 'Основание постановки',Семестр,YEAR(Дата_постановки) AS Год FROM Обучающиеся,Группы,Категории WHERE Обучающиеся.Код_группы = Группы.Код_группы {selWhere} AND Обучающиеся.Код_категории = Категории.Код_категории AND NOT EXISTS(SELECT Код_обучающегося FROM Архив WHERE Код_обучающегося = Обучающиеся.Код_обучающегося);", true);
                dataGridView4.Columns[0].Visible = false;
            }
            if (Data.Status == 4)
            {
                dataGridView5.DataSource = sql.MSRunQuery($"SELECT Код_архива,CONCAT(Фамилия,' ',Имя,' ',Отчество) AS ФИО,(Дата_рождения) AS 'Дата рождения',Группа,(Статус) AS 'Основание постановки',(Дата_снятия) AS 'Дата снятия с учета',(Причина_снятия) AS 'Причина снятия' FROM Обучающиеся,Группы,Категории,Архив WHERE Архив.Код_обучающегося = Обучающиеся.Код_обучающегося AND Обучающиеся.Код_группы = Группы.Код_группы AND Обучающиеся.Код_категории = Категории.Код_категории {selWhere}");
                dataGridView5.Columns[0].Visible = false;
            }
        }
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            string selWhere = null;
            TextBox t = sender as TextBox;
            string[] words = t.Text.Split(' ');
            if (words.Length == 1)
            {
                selWhere += $" AND Фамилия LIKE N'{words[0].Trim()}%'";
            }
            if (words.Length == 2)
            {
                selWhere += $" AND Фамилия LIKE N'{words[0].Trim()}%'";
                selWhere += $" AND Имя LIKE N'{words[1].Trim()}%'";

            }
            if (words.Length == 3)
            {
                selWhere += $" AND Фамилия LIKE N'{words[0].Trim()}%'";
                selWhere += $" AND Имя LIKE N'{words[1].Trim()}%'";
                selWhere += $" AND Отчество LIKE N'{words[2].Trim()}%'";
            }
            if (Data.Status == 3)
            {
                dataGridView4.DataSource = sql.MSRunQuery($"SELECT Код_обучающегося,CONCAT(Фамилия,' ',Имя,' ',Отчество) AS ФИО,Группа,(Статус) AS 'Основание постановки',Семестр,YEAR(Дата_постановки) AS Год FROM Обучающиеся,Группы,Категории WHERE Обучающиеся.Код_группы = Группы.Код_группы {selWhere} AND Обучающиеся.Код_категории = Категории.Код_категории AND NOT EXISTS(SELECT Код_обучающегося FROM Архив WHERE Код_обучающегося = Обучающиеся.Код_обучающегося);", true);
                dataGridView4.Columns[0].Visible = false;
            }
            if (Data.Status == 4)
            {
                dataGridView5.DataSource = sql.MSRunQuery($"SELECT Код_архива,CONCAT(Фамилия,' ',Имя,' ',Отчество) AS ФИО,(Дата_рождения) AS 'Дата рождения',Группа,(Статус) AS 'Основание постановки',(Дата_снятия) AS 'Дата снятия с учета',(Причина_снятия) AS 'Причина снятия' FROM Обучающиеся,Группы,Категории,Архив WHERE Архив.Код_обучающегося = Обучающиеся.Код_обучающегося AND Обучающиеся.Код_группы = Группы.Код_группы AND Обучающиеся.Код_категории = Категории.Код_категории {selWhere}");
                dataGridView5.Columns[0].Visible = false;
            }
        }
        private void pictureBox31_Click(object sender, EventArgs e)
        {
            Data.StatusDobForm = 1;
            ListForms.OpenForms("DobArchive", "MainForm", this, enabled: true, hide: false);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            dataGridViewCach.DataSource = sql.MSRunQuery($"SELECT Семестр,(Дата_постановки) AS 'Дата постановки на учет',(Статус) AS 'Основание постановки',Группа,(Дата_рождения) AS 'Дата рождения',CONCAT(Фамилия,' ',Имя,' ',Отчество) AS ФИО FROM Обучающиеся,Группы,Категории WHERE Обучающиеся.Код_категории = 1 AND Обучающиеся.Код_группы = Группы.Код_группы AND Обучающиеся.Код_категории = Категории.Код_категории AND NOT EXISTS(SELECT Код_обучающегося FROM Архив WHERE Код_обучающегося = Обучающиеся.Код_обучающегося);", true);
            ClsPrint _ClsPrint = new ClsPrint(dataGridViewCach, "Сироты");
            _ClsPrint.PrintForm();

        }
        private void pictureBox26_Click(object sender, EventArgs e)
        {
            dataGridViewCach.DataSource = sql.MSRunQuery("SELECT YEAR(Дата_постановки) AS Год,Семестр,(Статус) AS 'Основание постановки',Группа,CONCAT(Фамилия,' ',Имя,' ',Отчество) AS ФИО FROM Обучающиеся,Группы,Категории WHERE Обучающиеся.Код_группы = Группы.Код_группы AND Обучающиеся.Код_категории = Категории.Код_категории AND NOT EXISTS(SELECT Код_обучающегося FROM Архив WHERE Код_обучающегося = Обучающиеся.Код_обучающегося) ORDER BY Статус DESC;", true);
            ClsPrint _ClsPrint = new ClsPrint(dataGridViewCach, "База данных обучающихся \"Группы риска\" семестр/год");
            _ClsPrint.PrintForm();
        }

        private void pictureBox29_Click(object sender, EventArgs e)
        {
            dataGridViewCach.DataSource = sql.MSRunQuery("SELECT (Причина_снятия) AS 'Причина снятия',(Дата_снятия) AS 'Дата снятия с учета',(Статус) AS 'Основание постановки',Группа,(Дата_рождения) AS 'Дата рождения',CONCAT(Фамилия,' ',Имя,' ',Отчество) AS ФИО FROM Обучающиеся,Группы,Категории,Архив WHERE Архив.Код_обучающегося = Обучающиеся.Код_обучающегося AND Обучающиеся.Код_группы = Группы.Код_группы AND Обучающиеся.Код_категории = Категории.Код_категории ORDER BY Статус DESC;");
            ClsPrint _ClsPrint = new ClsPrint(dataGridViewCach, "Архив");
            _ClsPrint.PrintForm();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (Data.kodArchive == -1)
            {
                MessageBox.Show("Выберете студента!");
                return;
            }
            Enabled = false;
            var wordApp = new Microsoft.Office.Interop.Word.Application();
            try
            {
                var wordDoc = wordApp.Documents.Add($@"{AppDomain.CurrentDomain.BaseDirectory}Secret.docx");
                ReplaceStub("[ФИО]", mData[0], wordDoc);
                ReplaceStub("[Дата_рождения]", mData[1], wordDoc);
                ReplaceStub("[Группа]", mData[2], wordDoc);
                ReplaceStub("[Основание]", mData[3], wordDoc);
                ReplaceStub("[Дата_постановки]", mData[4], wordDoc);
                ReplaceStub("[Причина]", mData[5], wordDoc);
                ReplaceStub("[Дата_снятия]", mData[6], wordDoc);
                ReplaceStub("[Статус]", mData[7], wordDoc);
                ReplaceStub("[Обучение]", mData[8], wordDoc);
                if (sender as PictureBox == pictureBox7)
                {
                    wordDoc.SaveAs2($@"{s}\Сироты\{mData[0]} {mData[1]}.docx");
                    Process.Start($@"{s}\Сироты\{mData[0]} {mData[1]}.docx");
                }                 
                if (sender as PictureBox == pictureBox15) 
                {
                    wordDoc.SaveAs2($@"{s}\Инвалиды\{mData[0]} {mData[1]}.docx");
                    Process.Start($@"{s}\Инвалиды\{mData[0]} {mData[1]}.docx");
                }                  
                if (sender as PictureBox == pictureBox23)
                {
                    wordDoc.SaveAs2($@"{s}\Поведение, результаты диагностики\{mData[0]} {mData[1]}.docx");
                    Process.Start($@"{s}\Поведение, результаты диагностики\{mData[0]} {mData[1]}.docx");
                }                
                wordDoc.Close();               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Enabled = true;
        }
        private void ReplaceStub(string stubToReplace, string text, Document worldDocument)
        {
            var range = worldDocument.Content;
            range.Find.ClearFormatting();
            object wdReplaceAll = WdReplace.wdReplaceAll;
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text, Replace: wdReplaceAll);
        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {
            if (Data.kodArchive == -1)
            {
                MessageBox.Show("Выберите запись из списка!");
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Удалить запись", "Удаление записи", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
                return;
            sql.MSExecute($"DELETE FROM Обучающиеся WHERE Код_обучающегося = {Data.kodArchive}");
            Data.kodArchive = -1;
            UpData();
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Data.kodArchive = Convert.ToInt32((sender as DataGridView).CurrentRow.Cells[0].Value);
            mData[0] = (sender as DataGridView).CurrentRow.Cells[1].Value.ToString();
            mData[1] = Convert.ToDateTime((sender as DataGridView).CurrentRow.Cells[2].Value).ToString("dd.MM.yyyy");
            mData[2] = (sender as DataGridView).CurrentRow.Cells[3].Value.ToString();
            mData[3] = (sender as DataGridView).CurrentRow.Cells[4].Value.ToString();
            System.Data.DataTable data = sql.MSRunQuery($"SELECT Код_обучающегося FROM Архив WHERE Код_архива = {Data.kodArchive}");
            data = sql.MSRunQuery($"SELECT Дата_постановки FROM Обучающиеся WHERE Код_обучающегося = {data.Rows[0][0]}");
            mData[4] = Convert.ToDateTime(data.Rows[0][0]).ToString("dd.MM.yyyy");
            mData[5] = (sender as DataGridView).CurrentRow.Cells[6].Value.ToString();
            mData[6] = Convert.ToDateTime((sender as DataGridView).CurrentRow.Cells[5].Value).ToString("dd.MM.yyyy");
            mData[7] = "";
            mData[8] = "";
        }

        private void pictureBox30_Click(object sender, EventArgs e)
        {
            if (Data.kodArchive == -1)
            {
                MessageBox.Show("Выберите запись из списка!");
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Удалить запись", "Удаление записи", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
                return;
            sql.MSExecute($"DELETE FROM Архив WHERE Код_архива = {Data.kodArchive}", true);
            Data.kodArchive = -1;
            UpData();
        }

        private void pictureBox6_Click_1(object sender, EventArgs e)
        {
            if (Data.kodArchive == -1)
            {
                MessageBox.Show("Выберете студента!");
                return;
            }
            Enabled = false;
            var wordApp = new Microsoft.Office.Interop.Word.Application();
            try
            {
                
                var wordDoc = wordApp.Documents.Add($@"{AppDomain.CurrentDomain.BaseDirectory}Secret.docx");
                ReplaceStub("[ФИО]", mData[0], wordDoc);
                ReplaceStub("[Дата_рождения]", mData[1], wordDoc);
                ReplaceStub("[Группа]", mData[2], wordDoc);
                ReplaceStub("[Основание]", mData[3], wordDoc);
                ReplaceStub("[Дата_постановки]", mData[4], wordDoc);
                ReplaceStub("[Причина]", mData[5], wordDoc);
                ReplaceStub("[Дата_снятия]", mData[6], wordDoc);
                ReplaceStub("[Статус]", mData[7], wordDoc);
                ReplaceStub("[Обучение]", mData[8], wordDoc);
                wordDoc.SaveAs2($@"{s}\Архив\{mData[0]} {mData[1]}.docx");
                Process.Start($@"{s}\Архив\{mData[0]} {mData[1]}.docx");
                wordDoc.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Enabled = true;
        }

    }
}

