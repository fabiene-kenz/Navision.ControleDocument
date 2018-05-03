using Navision.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navision.WebApi.IServices
{
   public interface ICheckUser
    {
        bool UserExist(UserModel user);
    }
}
