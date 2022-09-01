using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace SQliteEngeV2
{
    public partial class Form1 : Form
    {
        readonly SQLEngine sql = new SQLEngine();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                pictureBox2.Image = Image.FromFile(openFileDialog1.FileName);


            sql.ExecuteImageDownload(pictureBox2, "@Фото", $"UPDATE тестТаблица SET Фото=(@Фото) WHERE Код_таблицы=2;");



            pictureBox2.Image = sql.ExecuteImageOpen($"SELECT Фото FROM тестТаблица WHERE Код_таблицы=2;");

            dataGridView1.DataSource = sql.RunQuery($"Select * FROM тестТаблица;");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sql.RunQuery($"Select * FROM тестТаблица;");

            dataGridView2.DataSource = sql.MSRunQuery($"Select * FROM Name;");
            DataTable comboTable = sql.RunQuery($"Select Код_таблицы,Текст,ЕщеСтолбец FROM тестТаблица");
            multiColumnComboBox1.DataSource = comboTable;
            multiColumnComboBox1.DisplayMember = "Текст";
            multiColumnComboBox1.ValueMember = "Код_таблицы";
            label2.Text = multiColumnComboBox1.SelectedValue + "";
        }
    }
}
