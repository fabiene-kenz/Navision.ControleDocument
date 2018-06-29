using Navision.ControleDocument.DependenciesServices.IServices;
using Navision.ControleDocuments.UWP.Dependencies;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(LoggerDependency))]
namespace Navision.ControleDocuments.UWP.Dependencies
{
    public class LoggerDependency : ILogger
    {
        public string GetLogFolder()
        {
            return System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        public async Task WriteLog(Exception ex)
        {
            string docFolder = GetLogFolder();
            string filePath = Path.Combine(docFolder, DateTime.Now.ToString("ddMMyyyy-HHmm-") + CrossDeviceInfo.Current.Id + ".log");
            string[] files;

            try
            {
                if (!Directory.Exists(docFolder))
                    Directory.CreateDirectory(docFolder);

                files = Directory.GetFiles(docFolder);

                if (files.Length == 0)
                    File.WriteAllText(filePath, null);
                else
                    filePath = CheckLogFileLength(docFolder, files);

                using (FileStream fs = File.Open(filePath, FileMode.Append, FileAccess.Write))
                {
                    string exFormat = "----------BEGIN DEVICE INFO----------" + System.Environment.NewLine +
                       DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + System.Environment.NewLine +
                        CrossDeviceInfo.Current.Manufacturer + System.Environment.NewLine +
                        CrossDeviceInfo.Current.Model + System.Environment.NewLine +
                        CrossDeviceInfo.Current.DeviceName + System.Environment.NewLine +
                        CrossDeviceInfo.Current.Platform + System.Environment.NewLine +
                        CrossDeviceInfo.Current.Version + System.Environment.NewLine +
                        CrossDeviceInfo.Current.Idiom + System.Environment.NewLine +
                        "----------END OF DEVICE INFO----------" + System.Environment.NewLine +
                        "----------BEGIN OF EXCEPTION----------" + System.Environment.NewLine +
                        ex.ToString() + System.Environment.NewLine +
                        "----------END OF EXCEPTION----------\n" + System.Environment.NewLine;

                    Byte[] exString = new UTF8Encoding(true).GetBytes(exFormat);
                    await fs.WriteAsync(exString, 0, exString.Length);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        string CheckLogFileLength(string folderPath, string[] files)
        {
            string filePath = null;
            if (new System.IO.FileInfo(files.Last()).Length >= 1000000)
            {
                var newPath = Path.Combine(folderPath, "Logs" + DateTime.Now.ToString("ddMMyyyy-HHmm") + CrossDeviceInfo.Current.Id + ".log");
                filePath = Path.Combine(folderPath, newPath);
                if (!File.Exists(filePath))
                    using (File.CreateText(filePath));
            }
            else
                filePath = files.Last();
            return filePath;
        }

    }
}
