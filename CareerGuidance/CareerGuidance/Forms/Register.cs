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
    public partial class Register : Form
    {
        readonly SQLEngine sql = new SQLEngine();
        public Register()
        {
            InitializeComponent();
        }
        private void Register_FormClosing(object sender, FormClosingEventArgs e) => Application.Exit();
        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            ListForms.OpenForms("MainMenu", false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*if (textBox1.Text == "" | textBox2.Text == "" | textBox5.Text == "" | textBox6.Text == "")
            {
                Controls.OfType<TextBox>().ToList().ForEach(item =>
                {
                    textBox1_Validating(item, null);
                });
                MessageBox.Show("Заполните обязательные поля");
                return;
            }
            if (textBox5.Text != textBox6.Text)
            {
                errorProvider1.SetError(textBox5, "Поля не совпадают");
                errorProvider1.SetError(textBox6, "Поля не совпадают");
                return;
            }
            if (textBox4.Text.Length < 5)
            {
                MessageBox.Show("Логин не должен быть меньше пяти символов");
                return;
            }
            if (textBox5.Text.Length < 5)
            {
                MessageBox.Show("Пароль не должен быть меншье пяти символов");
                return;
            }
            dataGridView1.DataSource = sql.RunQuery($"Select Логин FROM Пользователи WHERE Логин = ('{textBox4.Text.Trim()}');");
            if (dataGridView1.CurrentRow != null)
            {
                errorProvider1.SetError(textBox4, "Логин занят");
            }*/
            comboBox2.DataSource = sql.RunQuery($"Select Код_права FROM Права WHERE Права=('{comboBox1.SelectedValue}')");
            comboBox2.DisplayMember = "Код_права";
            comboBox2.ValueMember = "Код_права";

            if (int.Parse(comboBox2.SelectedValue.ToString()) == 1)
            {
                if (!Data.Admin)
                {
                    if (DialogResult.Yes == MessageBox.Show("Права администратора может назначить только администратор", "Ограниченый доступ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        ListForms.OpenForms("Aut2", true);
                        return;
                    }
                    else
                    {
                        ListForms.CleanText(this);
                        return;
                    }
                }
            }
            sql.Execute($"INSERT INTO Пользователи VALUES (NULL,('{textBox1.Text.Trim()}'),('{textBox2.Text.Trim()}'),('{textBox3.Text.Trim()}'),('{dateTime.Value.ToShortDateString()}'),('{textBox4.Text.Trim()}'),('{textBox5.Text.Trim()}'),('{comboBox2.SelectedValue}'));", true);
            dataGridView1.DataSource = sql.RunQuery($"Select Код_пользователя,Код_права FROM Пользователи WHERE Логин = ('{textBox5.Text.Trim()}') AND Пароль = ('{textBox6.Text.Trim()}');");
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Регистрация не завершена"); return;
            }
            else
            {
                if (DialogResult.Yes == MessageBox.Show("Регистрация прошла успешно, перейти на форму авторизации?", "Завершение регистрации", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    ListForms.CleanText(this);
                    this.Hide();
                    ListForms.OpenForms("Aut", true);
                    ListForms.CleanText(this);
                    Data.Admin = false;
                }
                else ListForms.CleanText(this);
            }
        }

        private void Register_Load(object sender, EventArgs e)
        {
            sql.CreateNewDataBase = false;
            sql.NameDataBase = "CareerGuidance.sqlite";

            comboBox1.DataSource = sql.RunQuery($"Select Права FROM Права");
            comboBox1.DisplayMember = "Права";
            comboBox1.ValueMember = "Права";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox tx = sender as TextBox;
            if (!string.IsNullOrEmpty(tx.Text)) errorProvider1.SetError(tx, null);
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            TextBox tx = sender as TextBox;
            if (string.IsNullOrEmpty(tx.Text))
            {
                errorProvider1.SetError(tx, "Заполните поле");
            }
            else
            {
                errorProvider1.SetError(tx, null);
            }
        }

       
    }
}
