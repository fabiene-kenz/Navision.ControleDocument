using Navision.ControleDocuments.Services.IServices;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Navision.ControleDocuments.Services.Services
{
    public class JsonService : IJsonService
    {
        public string GetJson()
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    var arrayByteConfig = webClient.DownloadData("http://navapi.saas.e-kenz.com/Json/Config.json");
                    System.Text.Encoding enc = System.Text.Encoding.ASCII;
                    string configJson = enc.GetString(arrayByteConfig);
                    return configJson;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
            
        }
    }
}
