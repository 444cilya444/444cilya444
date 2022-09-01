using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public struct stock
        {
            public string Команда_1 { get; set; }
            public string Команда_2 { get; set; }
            public int Счет_1_команды { get; set; }
            public int Счет_2_команды { get; set; }
        };
        public struct stock1
        {
            public string Команда { get; set; }
            public int Пропущенные { get; set; }
            public int Забитые { get; set; }
        };
        public struct stocSort
        {
            public string Команда { get; set; }
            public int Пропущенные { get; set; }
            public int Забитые { get; set; }
        };
        public Form1()
        {
            InitializeComponent();
        }
        public System.Collections.IEnumerable ItemsSource { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            List<stock> stockList = new List<stock>();
            using (StreamReader sr = new StreamReader("TestFile.txt", Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {


                    var parsed = line.Split('|'); //Делим строку по символу &, например                   
                    stockList.Add(new stock()
                    {
                        Команда_1 = parsed[0],
                        Команда_2 = parsed[1],
                        Счет_1_команды = Convert.ToInt32(parsed[2]),
                        Счет_2_команды = Convert.ToInt32(parsed[3])

                    });
                }
            }
            dataGridView1.DataSource = stockList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = SortLis.Values.ToList();
        }

        int schet;
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            schet = Convert.ToInt32((dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value).ToString());

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            List<stock1> stockList = new List<stock1>();
            foreach (var parse in SortLis.Values)
            {
                if (parse.Пропущенные == schet)
                {
                    stockList.Add(new stock1()
                    {
                        Команда = parse.Команда,
                        Пропущенные = parse.Пропущенные,
                        Забитые = parse.Забитые
                    });
                }
            }
            stockList.Sort((y, x) => x.Забитые.CompareTo(y.Забитые));
            dataGridView1.DataSource = stockList;
        }
        Dictionary<string, stocSort> SortLis = new Dictionary<string, stocSort>();
        private void Form1_Load(object sender, EventArgs e)
        {
            using (StreamReader sr = new StreamReader("TestFile.txt", Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var parsed = line.Split('|');
                    if (!SortLis.ContainsKey(parsed[0]))
                    {
                        SortLis.Add(parsed[0], new stocSort()
                        {
                            Команда = parsed[0],
                            Пропущенные = Convert.ToInt32(parsed[2]),
                            Забитые = Convert.ToInt32(parsed[3])
                        });
                    }
                    else
                    {
                        SortLis[parsed[0]] = new stocSort()
                        {
                            Команда = parsed[0],
                            Пропущенные = SortLis[parsed[0]].Пропущенные + Convert.ToInt32(parsed[2]),
                            Забитые = SortLis[parsed[0]].Пропущенные + Convert.ToInt32(parsed[3])
                        };
                    }

                    if (!SortLis.ContainsKey(parsed[1]))
                    {
                        SortLis.Add(parsed[1], new stocSort()
                        {
                            Команда = parsed[1],
                            Пропущенные = Convert.ToInt32(parsed[3]),
                            Забитые = Convert.ToInt32(parsed[2])

                        });
                    }
                    else
                    {
                        SortLis[parsed[1]] = new stocSort()
                        {
                            Команда = parsed[1],
                            Пропущенные = SortLis[parsed[1]].Пропущенные + Convert.ToInt32(parsed[3]),
                            Забитые = SortLis[parsed[1]].Пропущенные + Convert.ToInt32(parsed[2])
                        };
                    }
                }
            }
        }



    }
}
