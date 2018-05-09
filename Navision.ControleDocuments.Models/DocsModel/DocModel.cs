using System;
using System.Collections.Generic;
using System.Text;

namespace Navision.ControleDocuments.Models.DocsModel
{
    public class DocModel
    {
        public int IdDoc { get; set; }
        public string DocName { get; set; }
        public DateTime DocDate { get; set; }
        public Boolean? DocSatut { get; set; }
    }
}
