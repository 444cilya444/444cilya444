using CareerGuidance.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CareerGuidance
{
    public partial class Aut : StyleForm
    {
        readonly SQLEngine sql = new SQLEngine();
        public Aut()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            sql.CreateNewDataBase = false;
            sql.NameDataBase = "CareerGuidance.sqlite";
            ListForms.RegistrForm(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
        
            dataGridView1.DataSource = sql.RunQuery($"Select Код_пользователя,Код_права FROM Пользователи WHERE Логин = ('{textBox1.Text.Trim()}') AND Пароль = ('{textBox2.Text.Trim()}');");
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Неверно введен логин или пароль");
                return;
            }
            Data.Kod_polzovatel = int.Parse(new Regex("\\D").Replace(dataGridView1.CurrentRow.Cells[0].Value.ToString(), ""));
            Data.Prava = int.Parse(new Regex("\\D").Replace(dataGridView1.CurrentRow.Cells[1].Value.ToString(), ""));
            Hide();
            ListForms.OpenForms("MainMenu", false);
            ListForms.CleanText(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {         
            Hide();
            ListForms.OpenForms("Register", false);
        }

    }
}
