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
    public class FormController : BaseController
    {
        SecurityFormBLL oFormBLL;
        SecurityApplicationBLL oApplicationBLL;
        

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            oFormBLL = new SecurityFormBLL();
            oApplicationBLL = new SecurityApplicationBLL();
        }

        public ActionResult Index(string  pApplicationCode)
        {
            MDModel<SecurityApplication, SecurityForm> vModel = new MDModel<SecurityApplication, SecurityForm>();

            vModel.Master = oApplicationBLL.GetByCode(pApplicationCode);
            vModel.DetailList = oFormBLL.GetByApplicationCode(pApplicationCode).ToList();
            return View(vModel);
        }


        //POST: /User/Create

        public ActionResult Create(string pApplicationCode)
        {
            SecurityForm vForm = new SecurityForm();
            SecurityApplication vApplication = oApplicationBLL.GetByCode(pApplicationCode);

            vForm.ApplicationCode = vApplication.ApplicationCode;
            vForm.ApplicationName = vApplication.ApplicationName;
            vForm.ActiveFlag2 = true;
            return View(vForm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SecurityForm pSecurityForm)
        {
            if (ModelState.IsValid)
            {
                if (oFormBLL.Insert(pSecurityForm))
                {
                    SecurityFormActionBLL faBLL = new SecurityFormActionBLL();
                    List<SecurityFormAction> vList_BasicAction = new List<SecurityFormAction>();
                    vList_BasicAction.Add(GetBasic_SecurityFormAction(pSecurityForm, "001", "View"));
                    vList_BasicAction.Add(GetBasic_SecurityFormAction(pSecurityForm, "002", "Add"));
                    vList_BasicAction.Add(GetBasic_SecurityFormAction(pSecurityForm, "003", "Edit"));
                    vList_BasicAction.Add(GetBasic_SecurityFormAction(pSecurityForm, "004", "Delete"));
                    vList_BasicAction.Add(GetBasic_SecurityFormAction(pSecurityForm, "005", "Post"));
                    vList_BasicAction.Add(GetBasic_SecurityFormAction(pSecurityForm, "006", "Print"));

                    faBLL.InsertList(vList_BasicAction);

                    return RedirectToAction("Index", new { pApplicationCode = pSecurityForm.ApplicationCode });
                }
            }
            return View(pSecurityForm);
        }

        public SecurityFormAction GetBasic_SecurityFormAction(SecurityForm pSecurityForm, string pFormActionCode, string pFormActionName)
        {
            SecurityFormAction vAction = new SecurityFormAction();
            vAction.ID = 0;
            vAction.ApplicationCode = pSecurityForm.ApplicationCode;
            vAction.FormCode = pSecurityForm.FormCode;
            vAction.FormActionCode = pFormActionCode;//<<<<<<<<<<<<<<<<<
            vAction.FormActionName = pFormActionName;//<<<<<<<<<<<<<<<<<
            vAction.Remarks = string.Empty;
            vAction.ActiveFlag = "Y";
            return vAction;
        }
        public ActionResult Edit(string pApplicationCode, string FormCode)
        {
            SecurityForm vSecurityForm = oFormBLL.GetByCode(pApplicationCode, FormCode);

            if (vSecurityForm == null)
            {
                return HttpNotFound();
            }

            return View(vSecurityForm);
        }

        //
        // POST: /User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SecurityForm pSecurityForm)
        {
            if (ModelState.IsValid)
            {
                if (oFormBLL.Update(pSecurityForm))
                {
                    return RedirectToAction("Index", new { pApplicationCode = pSecurityForm.ApplicationCode });
                }
            }

            return View(pSecurityForm);
        }

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            base.Dispose(disposing);
        }
    }
}