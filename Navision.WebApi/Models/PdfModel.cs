using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Navision.WebApi.Models
{
    public class PdfModel
    {
        public string fileName { get; set; }
        public byte[] fileStream { get; set; }
    }
}