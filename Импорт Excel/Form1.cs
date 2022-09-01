using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Excel excel = new Excel();
            excel.excelDataTable(new string[3] { "1", "2", "3" }, dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Excel excel = new Excel();
            excel.inputExcel();
            up();
        }
        SQLEngine sql = new SQLEngine();
        private void Form1_Load(object sender, EventArgs e)
        {
            up();
        }
        void up()
        {
            DataTable dt = sql.MSRunQuery("SELECT * FROM Test");
            dataGridView1.DataSource = dt;
        }
    }
}
