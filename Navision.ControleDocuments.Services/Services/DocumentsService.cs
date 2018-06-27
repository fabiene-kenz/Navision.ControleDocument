using Navision.ControleDocuments.Models.DocsModel;
using Navision.ControleDocuments.Models.UserModels;
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
        public DocumentsService(UserModel user) : base(user)
        {

        }
        /// <summary>
        /// Get doc for user connected
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
        /// <summary>
        /// Get values to check in document
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task<List<DocumentValuesModel>> GetValueToCheck(DocModel document)
        {
            string Uri = @"/RecordLink/GetValueToCheck";
            var objDoc = JsonConvert.SerializeObject(document);
            var content = new StringContent(objDoc, Encoding.UTF8, "application/json");
            try
            {
                var response = await httpClient.PostAsync(Uribase + Uri, content);
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<DocumentValuesModel>>(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get values to check in document
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task<bool> ApproveOrRejectDocument(DocModel document)
        {
            string Uri = @"/Documents/ApproveOrRejectRecord";
            var objDoc = JsonConvert.SerializeObject(document);
            var content = new StringContent(objDoc, Encoding.UTF8, "application/json");
            try
            {
                var response = await httpClient.PostAsync(Uribase + Uri, content);
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<bool>(result);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
