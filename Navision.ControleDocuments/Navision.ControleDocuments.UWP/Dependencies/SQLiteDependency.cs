using Navision.ControleDocument.DependenciesServices.IServices;
using Navision.ControleDocuments.UWP.Dependencies;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDependency))]
namespace Navision.ControleDocuments.UWP.Dependencies
{
    public class SQLiteDependency : ISQLite
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string libFolder = Path.Combine(docFolder, "Database");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            return Path.Combine(libFolder, filename);
        }        
    }
}

