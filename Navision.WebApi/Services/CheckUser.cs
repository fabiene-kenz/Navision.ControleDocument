using Navision.DB;
using Navision.Models;
using Navision.WebApi.Helpers;
using Navision.WebApi.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Navision.WebApi.Services
{
    public class CheckUser : ICheckUser,IDisposable
    {
        #region Properties
        private readonly Context _db;
        #endregion
        #region CTR
        public CheckUser()
        {
            _db = new Context();
        }
        #endregion
        public void Dispose()
        {
            _db.Dispose();
        }
        /// <summary>
        /// Check if User exist
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UserExist(UserModel user)
        {
           return _db.UsersMobile.Any(u => u.User_Name.Contains(user.UserName.GetUser()) && u.Password == user.Password);
        }

        public
    }
}