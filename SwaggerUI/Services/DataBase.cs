using System;
using System.Data;
using System.Data.SQLite;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SwaggerUI.Services
{
    public class DataBase
    {
        private SQLiteConnection sqlite_conn;
        public DataBase()
        {
            sqlite_conn = new SQLiteConnection("Data Source=extensions.db; Version = 3; New = True; Compress = True;");
            
        }
        ~DataBase()
        {
            if (sqlite_conn != null)
            {
                sqlite_conn.Close();
                sqlite_conn.Dispose();
            }
        }

        public bool IsInDataBase(string str_arg)
        {
            sqlite_conn.Open();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT count(*) from `urls` where value='{str_arg}'";

            sqlite_cmd.ExecuteNonQuery();


            DataTable dta1 = new DataTable();
            SQLiteDataAdapter dataadap = new SQLiteDataAdapter(sqlite_cmd);
            dataadap.Fill(dta1);
            //del(dta1);
            sqlite_conn.Close();


            if (dta1.Rows[0][0].ToString() == "0")
                return false;

            return true;
        }
        public void InsertData(string str_arg)
        {
            sqlite_conn.Open();
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = $"INSERT INTO `urls`(value) VALUES('{str_arg}'); ";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
        }

        public DataTable DataSource(int page, int pageSize)
        {
            sqlite_conn.Open();

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * from `urls` LIMIT {pageSize} OFFSET {(pageSize * (page - 1))}";

            sqlite_cmd.ExecuteNonQuery();

            DataTable dta1 = new DataTable();
            SQLiteDataAdapter dataadap = new SQLiteDataAdapter(sqlite_cmd);
            dataadap.Fill(dta1);
            //del(dta1);
            sqlite_conn.Close();

            return dta1;
        }
        public int GetCount()
        {
            sqlite_conn.Open();

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT count(*) from `urls`";

            sqlite_cmd.ExecuteNonQuery();

            DataTable dta1 = new DataTable();
            SQLiteDataAdapter dataadap = new SQLiteDataAdapter(sqlite_cmd);
            dataadap.Fill(dta1);
            //del(dta1);
            sqlite_conn.Close();

            return Convert.ToInt32(dta1.Rows[0][0].ToString());
        }
    }
}
