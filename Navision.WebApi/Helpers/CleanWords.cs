using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Navision.WebApi.Helpers
{
    public static class CleanWords
    {
        /// <summary>
        /// Get user name with split @
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string GetUser(this string username)
        {
            string name = username.Split('@')[0];
            return name;
        }
        /// <summary>
        /// Get string array for User in Navision
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string[] SplitUser(this string user)
        {
            var test= user.Split(new string[] { @"\\" }, StringSplitOptions.None); 
            return user.Split(new string[] { @"\\" }, StringSplitOptions.None);
        }
    }
}