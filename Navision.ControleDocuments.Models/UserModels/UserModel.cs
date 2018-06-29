using System;
using System.Collections.Generic;
using System.Text;

namespace Navision.ControleDocuments.Models.UserModels
{
   public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string URL { get; set; }
    }
}
