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

        public async Task<List<DocModel>> GetDocuments()
        {
            string Uri = @"/Documents/GetDocuments";
            //var objUser = JsonConvert.SerializeObject(user);
            //var content = new StringContent(objUser, Encoding.UTF8, "application/json");
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
