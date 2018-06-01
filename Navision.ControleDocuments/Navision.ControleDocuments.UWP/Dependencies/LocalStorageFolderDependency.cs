using Navision.ControleDocument.DependenciesServices.IServices;
using Navision.ControleDocuments.UWP.Dependencies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalStorageFolderDependency))]
namespace Navision.ControleDocuments.UWP.Dependencies
{
   public class LocalStorageFolderDependency: ILocalStorageFolder
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string libFolder = Path.Combine(docFolder, "FilesPdf");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            return Path.Combine(libFolder, filename);
        }
    }
}
