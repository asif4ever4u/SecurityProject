using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecurityEF;
using Security.Models;
using SecurityEF.Repositories;
using System.Linq.Expressions;
using SecurityEF.BLL;
using System.Web.Security;

using System.Net;
using System.Web.Http;

namespace Security.Controllers
{
    public class UserWPIController : ApiController
    {
        public IEnumerable<SecurityEF.SecurityUser> GetUsers()
        {
            SecurityEF.BLL.SecurityUserBLL o = new SecurityUserBLL();
            return o.GetAll();
        }
    }
}