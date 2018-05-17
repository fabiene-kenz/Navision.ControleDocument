using Navision.DB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Navision.WebApi.App_Start
{
    public class GenerationToken
    {
        private const string _alg = "HmacSHA256";
        private const string _salt = "rz8LuOtFBXphj9WQfvFh";
        private static int _expirationMinutes = int.Parse(ConfigurationManager.AppSettings["ExpirationMinutes"]);
        /// <summary>
        /// Generate Token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="ip"></param>
        /// <param name="userAgent"></param>
        /// <param name="ticks"></param>
        /// <returns></returns>
        public static string GenerateToken(string username, string password, string ip, string userAgent, long ticks)
        {
            string hash = string.Join(":", new string[] { username, ip, userAgent, ticks.ToString() });
            string hashLeft = "";
            string hashRight = "";
            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                hmac.Key = Encoding.UTF8.GetBytes(GetHashedPassword(password));
                hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));
                hashLeft = Convert.ToBase64String(hmac.Hash);
                hashRight = string.Join(":", new string[] { username, ticks.ToString() });
            }
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashLeft, hashRight)));
        }
        /// <summary>
        /// Hash password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetHashedPassword(string password)
        {
            string key = string.Join(":", new string[] { password, _salt });
            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                // Hash the key.
                hmac.Key = Encoding.UTF8.GetBytes(_salt);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(key));
                return Convert.ToBase64String(hmac.Hash);
            }
        }
        /// <summary>
        /// Check if token is valid
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ip"></param>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static bool IsTokenValid(string token, string ip, string userAgent)
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
                    string username = parts[1].Split('@')[0];
                    long ticks = long.Parse(parts[2]);
                    DateTime timeStamp = new DateTime(ticks);
                    // Ensure the timestamp is valid.
                    bool expired = Math.Abs((DateTime.UtcNow - timeStamp).TotalMinutes) > _expirationMinutes;
                    if (!expired)
                    {
                        // Check if user in UsersMobile Table
                        Context c = new Context();
                        var user = c.UsersMobile.FirstOrDefault(u => u.User_Name.Contains(username));

                        if (user.User_Name == username)
                        {
                            string password = user.Password;
                            // Hash the message with the key to generate a token.
                            string computedToken = GenerateToken(username, password, ip, userAgent, ticks);
                            // Compare the computed token with the one supplied and ensure they match.
                            result = (tokenKey == computedToken);
                        }
                    }
                }
            }
            catch
            {
            }
            return result;
        }
    }
}