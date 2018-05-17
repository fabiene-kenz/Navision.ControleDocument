using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Navision.WebApi.App_Start
{
    public class ExceptionHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {

                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(filterContext.Exception);
                filterContext.ExceptionHandled = true;
            }
        }
    }
}