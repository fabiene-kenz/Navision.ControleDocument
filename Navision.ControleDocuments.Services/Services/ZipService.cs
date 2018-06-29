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
        public async Task<string> ZipLogs(string folderPath)
        {
            string outputPath = Path.Combine(folderPath, CrossDeviceInfo.Current.Id + ".zip");
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
