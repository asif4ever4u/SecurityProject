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
    public class FormActionController : BaseController
    {
        SecurityFormBLL oFormBLL;
        SecurityFormActionBLL oFormActionBLL;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            oFormBLL = new SecurityFormBLL();
            oFormActionBLL = new SecurityFormActionBLL();
        }

        public ActionResult Index(string pApplicationCode, string pFormCode)
        {
            MDModel<SecurityForm, SecurityFormAction> vModel = new MDModel<SecurityForm, SecurityFormAction>();
            vModel.Master = oFormBLL.GetByCode(pApplicationCode, pFormCode);
            vModel.DetailList = oFormActionBLL.GetByAppFormCode(vModel.Master.ApplicationCode, vModel.Master.FormCode).ToList();

            return View(vModel);
        }

        //POST: /User/Create

        public ActionResult Create(string pApplicationCode, string pFormCode)
        {
            SecurityForm vForm = oFormBLL.GetByCode(pApplicationCode, pFormCode);
            SecurityFormAction vFormAction = new SecurityFormAction();

            vFormAction.ApplicationCode = vForm.ApplicationCode;
            vFormAction.ApplicationName = vForm.ApplicationName;
            vFormAction.FormCode = vForm.FormCode;
            vFormAction.FormName = vForm.FormName;
            vFormAction.ActiveFlag = "Y";

            return View(vFormAction);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SecurityFormAction pFormAction)
        {
            if (ModelState.IsValid)
            {
                if (oFormActionBLL.Insert(pFormAction))
                {
                    return RedirectToAction("Index", new { pApplicationCode = pFormAction.ApplicationCode, pFormCode = pFormAction.FormCode });
                }
            }
            return View(pFormAction);
        }


        public ActionResult Edit(string pApplicationCode, string pFormCode, string pFormActionCode)
        {
            SecurityFormAction vFormAction = oFormActionBLL.GetByCode(pApplicationCode, pFormCode, pFormActionCode);
            if (vFormAction == null)
            {
                return HttpNotFound();
            }

            SecurityForm vForm = oFormBLL.GetByCode(pApplicationCode, pFormCode);
            vFormAction.ApplicationName = vForm.ApplicationName;
            vFormAction.FormName = vForm.FormName;

            return View(vFormAction);
        }

        //
        // POST: /User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SecurityFormAction pFormAction)
        {
            if (ModelState.IsValid)
            {
                if (oFormActionBLL.Update(pFormAction))
                {
                    return RedirectToAction("Index", new { pApplicationCode = pFormAction.ApplicationCode, pFormCode = pFormAction.FormCode });
                }
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            base.Dispose(disposing);
        }
    }
}