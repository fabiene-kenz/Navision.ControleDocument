using Navision.ControleDocument.DependenciesServices.IServices;
using Navision.ControleDocuments.Models.DocsModel;
using Navision.ControleDocuments.Models.LogsModel;
using Navision.ControleDocuments.Services.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Navision.ControleDocuments.Services.Services
{
   public class StreamService:ConnectService, IStreamService
    {
        public StreamService() : base()
        {

        }
        public StreamService(string token) : base(token)
        {

        }

        /// <summary>
        /// Clean the folder in WebApi after user processed the document
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public async Task<string> CleanFolder(DocModel doc)
        {
            string Uri = @"/Stream/CleanFolder";
            var objDoc = JsonConvert.SerializeObject(doc);
            var content = new StringContent(objDoc, Encoding.UTF8, "application/json");
            try
            {
                var response = await httpClient.PostAsync(Uribase + Uri, content);
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<string>(result);
            }
            catch (Exception ex)
            {
                await DependencyService.Get<ILogger>().WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// Get file
        /// </summary>
        /// <param name="pdffilepath"></param>
        /// <returns></returns>
        public async Task<List<PdfModel>> GetPdfFile(DocModel pdffilepath)
        {
            string Uri = @"/Stream/GetPdfFile";
            var objUser = JsonConvert.SerializeObject(pdffilepath);
            var content = new StringContent(objUser, Encoding.UTF8, "application/json");
            try
            {
                var reponse = await httpClient.PostAsync(Uribase + Uri,content);
                var result = await reponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<PdfModel>>(result);
            }
            catch (Exception ex)
            {
                await DependencyService.Get<ILogger>().WriteLog(ex);
                return new List<PdfModel>();
            }
        }

        public async Task<bool> SendLogs(string LogsFilePath)
        {
            LogsModel logModel = new LogsModel();
            logModel.fileName = Path.GetFileName(LogsFilePath);
            logModel.fileContent = System.IO.File.ReadAllBytes(LogsFilePath);

            string Uri = @"/Stream/GetLogFile";
            var obj = JsonConvert.SerializeObject(logModel);
            var content = new StringContent(obj, Encoding.UTF8, "application/json");
            try
            {
                var response = await httpClient.PostAsync(Uribase + Uri, content);
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<bool>(result);
            }
            catch(Exception ex)
            {
                await DependencyService.Get<ILogger>().WriteLog(ex);
                return false;
            }
        }
    }
}
