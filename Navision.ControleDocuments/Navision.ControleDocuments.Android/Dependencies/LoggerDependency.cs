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
using Plugin.DeviceInfo;
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

        /// <summary>
        /// Write each excpetion in log file with time, device info and core of exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public async Task WriteLog(Exception ex)
        {
            string docFolder = Path.Combine(GetLogFolder(), "Logs");
            string filePath = Path.Combine(docFolder, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() + '-' + CrossDeviceInfo.Current.Id + ".log");
            string[] files;

            try
            {
                if (!Directory.Exists(docFolder))
                    Directory.CreateDirectory(docFolder);

                files = Directory.GetFiles(docFolder);

                if (files.Length == 0)
                    File.WriteAllText(filePath, null);
                else
                    filePath = CheckLogFileLength(GetLogFolder(), files);

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

        /// <summary>
        /// Check the size of th Log file, if more than 1Mbm then creates a new file with TimeStamp and Device ID as title
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        string CheckLogFileLength(string folderPath, string[] files)
        {
            string filePath = null;
            if (new System.IO.FileInfo(files.Last()).Length >= 1000000)
            {
                filePath = Path.Combine(folderPath, "Logs", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() + '-' + CrossDeviceInfo.Current.Id + ".log");
                using (File.CreateText(filePath)) ;
            }
            else
                filePath = files.Last();
            return filePath;
        }
    }
}