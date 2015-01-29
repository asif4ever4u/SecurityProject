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
namespace Security.Controllers
{
    public class UserController : BaseController
    {
        SecurityUserBLL oUserBLL;
        //SecurityUserApplicationBLL oUserApplicationBLL;
        //private IRepository<SecurityUser> rep;
        // private MasterViewModel<SecurityEF.SecurityUser> oModel;
        //private string sModelKey = "oModel";

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            oUserBLL = new SecurityUserBLL();
            //uaBLL = new SecurityUserApplicationBLL();
        }

        
        public ActionResult Index(int pRefresh = 0)
        {
            IEnumerable<SecurityUser> vList;

            if (pRefresh == 1 || Session["UserList"] == null)
            {
                vList = oUserBLL.GetAll();
                Session["UserList"] = vList;
            }
            else 
            {
                vList = (IEnumerable<SecurityUser>)Session["UserList"];
            }
            return View(vList);
        }

        //POST: /User/Create
        
        public ActionResult Create()
        {
            SecurityUser oNew = new SecurityUser();
            oNew.ActiveFlag2 = true;
            return View(oNew);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SecurityUser pSecurityUser)
        {
            if (ModelState.IsValid)
            {
                if (pSecurityUser.Password == pSecurityUser.PasswordReType)
                {
                    if (oUserBLL.Insert(pSecurityUser))
                    {
                        return RedirectToAction("Index", new { pRefresh = 1 });
                    }
                }
            }

            pSecurityUser.Password = string.Empty;
            pSecurityUser.PasswordReType = string.Empty;
            return View(pSecurityUser);
        }


        public ActionResult Edit(string pUserCode)
        {
            SecurityUser vSecurityUser = oUserBLL.GetByCode(pUserCode);
            if (vSecurityUser == null)
            {
                return HttpNotFound();
            }

            vSecurityUser.Password = string.Empty;
            vSecurityUser.PasswordReType = string.Empty;
            return View(vSecurityUser);
        }

        //
        // POST: /User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SecurityUser pSecurityUser)
        {
            if (ModelState.IsValid)
            {
                if (oUserBLL.Update(pSecurityUser))
                {
                    return RedirectToAction("Index", new { pRefresh= 1 });
                }
            }

            pSecurityUser.Password = string.Empty;
            pSecurityUser.PasswordReType = string.Empty;

            return View(pSecurityUser);
        }

        ////GET: /User/Details/5
        //public ActionResult EditUserApps(long pUserID)
        //{
        //    MDModel<SecurityUser, SecurityUserApplication> vModel = new MDModel<SecurityUser, SecurityUserApplication>();

        //    vModel.Master = oBLL.GetByID(pUserID);
        //    vModel.DetailList = uaBLL.GetByUserCode(vModel.Master.UserCode);

        //    if (vModel.Master == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(vModel);
        //}

        //// POST: /User/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditUserApps(MDModel<SecurityUser, SecurityUserApplication> pUserApplicationModel)
        //{
        //    if (uaBLL.SaveList(pUserApplicationModel.DetailList))
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return HttpNotFound();
        //    }

        //}

        public ActionResult CheckLoginName(string pLoginName)
        {
            SecurityUser vUser = oUserBLL.GetLikeByLoginName(pLoginName);
            if (vUser != null)
            {
                return Json(pLoginName + " is not available, it is already given to " + vUser.EmployeeName + ".", JsonRequestBehavior.AllowGet);
            }
            return Json(pLoginName + " is available.", JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            base.Dispose(disposing);
        }
    }
}