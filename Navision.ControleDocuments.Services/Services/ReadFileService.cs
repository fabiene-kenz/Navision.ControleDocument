using Navision.ControleDocuments.Services.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Navision.ControleDocuments.Services.Services
{
    public class ReadFileService : IReadFileService
    {
        
        public Stream GetFileStream(string file)
        {
            var assembly = typeof(ReadFileService).Assembly;
            var dbStream = assembly.GetManifestResourceStream(file);
            return dbStream;
        }
    }
}
