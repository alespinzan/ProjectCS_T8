using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace DATAmanager
{
    public class gestorBBDD
    {
        SQLiteConnection cnx;

        public void Open()
        {
            // The path now includes the "BBDD" folder.
            string relativePath = Path.Combine("BBDD", "FlrUsersBBDD.db");
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(baseDirectory, relativePath);

            string dataSource = $"Data Source={dbPath};Version=3;";
            this.cnx = new SQLiteConnection(dataSource);

            try
            {
                this.cnx.Open();
                // Create the table if it doesn't exist
                string createTableQuery = "CREATE TABLE IF NOT EXISTS users (id TEXT PRIMARY KEY, password TEXT NOT NULL)";
                using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, cnx))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to or initializing database: {ex.Message}");
                throw;
            }
        }
        
        public void Close()
        {
            if (this.cnx != null && this.cnx.State != ConnectionState.Closed)
            {
                this.cnx.Close();
            }
        }

        public void RegisterUser(string username, string password)
        {
            // Using parameters to prevent SQL injection
            string query = "INSERT INTO users (id, password) VALUES (@id, @password)";

            using (SQLiteCommand cmd = new SQLiteCommand(query, cnx))
            {
                cmd.Parameters.AddWithValue("@id", username);
                cmd.Parameters.AddWithValue("@password", password);
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine("filas insertadas:" + rows);
            }
        }


        // Métodos de guet id y passwor, algoritmo de validación
        public string GetUserid(string Username)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM users where id = '" + Username + "'";
            SQLiteDataAdapter adp = new SQLiteDataAdapter(sql, cnx);
            int rows = adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["id"].ToString();
            }

            return null;
        }

        public string GetPassword(string username)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT password FROM users where id = '" + username + "'";
            SQLiteDataAdapter adp = new SQLiteDataAdapter(sql, cnx);
            int rows = adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["password"].ToString();
            }

            return null;
        }
    }
}
