using Navision.ControleDocument.DependenciesServices.IServices;
using Navision.ControleDocuments.Services.IServices;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Navision.ControleDocuments.Services.Services
{
    public class ZipService : IZipService
    {
        /// <summary>
        /// Zip the log folder and returns the location of the zip folder
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public async Task<string> ZipLogs(string folderPath)
        {
            string outputPath = Path.Combine(folderPath, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() + '-' + CrossDeviceInfo.Current.Id + ".zip");
            int logs = Directory.GetFiles(Path.Combine(folderPath, "Logs")).Length;
            if (logs == 0)
                return "";
            try
            {
                if (File.Exists(outputPath))
                    File.Delete(outputPath);
                ZipFile.CreateFromDirectory(Path.Combine(folderPath, "Logs"), outputPath);
                return outputPath;
            }
            catch (Exception ex)
            {
                await DependencyService.Get<ILogger>().WriteLog(ex);
                return null;
            }
        }
    }
}
