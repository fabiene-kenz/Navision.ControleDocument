
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
            using (var connection = new SqliteConnection("Filename=NavisionDocumentDB.sqlite3"))
            {
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
                SQLitePCL.Batteries_V2.Init();
                connection.Open();

                var createCommand = connection.CreateCommand();
                createCommand.CommandText =
                @"
                CREATE TABLE Companies (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    CompanyName TEXT,
                    Url TEXT
                );
                INSERT INTO Companies
                VALUES (1,'e-Kenz','https://mobilenavision.azurewebsites.net'),
                       (2,'BGL','https://mobilenavisionbgl.azurewebsites.net');

                CREATE TABLE VersionTable (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Version TEXT
                );
                INSERT INTO VersionTable
                VALUES (1,'1.0.0.0');
            ";
                createCommand.ExecuteNonQuery();
            }
            string dbUWP = ConfigurationManager.AppSettings["db"];
            File.Copy("NavisionDocumentDB.sqlite3", dbUWP, true);

        }
    }
}
