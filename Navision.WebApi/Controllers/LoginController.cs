using Navision.DB;
using Navision.Models;
using Navision.WebApi.App_Start;
using Navision.WebApi.Helpers;
using Navision.WebApi.IServices;
using Navision.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        [ExceptionHandlerAttribute]
        [@Authorize]
        /// <summary>
        /// Check user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>UserModel</returns>
        /// Login\CheckLogin
        public JsonResult CheckLogin(UserModel user)
        {
            ICheckUserServices checkUser = new CheckUserServices();
            // If exist retun user with token else without token
            if (checkUser.UserExist(user))
            {
                return new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        [ExceptionHandlerAttribute]
        /// <summary>
        /// Add new access for user
        /// </summary>
        /// <param name="user"></param>
        /// <returns> userModel</returns>
        /// Login\GetNewAccess
        public JsonResult GetNewAccess(UserModel user)
        {
            ICheckUserServices checkUserServices = new CheckUserServices();
            IUserServices userServices = new UserServices();
            // Check if user exist in User Table and not in User Mobile
            if (checkUserServices.UserExistInNavision(user) && !checkUserServices.UserExist(user))
            {
                // Add in User Mobile Table
                if (userServices.AddUSerInMobile(user))
                {
                    //string token = App_Start.GenerationToken.GenerateToken(user.UserName, user.Password, Request.UserHostAddress, Request.UserAgent, DateTime.Now.Ticks);
                    return new JsonResult { Data = true, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    return new JsonResult { Data = false, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            else
            {
                return new JsonResult { Data = false, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        [ExceptionHandlerAttribute]
        /// <summary>
        /// Get Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// Login\ConnectUser
        public JsonResult ConnectUser(UserModel user)
        {
            ICheckUserServices checkUser = new CheckUserServices();
            if (checkUser.UserExist(user))
            {
               
                string token = App_Start.GenerationToken.GenerateToken(user.UserName, user.Password, Request.UserHostAddress, Request.UserAgent, DateTime.Now.Ticks);
                return new JsonResult { Data = token, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult { Data = string.Empty, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

    }
}