using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Navision.ControleDocument.SQL.Models
{
    public class VersionModel
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public string Version { get; set; }
    }
}
