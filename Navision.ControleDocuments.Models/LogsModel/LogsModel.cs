using System;
using System.Collections.Generic;
using System.Text;

namespace Navision.ControleDocuments.Models.LogsModel
{
    public class LogsModel
    {
        public string fileName { get; set; }
        public byte[] fileContent { get; set; }
    }
}
