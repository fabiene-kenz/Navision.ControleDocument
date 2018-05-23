using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Navision.ControleDocument.SQL.Models
{
   public class Companies
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string Url { get; set; }
        public string Domain { get; set; }
    }
}
