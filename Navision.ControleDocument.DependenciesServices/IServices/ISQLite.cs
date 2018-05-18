using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Navision.ControleDocument.DependenciesServices.IServices
{
    public interface ISQLite
    {
        string GetLocalFilePath(string filename);
    }
}
