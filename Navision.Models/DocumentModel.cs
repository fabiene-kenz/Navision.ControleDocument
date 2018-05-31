using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navision.Models
{
    public class DocumentModel
    {
        public int IdDoc { get; set; }
        public string DocName { get; set; }
        public DateTime DocDate { get; set; }
        public Boolean? DocSatut { get; set; }
    }
}
