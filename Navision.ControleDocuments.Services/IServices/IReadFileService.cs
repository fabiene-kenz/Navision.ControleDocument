using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Navision.ControleDocuments.Services.IServices
{
    public interface IReadFileService
    {
        Stream GetFileStream(string file);
    }
}
