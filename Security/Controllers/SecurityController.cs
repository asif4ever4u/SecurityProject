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

namespace Security.Controllers
{
    public class SecurityController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            //SecurityLogin oNew = new SecurityLogin();
            //oNew.LoginName = "Administrator";
            //oNew.Password = "";
            //oNew.RememberMe = true;
            return View();
        }

        //public ActionResult FindLogin(string pLoginName)
        //{
        //    SecurityEF.BLL.SecurityUserBLL o = new SecurityUserBLL();
        //    return Json(o.GetByLoginName(pLoginName).EmployeeName, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetUsers(int id = 0)
        //{
        //    SecurityEF.BLL.SecurityUserBLL o = new SecurityUserBLL();
        //    return Json(o.GetAll(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(SecurityLogin pLogin)
        {

            SecurityEF.BLL.SecurityLoginBLL o = new SecurityLoginBLL();
            SecurityLogin vLogin = o.GetByLoginName(pLogin.LoginName);
            if (vLogin == null)
            {
                pLogin.Password = string.Empty;
                ViewBag.LoginError = "The user name or password provided is incorrect...!";
                return View(pLogin);
            }
            else if (pLogin.Password != vLogin.Password)
            {
                pLogin.Password = string.Empty;
                ViewBag.LoginError = "The password provided is incorrect...!";
                return View(pLogin);
            }

            vLogin.Password = string.Empty;
            string userData = Newtonsoft.Json.JsonConvert.SerializeObject(vLogin);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, vLogin.LoginName, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);

            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);

            return RedirectToAction("Index", "User");

            //if (vLogin.Roles.Split(new char[] { ';' }).Contains("Admin"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
                
            //}
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult LogOff2()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            base.Dispose(disposing);
        }
    }

    public class CustomPrincipal : System.Security.Principal.IPrincipal
    {
        public System.Security.Principal.IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            if (LoginInfo.Roles.Split(new char[] { ';' }).Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CustomPrincipal(string Username, SecurityLogin pLogin)
        {
            this.Identity = new System.Security.Principal.GenericIdentity(pLogin.LoginName, "Forms");
            this.LoginInfo = pLogin;
        }

        public SecurityLogin LoginInfo { get; set; }
    }
}