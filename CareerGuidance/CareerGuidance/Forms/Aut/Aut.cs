using CareerGuidance.Controls;
using CareerGuidance.Model;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace CareerGuidance
{
    public partial class Aut : ShadowedForm
    {
        readonly SQLEngine sql = new SQLEngine();
        public Aut()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            Data.StyleForms = 2;
            Animator.Start();
            egoldsFormStyle1.MaxButtonActivated = false;
            KeyPreview = true;
            ListForms.RegistrForm(this);
        }
        DataTable aut;
        private void button1_Click(object sender, EventArgs e)
        {
            new Thread(() => enter()).Start();
        }
        void enter()
        {
            visible(false);
            aut = sql.MSRunQuery($"Select Код_пользователя,Код_права FROM пользователи WHERE Логин = ('{textBox1.Text.Trim()}') AND Пароль = ('{textBox2.Text.Trim()}');");
            if (aut.Rows.Count == 0)
            {
                visible(true);
                textBox2.Text = null;
                MessageBox.Show("Неверно введен логин или пароль");
                return;
            }
            Data.Kod_polzovatel = int.Parse(new Regex("\\D").Replace(aut.Rows[0][0].ToString(), ""));
            Data.Prava = int.Parse(new Regex("\\D").Replace(aut.Rows[0][1].ToString(), ""));
            textBox1.Text = null;
            textBox2.Text = null;
            visible(true);
            ListForms.OpenForms("StartMenu", false, this, true);
        }

        void visible(bool offOn)
        {
            if (offOn)
            {
                pictureBox1.BeginInvoke((MethodInvoker)(() => pictureBox1.Visible = false));
                yt_Button1.BeginInvoke((MethodInvoker)(() => yt_Button1.Visible = true));
                yt_Button2.BeginInvoke((MethodInvoker)(() => yt_Button2.Visible = true));
                textBox1.BeginInvoke((MethodInvoker)(() => textBox1.Visible = true));
                textBox2.BeginInvoke((MethodInvoker)(() => textBox2.Visible = true));
            }
            else
            {

                pictureBox1.BeginInvoke((MethodInvoker)(() => pictureBox1.Visible = true));
                yt_Button1.BeginInvoke((MethodInvoker)(() => yt_Button1.Visible = false));
                yt_Button2.BeginInvoke((MethodInvoker)(() => yt_Button2.Visible = false));
                textBox1.BeginInvoke((MethodInvoker)(() => textBox1.Visible = false));
                textBox2.BeginInvoke((MethodInvoker)(() => textBox2.Visible = false));

            }
        }
        private void button2_Click(object sender, EventArgs e) => ListForms.OpenForms("Register", false, this, true);

        private void yt_Button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                new Thread(() => enter()).Start();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == (char)Keys.Enter;
        }
    }
}
