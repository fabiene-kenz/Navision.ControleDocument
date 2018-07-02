using Navision.ControleDocuments.Models.DocsModel;
using Navision.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Navision.ControleDocuments.Services.IServices
{
    /// <summary>
    /// Connect DB Navision
    /// </summary>
    public interface IDocumentsService
    {
        Task<List<DocModel>> GetDocuments();
        Task<List<DocumentValuesModel>> GetValueToCheck(DocModel document);
        Task<bool> ApproveOrRejectDocument(DocModel document);
    }
}
