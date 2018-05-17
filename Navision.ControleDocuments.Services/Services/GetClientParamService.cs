using Navision.ControleDocument.SQL;
using Navision.ControleDocument.SQL.Models;
using Navision.ControleDocuments.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navision.ControleDocuments.Services.Services
{
    public class GetClientParamService : IGetClientParamService
    {
        private readonly string _file;
        public GetClientParamService(string file)
        {
            _file = file;
        }
        public IQueryable<ClientParamModel> GetClient()
        {
            using (ClientParam c = new ClientParam(_file))
            {
              return  c.GetClientParams();
            }
        }
    }
}
