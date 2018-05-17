using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creation_Json.Model
{
    public class ConfigFile
    {
        public string Version { get; set; }
        public List<Company> Companies { get; set; }
    }
}
