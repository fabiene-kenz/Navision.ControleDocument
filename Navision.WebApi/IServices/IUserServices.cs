using Navision.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navision.WebApi.IServices
{
   public interface IUserServices
    {
        /// <summary>
        /// Add user in user mobile
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool AddUSerInMobile(UserModel user);
    }
}
