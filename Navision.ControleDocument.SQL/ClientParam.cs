using Navision.ControleDocument.SQL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Navision.ControleDocument.SQL
{
    public class ClientParam : Context, IDisposable
    {
        public ClientParam(string file) : base(file)
        {
            //_connect.Open();
        }

        public void Dispose()
        {
            _connect.Close();
        }

        public IQueryable<ClientParamModel> GetClientParams()
        {
            return _connect.Query<ClientParamModel>("select Client,URL from ClientParam").AsQueryable();           
        }
    }
}
