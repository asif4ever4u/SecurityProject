using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Security.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            ViewBag.MyName = "Asif Shakir";
            //SecurityEF.Repositories.IRepository<SecurityEF.SecurityApplication> o = new SecurityEF.Repositories.Repository<SecurityEF.SecurityApplication>();
            //SecurityEF.SecurityApplication o = ctx.SecurityApplications.First<SecurityEF.SecurityApplication>();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
