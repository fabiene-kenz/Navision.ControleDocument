using Navision.ControleDocument.SQL.Services;
using Navision.ControleDocument.SQL.Models;
using Navision.ControleDocuments.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Navision.ControleDocument.SQL.IServices;

namespace Navision.ControleDocuments.Services.Services
{
    public class GetClientParamService : IGetClientParamService
    {
        private readonly string _file;
        public GetClientParamService(string file)
        {
            _file = file;
        }
        /// <summary>
        /// Add Company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public int AddClient(Companies company)
        {
            using (ClientParam<Companies> c = new ClientParam<Companies>(_file))
            {
                return c.AddNewCompany(company);
            }
        }
        /// <summary>
        /// Del a Company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public int DelClient(Companies company)
        {
            using (ClientParam<Companies> c = new ClientParam<Companies>(_file))
            {
                return c.DelCompany(company);
            }
        }
        /// <summary>
        /// Get all companies
        /// </summary>
        /// <returns></returns>
        public IQueryable<Companies> GetClient()
        {
            ClientParam<Companies> c = new ClientParam<Companies>(_file);
            return  c.GetClientParams();
        }
        /// <summary>
        /// Update Company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public int UpdateClient(Companies company)
        {
            using (ClientParam<Companies> c = new ClientParam<Companies>(_file))
            {
                return c.UpdateCompany(company);
            }
        }
    }
}
