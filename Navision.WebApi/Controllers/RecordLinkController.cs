using Navision.DB;
using Navision.Models;
using Navision.WebApi.App_Start;
using Navision.WebApi.Helpers;
using Navision.WebApi.LinesPurchasOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Navision.WebApi.Controllers
{
    [@Authorize]
    [ExceptionHandler]
    public class RecordLinkController : Controller
    {
        // GET: RecordLink
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Get value to check
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetValueToCheck(DocumentModel document)
        {
            // Get user call api
            UserContext context = UserConnect.GetContext(HttpContext);
            UserMobile user = UserConnect.GetUserConnected(context.Token, context.Ip, context.UserAgent);
            LinesPurchasOrders_PortClient linesPurchasOrders_PortClient = GetLines(user);
            List<DocumentValuesModel> documentValues = new List<DocumentValuesModel>();
            try
            {
                var lines = linesPurchasOrders_PortClient.ReadMultiple(null, null, 0).Where(art => art.Document_No == document.IdDoc.ToString());

                documentValues.Add(new DocumentValuesModel { Name = "Total_Amount_Excl_VAT", Value = lines.FirstOrDefault().Total_Amount_Excl_VAT.ToString() });
                documentValues.Add(new DocumentValuesModel { Name = "Total_Amount_Incl_VAT", Value = lines.FirstOrDefault().Total_Amount_Incl_VAT.ToString() });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new JsonResult { Data = documentValues, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// Connect service lines
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private LinesPurchasOrders_PortClient GetLines(UserMobile user)
        {
            LinesPurchasOrders_PortClient linesPurchasOrders_PortClient = new LinesPurchasOrders_PortClient();
            linesPurchasOrders_PortClient.ClientCredentials.Windows.ClientCredential.UserName = user.User_Name;
            linesPurchasOrders_PortClient.ClientCredentials.Windows.ClientCredential.Password = CryptDecrypt.Decrypt(user.Password);
            return linesPurchasOrders_PortClient;
        }
    }
}