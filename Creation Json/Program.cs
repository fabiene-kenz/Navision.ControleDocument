using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using Creation_Json.Model;
using System.Diagnostics;

namespace Creation_Json
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigFile configObject = GetConfigObject();
            string jsonString = SerializeObject(configObject);
            WriteStringToFile(jsonString);
            Console.ReadLine();
            var versionInfo = typeof(Program).Assembly.GetName().Version.ToString();
        }

        /// <summary>
        /// Creates and returns an Object containing the version of the program and the list of the companies with their respective URL
        /// </summary>
        /// <returns>Returns a config object</returns>
        static private ConfigFile GetConfigObject()
        {
            ConfigFile objectConfig = new ConfigFile();
            List<Company> list = new List<Company>();
            objectConfig.Version = typeof(Program).Assembly.GetName().Version.ToString();

            list.Add(new Company() { CompanyName = "Company0", Url = "URL0", Domain = "Domain0" });
            list.Add(new Company() { CompanyName = "Company1", Url = "URL1", Domain = "Domain1" });
            list.Add(new Company() { CompanyName = "Company2", Url = "URL2", Domain = "Domain2" });
            objectConfig.Companies = list;

            return objectConfig;
        }

        /// <summary>
        /// Serializes the config object into JSON
        /// </summary>
        /// <param name="configObject">The object with the version and the list of companies with their URL</param>
        /// <returns>Returns a string containing the JSON object</returns>
        static private string SerializeObject(ConfigFile configObject)
        {
            Console.WriteLine("Sérialisation de l'objet en Json");
            try
            {
                string jsonString = JsonConvert.SerializeObject(configObject, Formatting.Indented);
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
        static private void WriteStringToFile(string jsonString)
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
            Console.WriteLine("Terminé");
        }
    }
}
