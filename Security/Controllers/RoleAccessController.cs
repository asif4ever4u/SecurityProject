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
    public class RoleAccessController : BaseController
    {
        private SecurityRoleBLL oRoleBLL;
        private SecurityUserRoleBLL oUserRoleBLL;
        private SecurityRoleAccessBLL oRoleAccessBLL;

        private SecurityApplicationBLL oApplicationBLL;
        private SecurityFormBLL oFormBLL;
        private SecurityRoleApplicationBLL oRoleApplicationBLL;


        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            oRoleBLL = new SecurityRoleBLL();
            oUserRoleBLL = new SecurityUserRoleBLL();
            oRoleAccessBLL = new SecurityRoleAccessBLL();
            oApplicationBLL = new SecurityApplicationBLL();
            oFormBLL = new SecurityFormBLL();
            oRoleApplicationBLL = new SecurityRoleApplicationBLL();
        }

        //GET: /User/Details/5
        public ActionResult IndexRoleForms(string pRoleCode, string pApplicationCode)
        {
            MMDModel<SecurityRole, SecurityApplication, SecurityForm> vModel = new MMDModel<SecurityRole, SecurityApplication, SecurityForm>();

            vModel.Master1 = oRoleBLL.GetByCode(pRoleCode);
            vModel.Master2 = oApplicationBLL.GetByCode(pApplicationCode);
            vModel.DetailList = oFormBLL.GetByApplicationCode(vModel.Master2.ApplicationCode).ToList();

            if (vModel.Master1 == null || vModel.Master2 == null)
            {
                return HttpNotFound();
            }

            return View(vModel);
        }

        //GET: /User/Details/5
        public ActionResult Edit(string pRoleCode, string pApplicationCode, string pFormCode)
        {
            MMDModel<SecurityRole, SecurityForm, SecurityRoleAccess> pModel = new MMDModel<SecurityRole, SecurityForm, SecurityRoleAccess>();

            pModel.Master1 = oRoleBLL.GetByCode(pRoleCode);
            pModel.Master2 = oFormBLL.GetByCode(pApplicationCode, pFormCode);
            pModel.DetailList = oRoleAccessBLL.GetByRoleForm(pModel.Master1, pModel.Master2);

            if (pModel.Master1 == null)
            {
                return HttpNotFound();
            }

            return View(pModel);
        }

        // POST: /User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MMDModel<SecurityRole, SecurityForm, SecurityRoleAccess> pModel)
        {
            if (oRoleAccessBLL.SaveList(pModel.DetailList))
            {
                return RedirectToAction("IndexRoleForms", new { pRoleCode = pModel.Master1.RoleCode, pApplicationCode = pModel.Master2.ApplicationCode });
            }
            else
            {
                return HttpNotFound();
            }
        }



        //
        //GET: /User/Details/5
        public ActionResult EditRoleApps(string pRoleCode)
        {
            MDModel<SecurityRole, SecurityRoleApplication> vModel = new MDModel<SecurityRole, SecurityRoleApplication>();

            vModel.Master = oRoleBLL.GetByCode(pRoleCode);
            vModel.DetailList = oRoleApplicationBLL.GetByRole(vModel.Master);

            if (vModel.Master == null)
            {
                return HttpNotFound();
            }

            return View(vModel);
        }

        // POST: /User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRoleApps(MDModel<SecurityRole, SecurityRoleApplication> pModel)
        {
            if (oRoleApplicationBLL.SaveList(pModel.DetailList))
            {
                return RedirectToAction("Index", "Role");
            }
            else
            {
                return HttpNotFound();
            }

        }


        ////
        ////GET: /User/Details/5
        //public ActionResult EditUserRoles(long pUserID = 0)
        //{
        //    MDModel<SecurityUser, SecurityUserRole> vModel = new MDModel<SecurityUser, SecurityUserRole>();

        //    vModel.Master = uBLL.GetByID(pUserID);
        //    vModel.DetailList = urBLL.GetByUserID(pUserID);

        //    if (vModel.Master == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(vModel);
        //}

        //// POST: /User/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditUserRoles(MDModel<SecurityUser, SecurityUserRole> pModel)
        //{
        //    if (urBLL.SaveList(pModel.DetailList))
        //    {
        //        return RedirectToAction("Index", "User");
        //    }
        //    else
        //    {
        //        return HttpNotFound();
        //    }

        //}

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            base.Dispose(disposing);
        }
    }
}