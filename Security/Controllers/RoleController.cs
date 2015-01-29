using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecurityEF;
using SecurityEF.BLL;
using Security.Models;

namespace Security.Controllers
{
    public class RoleController : BaseController
    {
        private SecurityRoleBLL oBLL;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            oBLL = new SecurityRoleBLL();
        }

        //
        // GET: /SecurityRole/
        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            IEnumerable<SecurityRole> vList = oBLL.GetAll();
            return View(vList);
        }

        public ActionResult Create()
        {
            SecurityRole vSecurityRole = new SecurityRole();
            vSecurityRole.ActiveFlag2 = true;
            return View(vSecurityRole);
        }

        //
        // POST: /Application/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SecurityRole pRole)
        {
            if (ModelState.IsValid)
            {
                if (oBLL.Insert(pRole))
                    {
                        return RedirectToAction("Index");
                    }
            }
            return View(pRole);
        }

        //
        // GET: /SecurityRole/Edit/5
        public ActionResult Edit(string pRoleCode)
        {
            SecurityRole vRole = oBLL.GetByCode(pRoleCode);
            if (vRole == null)
            {
                return HttpNotFound();
            }
            return View(vRole);
        }

        //
        // POST: /Group/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SecurityRole pRole)
        {
            if (ModelState.IsValid)
            {
                if (oBLL.Update(pRole))
                {
                    return RedirectToAction("Index");
                }
            }
            return View(pRole);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}