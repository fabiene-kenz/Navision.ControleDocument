using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Navision.ControleDocument.DependenciesServices.IServices;
using Navision.ControleDocuments.Droid.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDependency))]
namespace Navision.ControleDocuments.Droid.Dependencies
{
    public class SQLiteDependency : ISQLite
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string libFolder = Path.Combine(docFolder, "Database");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            return Path.Combine(libFolder, filename);
        }
    }
}