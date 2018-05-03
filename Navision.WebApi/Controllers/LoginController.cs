using Navision.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Navision.WebApi.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Check user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>UserModel</returns>
        /// Login\CheckLogin
        public JsonResult CheckLogin(UserModel user)
        {
            user.Token= Navision.WebApi.App_Start.GenerationToken.GenerateToken(user.UserName, user.Password, Request.UserHostAddress, Request.UserAgent, DateTime.Now.Ticks);
            return new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}