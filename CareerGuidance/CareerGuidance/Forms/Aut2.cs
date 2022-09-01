using CareerGuidance.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CareerGuidance.Forms
{
    public partial class Aut2 : Form
    {
        readonly SQLEngine sql = new SQLEngine();
        public Aut2()
        {
            InitializeComponent();
        }

        private void Aut2_Load(object sender, EventArgs e)
        {
            sql.CreateNewDataBase = false;
            sql.NameDataBase = "CareerGuidance.sqlite";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sql.RunQuery($"Select Код_пользователя,Код_права FROM Пользователи WHERE Логин = ('{textBox1.Text.Trim()}') AND Пароль = ('{textBox2.Text.Trim()}');");
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Неверно введен логин или пароль");
                return;
            }
            Data.Admin = true;
            Hide();
        }

        private void Aut2_FormClosing(object sender, FormClosingEventArgs e) => Application.Exit();

    }
}
