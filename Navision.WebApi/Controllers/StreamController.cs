using GhostscriptSharp;
using iTextSharp.text.pdf;
using Navision.Models;
using Navision.WebApi.App_Start;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Navision.WebApi.Controllers
{
    enum FileFormatEnum { jpg, jpeg, png, bmp, pdf };

    [ExceptionHandler]
    //[@Authorize]
    public class StreamController : Controller
    {
        static string configFilePath = "TODO";
        static string MULTIPLE_FILE_LOCATION = "output%d.jpg";

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

                return new JsonResult { Data = jsonModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        /// <summary>
        /// Get the PDF file specified in argument
        /// Create an instance of PdfModel containing the content of the Pdf as a array of bytes and the file name
        /// (The maximum size of the json, and hence the PDf file, is an Int32 maximum size (2GB))
        /// </summary>
        /// <returns>Returns a JsonResult of the pdf content to the client</returns>
        public JsonResult GetPdfFile(DocumentModel pdffilepath)
        {
            var filepath = pdffilepath.Url;
            var uri = new System.Uri(filepath);
            var nameFolder = uri.Segments.Last().Split('.')[0];
            var fileFormat = uri.Segments.Last().Split('.')[1].ToLower();

            var folder = Server.MapPath(@"~/Logs/FileRead/" + nameFolder);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Info(folder);


            if (fileFormat == FileFormatEnum.pdf.ToString())
                SavePdfToImage(folder, filepath);
            else if (fileFormat == FileFormatEnum.jpg.ToString() || fileFormat == FileFormatEnum.jpeg.ToString()
                || fileFormat == FileFormatEnum.png.ToString() || fileFormat == FileFormatEnum.bmp.ToString())
            {
                using (WebClient webClient = new WebClient())
                    webClient.DownloadFile(filepath, folder + "/" + nameFolder + '.' + fileFormat);
            }

            List<PdfModel> listPdfModel = getFolderContent(folder, nameFolder);
            
            return new JsonResult { Data = listPdfModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// Get and return the content of the folder
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="nameFolder"></param>
        /// <returns></returns>
        private List<PdfModel> getFolderContent(string folder, string nameFolder)
        {
            var result = Directory.GetFiles(folder);
            string url = WebConfigurationManager.AppSettings["URL"];
            List<PdfModel> listPdfModel = new List<PdfModel>();

            foreach (var doc in result)
            {
                var uriDoc = new System.Uri(doc);
                var nameDoc = uriDoc.Segments.Last();
                listPdfModel.Add(new PdfModel { URL = url + @"/Logs/FileRead/" + nameFolder + "/" + nameDoc, fileName = nameDoc });
            }
            return listPdfModel;
        }

        private void SavePdfToImage(string folder, string filepath)
        {
           Logger logger = LogManager.GetCurrentClassLogger();
            logger.Info(folder);

            PdfReader pdfReader = new PdfReader(filepath);
            int numberOfPages = pdfReader.NumberOfPages;

            var finalPath = string.Join("/", folder, MULTIPLE_FILE_LOCATION);
            logger.Info(finalPath);
            GhostscriptWrapper.GeneratePageThumbs(filepath, finalPath, 1, numberOfPages, 100, 100);
        }

        /// <summary>
        /// Clean and delete the folder specified in doc.Url after user processed the document
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public JsonResult CleanFolder(DocumentModel doc)
        {
            try
            {
                var filepath = doc.Url;
                var uri = new System.Uri(filepath);
                var nameFolder = uri.Segments.Last().Split('.')[0];

                var folder = Server.MapPath(@"~/Logs/FileRead/" + nameFolder);
                if (Directory.Exists(folder))
                    Directory.Delete(folder, true);

                return new JsonResult { Data = "OK", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch(Exception ex)
            {
                return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

    }
}