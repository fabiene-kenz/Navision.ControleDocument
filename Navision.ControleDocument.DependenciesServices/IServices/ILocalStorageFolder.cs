using System;
using System.Collections.Generic;
using System.Text;

namespace Navision.ControleDocument.DependenciesServices.IServices
{
   public interface ILocalStorageFolder
    {
        string GetLocalFilePath(string filename);
    }
}
