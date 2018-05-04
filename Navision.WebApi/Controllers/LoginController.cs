﻿using Navision.DB;
using Navision.Models;
using Navision.WebApi.Helpers;
using Navision.WebApi.IServices;
using Navision.WebApi.Services;
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
            ICheckUserServices checkUser = new CheckUserServices();
            // If exist retun user with token else without token
            if (checkUser.UserExist(user))
            {
                user.Token = App_Start.GenerationToken.GenerateToken(user.UserName, user.Password, Request.UserHostAddress, Request.UserAgent, DateTime.Now.Ticks);
                return new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }           
        }
        /// <summary>
        /// Add new access for user
        /// </summary>
        /// <param name="user"></param>
        /// <returns> userModel</returns>
        public JsonResult GetNewAccess(UserModel user)
        {
            ICheckUserServices checkUserServices = new CheckUserServices();
            IUserServices userServices = new UserServices();
            if (checkUserServices.UserExistInNavision(user))
            {
                if (userServices.AddUSerInMobile(user))
                {
                    user.Token = App_Start.GenerationToken.GenerateToken(user.UserName, user.Password, Request.UserHostAddress, Request.UserAgent, DateTime.Now.Ticks);
                    return new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    return new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            else
            {
                return new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}