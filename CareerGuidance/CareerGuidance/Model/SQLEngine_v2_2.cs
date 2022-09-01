using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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

    const string sql_BD_defoult = "CareerGuidance.sqlite";
    string Puth;
    string NameDB = "Polzovateli.mdf";

    #region SQLite

    private SQLiteConnection sql_connect = new SQLiteConnection();
    private SQLiteCommand sql_command;
    private SQLiteDataAdapter sql_dataAdapter;
    private DataTable sql_dataTable = new DataTable();
    private readonly Dictionary<string, DataTable> dictionaryTable = new Dictionary<string, DataTable> { };
    public string NameDataBase { get; set; }
    public DataSet ds = new DataSet();

    /// <summary>Создать новую базу данных(True-да:False-нет)
    /// 
    /// </summary>
    public bool CreateNewDataBase { get; set; }
    /// <summary>Открывает соединение с базой данных
    /// 
    /// </summary>
    private void Connect(string sql_BD)
    {
        CreateNewDataBase = false;
        NameDataBase = sql_BD;

        sql_connect = new SQLiteConnection();
        sql_connect.ConnectionString = "Data Source=" + NameDataBase + ";Version=3;New=" + CreateNewDataBase.ToString() + ";Compress=True;UTF8Encoding=True";
        sql_connect.Open();

        const string sqlString = "PRAGMA foreign_keys = ON;";
        SQLiteCommand command = new SQLiteCommand(sqlString, sql_connect);
        command.ExecuteNonQuery();
    }
    #region Запросы SQLite

    /// <summary>Выполняет запрос на языке Sqlite
    /// 
    /// </summary>
    /// <param name="sql_str">Скрипт запроса на выполнение</param>
    /// <param name="errorShow">Отображать ошибку при выполнение запроса? По умолчанию(нет)</param>
    public void Execute(string sql_str, bool errorShow = true, string sql_BD = sql_BD_defoult)
    {
        try
        {
            Connect(sql_BD);
            sql_command = sql_connect.CreateCommand();
            sql_command.CommandText = sql_str;
            sql_command.ExecuteNonQuery();
            sql_connect.Close();
        }
        catch (Exception ex) { if (errorShow) MessageBox.Show(ex.Message); }
    }
    /// <summary>Выполняет запрос на языке Sqlite и возвращает виртуальную таблицу
    /// 
    /// </summary>
    /// <param name="StringQuery">Строка запроса на выполнение</param>
    /// /// <param name="errorShow">Отображать ошибку при выполнение запроса? По умолчанию(да)</param>
    public DataTable RunQuery(string sql_str, bool errorShow = true, string sql_BD = sql_BD_defoult)
    {
        try
        {
            Connect(sql_BD);
            if (!dictionaryTable.ContainsKey(sql_str))
            {
                sql_command = sql_connect.CreateCommand();
                sql_dataAdapter = new SQLiteDataAdapter(sql_str, sql_connect);
                sql_dataTable = new DataTable();
                sql_dataAdapter.Fill(sql_dataTable);
            }
            else
            {
                sql_dataAdapter.Fill(dictionaryTable[sql_str]);
            }
        }
        catch (Exception ex) { if (errorShow) MessageBox.Show(ex.Message); }
        return sql_dataTable;
    }
    #endregion


    #region Фото-Файлы

    /// <summary>
    /// Выполняет запрос на языке Sqlite и помещает изображение в бд<br/>
    /// Пример вызова метода(sql.ExecuteImageDownload(pictureBox1, "@Фото", $"UPDATE Пользователи SET Фото=(@Фото) WHERE Код_пользователя=1;");)
    /// </summary>
    /// <param name="PictureBox"></param>
    /// <param name="paramImage"></param>
    /// <param name="sql_str">UPDATE Пользователи SET Фото=(@Фото) WHERE Код_пользователя=1;</param>
    /// <param name="errorShow"></param>
    Array image;
    public void ExecuteImageDownload(PictureBox PictureBox, string paramImage, string sql_str, bool errorShow = true, string sql_BD = sql_BD_defoult)
    {
        try
        {
            if (PictureBox.Image == null) { MessageBox.Show("Изображение не найдено"); return; }
            MemoryStream ms = new MemoryStream();
            PictureBox.Image.Save(ms, ImageFormat.Png);
            image = ms.ToArray();
            Connect(sql_BD);
            sql_command = sql_connect.CreateCommand();
            sql_command.CommandText = sql_str;
            sql_command.Parameters.Add(paramImage, DbType.Binary, 8000).Value = image;
            sql_command.ExecuteNonQuery();
            sql_connect.Close();
        }
        catch (Exception ex) { if (errorShow) MessageBox.Show(ex.Message); }
    }
    /// <summary>
    ///  Выполняет запрос на языке Sqlite и возвращает в типе данных Image бд<br/>
    /// $"SELECT Фото FROM Пользователи WHERE Код_пользователя=1;"
    /// </summary>
    /// <param name="sql_str"></param>
    /// <param name="errorShow"></param>
    /// <returns></returns>
    public Image ExecuteImageOpen(string sql_str, bool errorShow = true, string sql_BD = sql_BD_defoult)
    {
        Image a2 = null;
        try
        {
            Connect(sql_BD);
            sql_command = sql_connect.CreateCommand();
            sql_command.CommandText = sql_str;
            IDataReader rdr = sql_command.ExecuteReader();

            while (rdr.Read())
            {
                byte[] a = (byte[])rdr[0];
                a2 = ByteToImage(a);
            }
            sql_connect.Close();
        }
        catch (Exception ex) { if (errorShow) MessageBox.Show(ex.Message); }
        return a2;
    }
    public Image ByteToImage(byte[] imageBytes)
    {
        // Convert byte[] to Image
        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
        ms.Write(imageBytes, 0, imageBytes.Length);
        Image image = new Bitmap(ms);
        return image;
    }
    #endregion


    #endregion

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

