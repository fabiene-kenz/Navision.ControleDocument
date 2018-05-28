using Navision.WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Navision.WebApi.Controllers
{
    public class StreamController : Controller
    {

        static string configFilePath = "TODO";

        // GET: Stream
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Read the json specified in 'jsonFilePath' string to get a stream
        /// Deserialize the stream as a JsonModel
        /// Return the Json to the client
        /// </summary>
        /// <returns>Returns a JsonResult containing the content of the Json as a JsonModel</returns>
        public JsonResult GetConfigFile()
        {
            using (StreamReader reader = (System.IO.File.OpenText(configFilePath)))
            {
                JsonSerializer serializer = new JsonSerializer();
                JsonModel jsonModel = (JsonModel)serializer.Deserialize(reader, typeof(JsonModel));

                return new JsonResult { Data = jsonModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
            }
        }

        /// <summary>
        /// Get the PDF file specified in argument
        /// Create an instance of PdfModel containing the content of the Pdf as a array of bytes and the file name
        /// (The maximum size of the json, and hence the PDf file, is an Int32 maximum size (2GB))
        /// </summary>
        /// <returns>Returns a JsonResult of the pdf content to the client</returns>
        public JsonResult GetPdfFile(string pdfFilePath)
        {
            PdfModel pdfModel = new PdfModel()
            {
                fileStream = System.IO.File.ReadAllBytes(pdfFilePath),
                fileName = Path.GetFileName(pdfFilePath)
            };

            return new JsonResult { Data = pdfModel,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }
    }
}