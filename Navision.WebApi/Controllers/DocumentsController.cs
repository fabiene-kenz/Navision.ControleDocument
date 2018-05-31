using Navision.DB;
using Navision.Models;
using Navision.WebApi.App_Start;
using Navision.WebApi.Approve;
using Navision.WebApi.Helpers;
using Navision.WebApi.Models;
using Navision.WebApi.RecordLink;
using Navision.WebApi.PurchaseOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Navision.WebApi.Controllers
{
    [ExceptionHandler]
    [@Authorize]
    public class DocumentsController : Controller
    {
        // GET: Documents
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetDocuments()
        {
            // Get user call api
            UserContext context = UserConnect.GetContext(HttpContext);
            UserMobile user = UserConnect.GetUserConnected(context.Token, context.Ip, context.UserAgent);
            List<DocumentModel> listsDocuments = new List<DocumentModel>();
            try
            {
                var purchaseOrders_PortClient = GetSalesOrder(user);
                // filter on statut

                PurchaseOrders_Filter fi = new PurchaseOrders_Filter();
                fi.Field = PurchaseOrders_Fields.Status;
                fi.Criteria = "Pending Approval";
                PurchaseOrders_Filter[] fiArray = new PurchaseOrders_Filter[] { fi };

                var results = await purchaseOrders_PortClient.ReadMultipleAsync(fiArray, null, 0);
                var approvalsMgmt=GetApprove(user);

                var recordlink = GetRecordLink(user);

                RecordLinkPage_Filter rlp = new RecordLinkPage_Filter();
                RecordLinkPage_Filter[] rlparray = new RecordLinkPage_Filter[] { rlp };


                foreach (var sale in results.ReadMultiple_Result1)
                {
                    var recordId = "Purchase Header: 1," + sale.No;
                    // check if user connected can approve a doc
                    if (approvalsMgmt.HasOpenApprovalEntriesForCurrentUser(recordId))
                    {
                        var docs = recordlink.ReadMultiple(rlparray, null, 0).Where(d=>d.Record_ID== recordId);
                        listsDocuments.AddRange( docs.Select(d => new DocumentModel() { DocName = d.Description, DocDate = d.Created, DocSatut = ((EnumStatut.Values) d.Statut).GetBool()}).ToList());
                    }                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new JsonResult { Data = listsDocuments,JsonRequestBehavior=JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// Get Approve service ref
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private ApprovalsMgmt_PortClient GetApprove(UserMobile user)
        {
            ApprovalsMgmt_PortClient approvalsMgmt = new ApprovalsMgmt_PortClient();
            approvalsMgmt.ClientCredentials.Windows.ClientCredential.UserName = user.User_Name;
            approvalsMgmt.ClientCredentials.Windows.ClientCredential.Password = CryptDecrypt.Decrypt(user.Password);
            return approvalsMgmt;
        }
        /// <summary>
        /// Get SalesOrder service ref
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private PurchaseOrders_PortClient GetSalesOrder(UserMobile user)
        {

            PurchaseOrders_PortClient purchaseOrders_PortClient = new PurchaseOrders_PortClient();
            purchaseOrders_PortClient.ClientCredentials.Windows.ClientCredential.UserName = user.User_Name;
            purchaseOrders_PortClient.ClientCredentials.Windows.ClientCredential.Password = CryptDecrypt.Decrypt(user.Password);
            return purchaseOrders_PortClient;
        }
        /// <summary>
        /// Get RecordLink service ref
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private RecordLinkPage_PortClient GetRecordLink(UserMobile user)
        {
            RecordLinkPage_PortClient linkPage_PortClient = new RecordLinkPage_PortClient();
            linkPage_PortClient.ClientCredentials.Windows.ClientCredential.UserName = user.User_Name;
            linkPage_PortClient.ClientCredentials.Windows.ClientCredential.Password = CryptDecrypt.Decrypt(user.Password);
            return linkPage_PortClient;
        }

    }
}