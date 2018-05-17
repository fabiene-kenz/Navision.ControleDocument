
using Microsoft.Data.Sqlite;
using SQLite.Net;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLLite
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = File.Create("NavisionDocumentDB.sqlite3");
            fs.Close();
            //var platform = new ISQLitePlatform();
            //var co=new  SQLiteConnectionString("NavisionDocumentDB.sqlite",false);
            // SQLiteConnection connect = new SQLiteConnection(null, "NavisionDocumentDB.sqlite");
            // connect.CreateTable()
            // var t= new  SQLiteConnection.CreateFile("MyDatabase.sqlite");
            using (var connection = new SqliteConnection("Filename=NavisionDocumentDB.sqlite3"))
            {
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
                SQLitePCL.Batteries_V2.Init();
                connection.Open();

                var createCommand = connection.CreateCommand();
                createCommand.CommandText =
                @"
                CREATE TABLE ClientParam (
                    Client TEXT,
                    URL TEXT
                );
                INSERT INTO ClientParam
                VALUES ('e-Kenz','https://mobilenavision.azurewebsites.net'),
                       ('BGL','https://mobilenavisionbgl.azurewebsites.net');
            ";
                createCommand.ExecuteNonQuery();

                // Without this, the query returns one since the built-in NOCASE collation only
                // handles ASCII characters (A-Z)
                connection.CreateCollation("NOCASE", (x, y) => string.Compare(x, y, ignoreCase: true));

                var queryCommand = connection.CreateCommand();
                queryCommand.CommandText =
                @"
                SELECT count()
                FROM ClientParam
                WHERE Client = 'e-Kenz' COLLATE NOCASE
            ";
                var count = (long)queryCommand.ExecuteScalar();

                Console.WriteLine($"Results: {count}");
            }
            string dbUWP = ConfigurationManager.AppSettings["dbUWP"];
            File.Copy("NavisionDocumentDB.sqlite3", dbUWP, true);
            string dbAndroid = ConfigurationManager.AppSettings["dbAndroid"];
            File.Copy("NavisionDocumentDB.sqlite3", dbAndroid, true);

        }
    }
}
