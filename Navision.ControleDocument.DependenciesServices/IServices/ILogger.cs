using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Navision.ControleDocument.DependenciesServices.IServices
{
    public interface ILogger
    {
        Task WriteLog(Exception ex);
        string GetLogFolder();
    }
}
