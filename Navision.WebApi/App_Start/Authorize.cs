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
            // Il a déjà etait connecté
            if (HttpContext.Current.Request.Headers["Authorization"].Any())
            {
                var token = HttpContext.Current.Request.Headers["Authorization"].Replace("Bearer", string.Empty).Replace(@"\", string.Empty);
                var userAgent = HttpContext.Current.Request.Headers["User-Agent"];
                var ip = HttpContext.Current.Request.UserHostAddress;
                //return true;
                return GenerationToken.IsTokenValid(token, ip, userAgent);
            }
            else
            {
                return false;
            }

        }

    }
}