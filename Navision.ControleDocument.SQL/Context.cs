//using Microsoft.Data.Sqlite;
using Navision.ControleDocument.SQL.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navision.ControleDocument.SQL
{
    public abstract class Context<T> where T : new()
    {
        protected SQLiteConnection _connect;

        protected T Repository { get; set; }

        public Context(string file, T repository)
        {
            Repository = repository;
            _connect = new SQLiteConnection(file);
        }
        /// <summary>
        /// Del a row
        /// </summary>
        /// <param name="objet"></param>
        /// <returns></returns>
        protected int Del(T objet)
        {
            return _connect.Delete(objet);
        }
        /// <summary>
        /// Get table
        /// </summary>
        /// <returns></returns>
        protected IQueryable<T> Get()
        {
            return _connect.Table<T>().AsQueryable();
        }
        /// <summary>
        /// Update row
        /// </summary>
        /// <param name="objet"></param>
        /// <returns></returns>
        protected int Update(T objet)
        {
            return _connect.Update(objet);
        }
        /// <summary>
        /// Add row
        /// </summary>
        /// <param name="objet"></param>
        /// <returns></returns>
        protected int Add(T objet)
        {
            return _connect.Insert(objet);
        }

    }
}
