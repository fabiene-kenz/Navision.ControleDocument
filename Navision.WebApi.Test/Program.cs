using Navision.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Navision.WebApi.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //string uriBase = "http://localhost:61798/";
            string uriBase = "http://mobilenavision.azurewebsites.net/";
            string Uri = @"Login\CheckLogin";

            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();

            UserModel u = new UserModel { UserName = "fabien.richard@e-kenz.com", Password = "password" };
            var objUser = JsonConvert.SerializeObject(u);
            var content = new StringContent(objUser, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            { 
                client.DefaultRequestHeaders.Add("User-Agent", "Navision");
                var reponse = Task.Run(async () => await client.PostAsync(uriBase + Uri, content)).Result;
                var result = reponse.Content.ReadAsStringAsync().Result;
                var retour = JsonConvert.DeserializeObject<UserModel>(result);
            }

        }
    }
}
