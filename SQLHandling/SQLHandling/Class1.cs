using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace SQLHandling
{
    public class DBConnection
    {
        private string path;
        private string file;
        private SQLiteConnection sqlite;
        private SQLiteCommand cmd;
        public DBConnection(string path_to_file, string file_name)
        {
            this.path = path_to_file;
            this.file = file_name;
        }

        public string Connecter()
        {
            try
            {
                sqlite = new SQLiteConnection("Data Source=" + this.path + "/" + this.file);
                sqlite.Open();
                this.cmd = sqlite.CreateCommand();
                Console.WriteLine("Database Reached");
                return "Database Reached";
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Can't reach database: " + ex);
                return "Cant Reach Database: " + ex;
            }
        }

        public DataTable Consulter(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                this.cmd.CommandText = query;
                SQLiteDataAdapter ad = new SQLiteDataAdapter(this.cmd);
                ad.Fill(dt);
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Can't reach database: " + ex);
            }
            this.sqlite.Close();
            return dt;
        }

        public List<DataTable> Consulter(List<string> querys, List<DataTable> datatables)
        {
            int count = 0;
            List<DataTable> result = new List<DataTable>();
            foreach (DataTable dt in datatables)
            {
                this.cmd.CommandText = querys[count];
                SQLiteDataAdapter ad = new SQLiteDataAdapter(this.cmd);
                ad.Fill(dt);
                result.Add(dt);
                count++;
            }
            return result;
        }
    }
}
