using Navision.DB;
using Navision.Models;
using Navision.WebApi.App_Start;
using Navision.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Navision.WebApi.Helpers
{
    public class UserConnect
    {
        /// <summary>
        /// Get User connected
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ip"></param>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static UserMobile GetUserConnected(string token, string ip, string userAgent)
        {
            bool result = false;
            try
            {
                var tokenKey = HttpUtility.HtmlEncode(token).Replace("&quot;", string.Empty).Trim();
                // Base64 decode the string, obtaining the token:username:timeStamp.
                string key = Encoding.UTF8.GetString(Convert.FromBase64String(tokenKey));
                // Split the parts.
                string[] parts = key.Split(new char[] { ':' });
                if (parts.Length == 3)
                {
                    // Get the hash message, username, and timestamp.
                    string hash = parts[0];
                    string username = parts[1];
                    long ticks = long.Parse(parts[2]);
                    DateTime timeStamp = new DateTime(ticks);

                    // Check if user in UsersMobile Table
                    Context c = new Context();
                    var userConnect = username.Split('@')[0];
                    var user = c.UsersMobile.FirstOrDefault(u => u.User_Name.ToLower().Contains(userConnect));

                    if (user.User_Name.ToLower().Contains(userConnect))
                    {
                        string password = user.Password;
                        // Hash the message with the key to generate a token.
                        string computedToken = GenerationToken.GenerateToken(username, password, ip, userAgent, ticks);
                        // Compare the computed token with the one supplied and ensure they match.
                        result = (tokenKey == computedToken);
                        if (result)
                        {
                            return user;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new UserMobile();
        }
        /// <summary>
        /// Get context's info
        /// </summary>
        /// <param name="httpcontext"></param>
        /// <returns></returns>
        public static UserContext GetContext(HttpContextBase httpcontext)
        {
            var token = HttpContext.Current.Request.Headers["Authorization"].Replace("Bearer", string.Empty).Replace(@"\", string.Empty);
            var userAgent = HttpContext.Current.Request.Headers["User-Agent"];
            var ip = HttpContext.Current.Request.UserHostAddress;

            return new UserContext { Ip = ip, Token = token, UserAgent = userAgent };
        }
    }
}