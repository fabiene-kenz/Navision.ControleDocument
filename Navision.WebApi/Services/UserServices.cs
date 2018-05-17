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
    public class UserServices : IUserServices
    {
        #region Proprieties
        private readonly Context _db;
        #endregion
        #region CTR
        public UserServices()
        {
            _db = new Context();
        }
        #endregion
        public bool AddUSerInMobile(UserModel user)
        {
            var userName = user.UserName.GetUser().ToLower();
            if (_db.Users.Any(u => u.User_Name.ToLower().Contains(userName)))
            {
                try
                {
                    var userNav = _db.Users.FirstOrDefault(u => u.User_Name.ToLower().Contains(userName));
                    _db.UsersMobile.Add(new UserMobile
                        {
                            Application_ID = userNav.Application_ID,
                            Change_Password = userNav.Change_Password,
                            Authentication_Email = userNav.Authentication_Email,
                            Contact_Email = userNav.Contact_Email,
                            Exchange_Identifier = userNav.Exchange_Identifier,
                            Expiry_Date = userNav.Expiry_Date,
                            Full_Name = userNav.Full_Name,
                            Password = user.Password,
                            License_Type = userNav.License_Type,
                            State = userNav.State,
                            timestamp = userNav.timestamp,
                            User_Name = userNav.User_Name,
                            User_Security_ID = userNav.User_Security_ID,
                            Windows_Security_ID = userNav.Windows_Security_ID
                        }
                    );
                    _db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}