using Navision.ControleDocuments.Models.DocsModel;
using Navision.ControleDocuments.Services.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
                return new List<PdfModel>();
            }
        }
    }
}
