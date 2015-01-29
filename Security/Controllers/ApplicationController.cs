using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecurityEF;
using Security.Models;
using SecurityEF.BLL;

namespace Security.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ApplicationController : BaseController
    {
        SecurityApplicationBLL oApplicationBLL;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            oApplicationBLL = new SecurityApplicationBLL();
        }

        //
        // GET: /Application/
        public ActionResult Index()
        {
            IEnumerable<SecurityApplication> vList = oApplicationBLL.GetAll();
            return View(vList);
        }

        //
        // GET: /Application/Create

        public ActionResult Create()
        {
            SecurityApplication oNew = new SecurityApplication();
            oNew.ActiveFlag2 = true;
            return View(oNew);
        }

        //
        // POST: /Application/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SecurityApplication pApplication)
        {
            if (ModelState.IsValid)
            {
                if (oApplicationBLL.Insert(pApplication))
                    {
                        return RedirectToAction("Index");
                    }
                
            }
            return View(pApplication);
        }

        //
        // GET: /Application/Edit/5

        public ActionResult Edit(string pApplicationCode)
        {
            SecurityApplication vApplication = oApplicationBLL.GetByCode(pApplicationCode);
            if (vApplication == null)
            {
                return HttpNotFound();
            }

            return View(vApplication);
        }

        //
        // POST: /Application/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SecurityApplication pApplication)
        {
            if (ModelState.IsValid)
            {
                if (oApplicationBLL.Update(pApplication))
                {
                    return RedirectToAction("Index");
                }
            }
            return View(pApplication);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}