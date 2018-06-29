using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Navision.ControleDocuments.Services.IServices
{
    public interface IZipService
    {
        Task<string> ZipLogs(string folderPath);
    }
}
