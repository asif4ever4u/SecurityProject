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
    public class UserAccessController : BaseController
    {
        private SecurityApplicationBLL oApplicationBLL;
        private SecurityFormBLL oFormBLL;
        private SecurityFormActionBLL oFormActionBLL;
        private SecurityRoleBLL oRoleBLL;
        private SecurityRoleAccessBLL oRoleAccessBLL;
        private SecurityRoleApplicationBLL oRoleApplicationBLL;
        private SecurityUserBLL oUserBLL;
        private SecurityUserAccessBLL oUserAccessBLL;
        private SecurityUserApplicationBLL oUserApplicationBLL;
        private SecurityUserRoleBLL oUserRoleBLL;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            oApplicationBLL = new SecurityApplicationBLL();
            oFormBLL = new SecurityFormBLL();
            oFormActionBLL = new SecurityFormActionBLL();
            oRoleBLL = new SecurityRoleBLL();
            oRoleAccessBLL = new SecurityRoleAccessBLL();
            oRoleApplicationBLL = new SecurityRoleApplicationBLL();
            oUserBLL = new SecurityUserBLL();
            oUserAccessBLL = new SecurityUserAccessBLL();
            oUserApplicationBLL = new SecurityUserApplicationBLL();
            oUserRoleBLL = new SecurityUserRoleBLL();
        }

        //GET: /User/Details/5
        public ActionResult IndexUserForms(string pUserCode, string pApplicationCode)
        {
            MMDModel<SecurityUser, SecurityApplication, SecurityForm> vModel = new MMDModel<SecurityUser, SecurityApplication, SecurityForm>();

            vModel.Master1 = oUserBLL.GetByCode(pUserCode);
            vModel.Master2 = oApplicationBLL.GetByCode(pApplicationCode);
            vModel.DetailList = oFormBLL.GetByApplicationCode(vModel.Master2.ApplicationCode).ToList();

            if (vModel.Master1 == null || vModel.Master2 == null)
            {
                return HttpNotFound();
            }

            return View(vModel);
        }

        //GET: /User/Details/5
        public ActionResult Edit(string pUserCode, string pApplicationCode, string pFormCode)
        {
            MMDModel<SecurityUser, SecurityForm, SecurityUserAccess> pModel = new MMDModel<SecurityUser, SecurityForm, SecurityUserAccess>();

            pModel.Master1 = oUserBLL.GetByCode(pUserCode);
            pModel.Master2 = oFormBLL.GetByCode(pApplicationCode, pFormCode);
            pModel.DetailList = oUserAccessBLL.GetByUserAppForm(pModel.Master1, pModel.Master2);

            if (pModel.Master1 == null)
            {
                return HttpNotFound();
            }

            return View(pModel);
        }

        // POST: /User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MMDModel<SecurityUser, SecurityForm, SecurityUserAccess> pModel)
        {
            if (oUserAccessBLL.SaveList(pModel.DetailList))
            {
                return RedirectToAction("IndexUserForms", new { pUserCode = pModel.Master1.UserCode, pApplicationCode = pModel.Master2.ApplicationCode, pFormCode = pModel.Master2.FormCode });
            }
            else
            {
                return HttpNotFound();
            }
        }



        //
        //GET: /User/Details/5
        public ActionResult EditUserApps(string pUserCode)
        {
            MDModel<SecurityUser, SecurityUserApplication> vModel = new MDModel<SecurityUser, SecurityUserApplication>();

            vModel.Master = oUserBLL.GetByCode(pUserCode);
            vModel.DetailList = oUserApplicationBLL.GetByUserCode(vModel.Master.UserCode).ToList();

            if (vModel.Master == null)
            {
                return HttpNotFound();
            }

            return View(vModel);
        }

        // POST: /User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserApps(MDModel<SecurityUser, SecurityUserApplication> pModel)
        {
            if (oUserApplicationBLL.SaveList(pModel.DetailList))
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                return HttpNotFound();
            }

        }

        //
        //GET: /User/Details/5
        public ActionResult EditUserRoles(string pUserCode)
        {
            MDModel<SecurityUser, SecurityUserRole> vModel = new MDModel<SecurityUser, SecurityUserRole>();

            vModel.Master = oUserBLL.GetByCode(pUserCode);
            vModel.DetailList = oUserRoleBLL.GetByUserCode(pUserCode);

            if (vModel.Master == null)
            {
                return HttpNotFound();
            }

            return View(vModel);
        }

        // POST: /User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserRoles(MDModel<SecurityUser, SecurityUserRole> pModel)
        {
            if (oUserRoleBLL.SaveList(pModel.DetailList))
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                return HttpNotFound();
            }
        }

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            base.Dispose(disposing);
        }
    }
}