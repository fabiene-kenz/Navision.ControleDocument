using Navision.ControleDocuments.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Navision.ControleDocuments.Services.IServices
{
    /// <summary>
    /// Connect DB Navision
    /// </summary>
    public interface IUserLoginService
    {
        /// <summary>
        /// Get Token for user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<string> GetToken(UserModel user);
        /// <summary>
        /// Add a new entry for the app
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> AddUser(UserModel user);
    }
}
