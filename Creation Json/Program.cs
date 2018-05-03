using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Creation_Json.Model;
using Newtonsoft.Json;

namespace Creation_Json
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Company> list = getCompanyList();
            string jsonString = serializeList(list);
            writeStringToFile(jsonString);
            Console.ReadLine();
        }

        /// <summary>
        /// Creates and returns a list of Company objects
        /// </summary>
        /// <returns>Returns a list of Companies objects</returns>
        static private List<Company> getCompanyList()
        {
            Console.WriteLine("Recuperation de la list des Companies");
            List<Company> list = new List<Company>();

            list.Add(new Company() {CompanyName = "Company", Url="URL"});
            list.Add(new Company() {CompanyName = "Company1", Url = "URL1"});
            list.Add(new Company() { CompanyName = "Company2", Url = "URL2" });

            return list;
        }

        /// <summary>
        /// Serializes the list of companies into JSON
        /// </summary>
        /// <param name="companyList">The list of Companies objects is needed</param>
        /// <returns>Returns a string containing the JSON object</returns>
        static private string serializeList(List<Company> companyList)
        {
            Console.WriteLine("Serialisation de la liste des companies");
            try
            {
                string jsonString = JsonConvert.SerializeObject(companyList, Formatting.Indented);
                Console.WriteLine(jsonString);
                return jsonString;

            }
            catch (JsonException e)
            {
                throw e;
            }
        }
        
        /// <summary>
        /// Write the json string into a test.json file in Navision.ControleDocument.Services/fichierJson/
        /// </summary>
        /// <param name="jsonString">String containing the JSON object</param>
        static private void writeStringToFile(string jsonString)
        {
            string jsonFilePath = ConfigurationManager.AppSettings["jsonFilePath"];
            Console.WriteLine("Ecriture du fichier JSON");
            try
            {
                System.IO.File.WriteAllText(jsonFilePath, jsonString);
            }
            catch (IOException e)
            {
                throw e;
            }
            Console.WriteLine("Terminer");
        }
    }
}
