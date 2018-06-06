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
    public class DocumentsService : ConnectService, IDocumentsService
    {
        public DocumentsService() : base()
        {

        }
        public DocumentsService(string token) : base(token)
        {

        }
        /// <summary>
        /// GEt doc for user connected
        /// </summary>
        /// <returns></returns>
        public async Task<List<DocModel>> GetDocuments()
        {
            string Uri = @"/Documents/GetDocuments";
            try
            {
                var reponse = await httpClient.GetStringAsync(Uribase + Uri);
                var test = JsonConvert.DeserializeObject<List<DocModel>>(reponse);
                return JsonConvert.DeserializeObject<List<DocModel>>(reponse);
            }
            catch (Exception ex)
            {
                return new List<DocModel>();
            }
        }
    }
}
