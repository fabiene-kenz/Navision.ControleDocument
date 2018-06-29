using System;
using System.Collections.Generic;
using System.Text;

namespace Navision.ControleDocuments.Models.DocsModel
{
    public class DocModel
    {
        public string IdDoc { get; set; }
        public string DocName { get; set; }
        public string Url { get; set; }
        public DateTime DocDate { get; set; }
        public Boolean? DocSatut { get; set; } 

        public string VendorInvoiceNo { get; set; }
        public string VendorName { get; set; }
        public string VendorShipNo { get; set; }
        public string DocumentDate { get; set; }

        public bool IsApprove { get; set; }
    }
}
