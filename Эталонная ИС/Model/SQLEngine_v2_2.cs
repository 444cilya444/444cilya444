using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;

/// <summary>Класс для работы с SQlite
/// 
/// </summary>
public class SQLEngine
{

    string Puth;
    string NameDB = "Database1.mdf";

    #region MSSql
    private SqlConnection mssql_connect = new SqlConnection();
    private SqlCommand mssql_command;
    private SqlDataAdapter mssql_dataAdapter;
    private DataTable mssql_dataTable = new DataTable();
    public DataSet msds = new DataSet();

    private void MSConnect()
    {
        string s = AppDomain.CurrentDomain.BaseDirectory;
        Puth = $@"{s}{NameDB}";
        mssql_connect = new SqlConnection();
        mssql_connect.ConnectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Puth};Integrated Security=True";
        try
        {
            mssql_connect.Open();
        }
        catch (Exception e)
        {       
            if (mssql_connect.State == ConnectionState.Closed)
                MessageBox.Show("Ошибка подключеня к базе данных!\nПуть подключения: \n" + Puth + "\nТекст ошибки: "+ e.Message);
        }
    }
    public DataTable MSRunQuery(string sql_str, bool errorShow = false, string mssql_BD = "Default")
    {     
        try
        {
            MSConnect();
            if (mssql_BD == "Default")
                mssql_BD = Puth;           
            mssql_command = mssql_connect.CreateCommand();
            mssql_dataAdapter = new SqlDataAdapter(sql_str, mssql_connect);
            mssql_dataTable = new DataTable();
            mssql_dataAdapter.Fill(mssql_dataTable);
            mssql_connect.Close();
        }
        catch (Exception ex) { if (errorShow) MessageBox.Show(ex.Message); }
        return mssql_dataTable;
    }

    public void MSExecute(string sql_str, bool errorShow = true, string mssql_BD = "Default")
    {
        try
        {
            MSConnect();
            if (mssql_BD == "Default")
                mssql_BD = Puth;
            mssql_command = mssql_connect.CreateCommand();
            mssql_command.CommandText = sql_str;
            mssql_command.ExecuteNonQuery();
            mssql_connect.Close();
        }
        catch (Exception ex) { if (errorShow) MessageBox.Show(ex.Message); }
    }
    #endregion


}

