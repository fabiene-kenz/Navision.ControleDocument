using Navision.ControleDocument.SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navision.ControleDocuments.Services.IServices
{
   public interface IGetClientParamService
    {
        /// <summary>
        /// Get all Companies
        /// </summary>
        /// <returns></returns>
        IQueryable<Companies> GetClient();
        /// <summary>
        ///  Add new company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        int AddClient(Companies company);
        /// <summary>
        /// Update Client
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        int UpdateClient(Companies company);
        /// <summary>
        /// Delete Client
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        int DelClient(Companies company);
    }
}
