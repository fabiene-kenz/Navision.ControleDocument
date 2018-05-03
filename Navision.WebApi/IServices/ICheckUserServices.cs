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
        /// <summary>
        ///  /// Check if User exist
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool UserExist(UserModel user);
        /// <summary>
        /// Check if user exist in Navision
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool UserExistInNavision(UserModel user);
    }
}
