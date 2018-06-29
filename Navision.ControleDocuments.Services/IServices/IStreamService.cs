using Navision.ControleDocuments.Models.DocsModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Navision.ControleDocuments.Services.IServices
{
   public interface IStreamService
    {
        Task<List<PdfModel>> GetPdfFile(DocModel pdffilepath);
        Task<string> CleanFolder(DocModel doc);
        Task<bool> SendLogs(string LogsFilePath);
    }
}
