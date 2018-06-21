using Navision.ControleDocuments.Models.UserModels;
using Navision.ControleDocuments.Services.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Navision.ControleDocuments.Services.Services
{
    /// <summary>
    /// Connect DB Navision
    /// </summary>
    public class UserLoginService : ConnectService, IUserLoginService
    {
        public UserLoginService() : base()
        {

        }
        public UserLoginService(UserModel user) : base(user)
        {

        }
        /// <summary>
        /// Add a new entry for the app
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> AddUser(UserModel user)
        {
            string Uri = @"/Login/GetNewAccess";
            var objUser = JsonConvert.SerializeObject(user);
            var content = new StringContent(objUser, Encoding.UTF8, "application/json");
            try
            {
                var reponse = await httpClient.PostAsync(Uribase + Uri, content);
                var result = await reponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<bool>(result);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Get Token for user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> GetToken(UserModel user)
        {
            string Uri = @"/Login/ConnectUser";
            var objUser = JsonConvert.SerializeObject(user);
            var content = new StringContent(objUser, Encoding.UTF8, "application/json");
            try
            {
                var reponse = await httpClient.PostAsync(Uribase + Uri, content);
                var result = await reponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<string>(result);
            }
            catch(HttpRequestException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
