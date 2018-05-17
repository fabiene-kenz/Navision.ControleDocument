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

[assembly: Dependency(typeof(SQLite))]
namespace Navision.ControleDocuments.Droid.Dependencies
{
    public class SQLite : ISQLite
    {
        public string GetLocalFilePath(Stream file, string filename)
        {
            string docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string libFolder = Path.Combine(docFolder, "Database");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            if (!File.Exists(Path.Combine(libFolder, filename)))
            {
                string filePath = Path.Combine(libFolder, filename);
                FileStream fileStream = File.Create(filePath, (int)file.Length);
                // Initialize the bytes array with the stream length and then fill it with data
                byte[] bytesInStream = new byte[file.Length];
                file.Read(bytesInStream, 0, bytesInStream.Length);
                // Use write method to write to the file specified above
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
            return Path.Combine(libFolder, filename);
        }
    }
}