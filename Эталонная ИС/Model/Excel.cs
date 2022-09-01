using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Model
{
    internal class Excel
    {
        SQLEngine sql = new SQLEngine();
        public void inputExcel()
        {
            DataTable dataTable, dataTable2 = new DataTable();
            int rCnt, cCnt;
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Excel (*.XLSX)|*.XLSX";
            opf.ShowDialog();
            DataTable tb = new DataTable();
            string filename = opf.FileName;
            if (opf.FileName == "")
                return;
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            Microsoft.Office.Interop.Excel.Range ExcelRange;
            ExcelWorkBook = ExcelApp.Workbooks.Open(filename, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false,
                false, 0, true, 1, 0);
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);
            ExcelRange = ExcelWorkSheet.UsedRange;
            string[] str = new string[ExcelRange.Columns.Count];
            object[,] data = ExcelRange.Value;
            for (rCnt = 2; rCnt <= ExcelRange.Rows.Count - 1; rCnt++)
            {
                for (cCnt = 1; cCnt <= ExcelRange.Columns.Count; cCnt++)
                {

                    if (data[rCnt, cCnt] is DateTime)
                        str[cCnt - 1] = Convert.ToDateTime(data[rCnt, cCnt]).ToString("yyyy.MM.dd");
                    else
                        str[cCnt - 1] = data[rCnt, cCnt].ToString();
                    
                }
               /* try
                {*/
                    dataTable = sql.MSRunQuery($"SELECT Код_категории FROM Категории WHERE Статус = N'{str[0]}'");
                    dataTable2 = sql.MSRunQuery($"SELECT Код_группы FROM Группы WHERE Группа = N'{str[1]}'");
                    str[7] += " Семестр";
                    sql.MSExecute($"INSERT INTO Обучающиеся VALUES ({dataTable.Rows[0][0]},{dataTable2.Rows[0][0]},N'{str[2]}',N'{str[3]}',N'{str[4]}','{Convert.ToDateTime(str[5]):yyyy-MM-dd}','{Convert.ToDateTime(str[6]):yyyy-MM-dd}',N'{str[7]}');", true);

              /*  }
                catch (Exception e)
                {

                    MessageBox.Show("Импорт был закончен с ошибкой!Проверте коретность данных Excel\n " + e.Message);
                    ExcelWorkBook.Close(true, null, null);
                    ExcelApp.Quit();
                    releaseObject(ExcelWorkSheet);
                    releaseObject(ExcelWorkBook);
                    releaseObject(ExcelApp);
                    return;
                }*/
            }
            ExcelWorkBook.Close(true, null, null);
            ExcelApp.Quit();
            releaseObject(ExcelWorkSheet);
            releaseObject(ExcelWorkBook);
            releaseObject(ExcelApp);
            MessageBox.Show("Импорт выполнен");
        }





        public DataGridView excelDataTable(string[] columns, DataGridView dataGridView)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Excel (*.XLSX)|*.XLSX";
            opf.ShowDialog();
            DataTable tb = new DataTable();
            string filename = opf.FileName;
            if (opf.FileName == "")
                return null;

            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            Microsoft.Office.Interop.Excel.Range ExcelRange;
            ExcelWorkBook = ExcelApp.Workbooks.Open(filename, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false,
                false, 0, true, 1, 0);
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);
            ExcelRange = ExcelWorkSheet.UsedRange;
            object[,] str = new object[ExcelRange.Rows.Count - 1, ExcelRange.Columns.Count - 1];
            object[,] data = ExcelRange.Value;
            ExcelWorkBook.Close(true, null, null);
            ExcelApp.Quit();
            releaseObject(ExcelWorkSheet);
            releaseObject(ExcelWorkBook);
            releaseObject(ExcelApp);
            return summary(data, columns, dataGridView);
        }
        public DataGridView summary(object[,] matr, string[] columns, DataGridView dataGridView)
        {

            foreach (var item in columns)
                dataGridView.Columns.Add(item, item);
            dataGridView.RowCount = matr.GetLength(0) - 1;
            for (int i = 0; i < matr.GetLength(0) - 1; i++)
                for (int j = 0; j < matr.GetLength(1); j++)
                {
                    if (matr[i + 1, j + 1] is DateTime)
                        dataGridView.Rows[i].Cells[j].Value = Convert.ToDateTime(matr[i + 1, j + 1]).ToString("yyyy.MM.dd");
                    else
                        dataGridView.Rows[i].Cells[j].Value = matr[i + 1, j + 1];
                }

            return dataGridView;

        }

        private void releaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
