using Navision.ControleDocument.SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navision.ControleDocuments.Services.IServices
{
   public interface IVersionService
    {
        /// <summary>
        /// Get all Version
        /// </summary>
        /// <returns></returns>
        IQueryable<VersionModel> GetVersion();
        /// <summary>
        ///  Add new company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        int AddVersion(VersionModel version);
        /// <summary>
        /// Update Client
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        int UpdateVersion(VersionModel version);
        /// <summary>
        /// Delete Client
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        int DelVersion(VersionModel version);
    }
}
