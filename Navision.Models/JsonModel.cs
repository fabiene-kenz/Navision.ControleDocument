using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navision.Models
{
    public class JsonModel
    {
        public string Version { get; set; }
        public List<Company> Companies { get; set; }
    }

    public class Company
    {
        public string CompanyName { get; set; }
        public string Url { get; set; }
        public string Domain { get; set; }
    }
}
