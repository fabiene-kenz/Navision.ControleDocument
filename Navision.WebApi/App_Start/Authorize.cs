using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Navision.WebApi.App_Start
{
    public class Authorize: AuthorizeAttribute
    {
       

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        { 
            if (HttpContext.Current.Request.Headers["username"].Any())
            {
                var user = HttpContext.Current.Request.Headers["username"];
                // Je vérifie si l'appel au WebService vient de l'app
                if (HttpContext.Current.Request.Headers["Authorization"].Any())
                {                    
                    var idApp = HttpContext.Current.Request.Headers["Authorization"];
                    //var idAppDecrypted = c.DataDencrypted(idApp.Replace("Basic", String.Empty));
                    return true;
                    //if (idAppDecrypted == TokenConstants.AppId)
                    //{
                    //    return true;
                    //}
                    //else
                    //{
                    //    return false;
                    //}
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

    }
}