using CareerGuidance.Model;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CareerGuidance.Forms
{
    public partial class Register : Form
    {
        readonly SQLEngine sql = new SQLEngine();
        public Register()
        {
            InitializeComponent();
            egoldsFormStyle1.MaxButtonActivated = false;
            dateTime.Value = DateTime.Now.AddYears(-15);
        }
        private void Register_FormClosing(object sender, FormClosingEventArgs e) => Application.Exit();
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            ListForms.OpenForms("Aut", false, this, true);
        }
        DataTable reg;
        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" | textBox2.Text == "" | textBox5.Text == "" | textBox6.Text == "")
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
            reg = sql.MSRunQuery($"Select Логин FROM пользователи WHERE Логин = N'{textBox4.Text.Trim()}';");
            if (reg.Rows.Count != 0)
            {
                errorProvider1.SetError(textBox4, "Логин занят");
                return;
            }
            DataTable kod = sql.MSRunQuery($"Select Код_права FROM права WHERE Права=N'{comboBox1.SelectedValue}'");

            if (int.Parse(kod.Rows[0][0].ToString()) == 2)
            {
                if (!Data.Admin)
                {
                    if (DialogResult.Yes == MessageBox.Show("Права администратора может назначить только администратор", "Ограниченый доступ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        ListForms.OpenForms("Aut2", true, this, false);
                        return;
                    }
                    else
                    {
                        ListForms.CleanText(this);
                        return;
                    }
                }
            }

            sql.MSExecute($"INSERT INTO пользователи VALUES (N'{ListForms.capSentences(textBox1.Text.Trim())}',N'{ListForms.capSentences(textBox2.Text.Trim())}',N'{ListForms.capSentences(textBox3.Text.Trim())}','{dateTime.Value:yyyy-MM-dd}',N'{textBox4.Text}',N'{textBox5.Text}','{kod.Rows[0][0]}');");
            reg = sql.MSRunQuery($"Select Код_пользователя,Код_права FROM пользователи WHERE Логин = N'{textBox4.Text}' AND Пароль = N'{textBox6.Text}';");
            if (reg.Rows.Count == 0)
            {
                MessageBox.Show("Регистрация не завершена"); return;
            }
            else
            {
                if (DialogResult.Yes == MessageBox.Show("Регистрация прошла успешно, перейти на форму авторизации?", "Завершение регистрации", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    ListForms.OpenForms("Aut", true, this, true);
                    Data.Admin = false;
                }
            }
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
        }

        private void Register_Load(object sender, EventArgs e)
        {
            egoldsFormStyle1.FormStyle = (Components.EgoldsFormStyle.fStyle)Data.StyleForms;
            comboBox1.DataSource = sql.MSRunQuery($"Select Права FROM права");
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' || textBox1.Text.Length > 20)
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' || textBox2.Text.Length > 20)
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' || textBox3.Text.Length > 20)
                e.Handled = true;
        }

    }
}
