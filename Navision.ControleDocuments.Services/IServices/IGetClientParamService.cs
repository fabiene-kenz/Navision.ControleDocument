using Navision.ControleDocument.SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navision.ControleDocuments.Services.IServices
{
   public interface IGetClientParamService
    {
        IQueryable<ClientParamModel> GetClient();
    }
}
