using Navision.DB;
using Navision.Models;
using Navision.WebApi.App_Start;
using Navision.WebApi.Helpers;
using Navision.WebApi.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Navision.WebApi.Services
{
    public class CheckUserServices : ICheckUserServices, IDisposable
    {

        #region Properties
        private readonly Context _db;
        #endregion

        #region CTR
        public CheckUserServices()
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
            var name = user.UserName.GetUser();
            return _db.UsersMobile.Any(u => u.User_Name.ToLower().Contains(name.ToLower()) && u.Password == user.Password);
        }
        /// <summary>
        /// Check if user exist in Navision
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UserExistInNavision(UserModel user)
        {
            var name = user.UserName.GetUser();
            return _db.Users.Any(u => u.User_Name.ToLower().Contains(name.ToLower()));
        }
    }
}