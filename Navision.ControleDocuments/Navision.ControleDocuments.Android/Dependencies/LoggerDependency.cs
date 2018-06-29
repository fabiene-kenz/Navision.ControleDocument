using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Navision.ControleDocument.DependenciesServices.IServices;
using Navision.ControleDocuments.Droid.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(LoggerDependency))]
namespace Navision.ControleDocuments.Droid.Dependencies
{
    public class LoggerDependency : ILogger
    {
        public string GetLogFolder()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
        }

        public async Task WriteLog(Exception ex)
        {
            string docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string libFolder = Path.Combine(docFolder, "Logs");
            string filePath = Path.Combine(libFolder, "Logs.txt");

            try
            {
                if (!Directory.Exists(libFolder))
                    Directory.CreateDirectory(libFolder);
                if (!File.Exists(filePath))
                    File.CreateText(filePath);

                using (System.IO.StreamWriter writer = new StreamWriter(filePath, true))
                    await writer.WriteLineAsync(ex.StackTrace);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}