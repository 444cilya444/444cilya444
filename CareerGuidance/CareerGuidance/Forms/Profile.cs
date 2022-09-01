using CareerGuidance.Model;
using System;
using System.Windows.Forms;

namespace CareerGuidance
{
    public partial class Profile : Form
    {
        readonly SQLEngine sql = new SQLEngine();
        public Profile()
        {
            InitializeComponent();
        }

        private void Statistics_Load(object sender, EventArgs e)
        {
            sql.CreateNewDataBase = false;
            sql.NameDataBase = "CareerGuidance.sqlite";
            dataGridView1.DataSource = sql.RunQuery($"Select Имя,Фамилия,Отчество,Дата_рождения,Права FROM Пользователи,Права WHERE Пользователи.Код_права = Права.Код_права And Код_пользователя = ({Data.Kod_polzovatel});");
            label1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            label2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            label3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            label4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            label5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            ListForms.OpenForms("MainMenu", false);
        }

        private void Profile_FormClosing(object sender, FormClosingEventArgs e) => Application.Exit();

    }
}
