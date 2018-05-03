using Navision.DB;
using Navision.WebApi.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Navision.WebApi.Services
{
    public class UserServices: IUserServices
    {
        private readonly Context _db;
    }
}