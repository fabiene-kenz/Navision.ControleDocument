//using Microsoft.Data.Sqlite;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Navision.ControleDocument.SQL
{
    public class Context
    {
        protected SQLiteConnection _connect;
        public Context(string file)
        {
            //_connect = new SQLiteConnection("Filename=" + file);
            _connect = new SQLiteConnection(file);
        }
    }
}
