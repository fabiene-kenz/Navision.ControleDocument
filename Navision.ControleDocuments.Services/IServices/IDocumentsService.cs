using Navision.ControleDocuments.Models.DocsModel;
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
    }
}
