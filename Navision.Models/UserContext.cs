using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navision.Models
{
    public class UserContext
    {
        public string Token { get; set; }
        public string UserAgent { get; set; }
        public string Ip { get; set; }
    }
}
