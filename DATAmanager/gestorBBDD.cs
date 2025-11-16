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

            //users (id text primarykey, password text);

            try
            {
                this.cnx.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to database: {ex.Message}");
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

        public void InsertUser(string username, string password)
        {
            string query = "INSERT INTO users VALUES('" + username + "','" + password+"')";

            SQLiteCommand cmd = new SQLiteCommand(query, cnx);
            int rows = cmd.ExecuteNonQuery();

            Console.WriteLine("filas insertadas:" + rows);
        }

    
    }
}
