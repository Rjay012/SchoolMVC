using SchoolMVC.Models;
using SchoolMVC.Models.StandardModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SchoolMVC.Controllers
{
    public class StandardController : Controller
    {
        SchoolDBEntities SDBE = new SchoolDBEntities();
        // GET: Standard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Table(string searchString)
        {
            var standard = SDBE.Standards.ToList();

            if(!String.IsNullOrEmpty(searchString))
            {
                standard = standard.Where(s => s.standardName.Contains(searchString)).ToList();
            }

            return PartialView("Partials/Tables/_StandardTable", standard);
        }

        public ActionResult NewStandardModal()
        {
            return PartialView("Partials/Modals/_NewStandardModal");
        }

        public ActionResult Details(int? standardID)
        {
            if(standardID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Standard standard = SDBE.Standards.Find(standardID);

            if(standard == null)
            {
                return HttpNotFound();
            }

            return PartialView("Partials/Modals/_Edit", standard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveNewStandard(StandardModel standardModel)
        {
            Standard standard = new Standard();
            if(ModelState.IsValid)
            {
                standard.standardName = standardModel.StandardName;
                standard.description = standardModel.Description;

                SDBE.Standards.Add(standard);
                SDBE.SaveChanges();
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StandardModel standardModel)
        {
            Standard standard = new Standard();
            if(ModelState.IsValid)
            {
                standard.standardID = standardModel.StandardID;
                standard.standardName = standardModel.StandardName;
                standard.description = standardModel.Description;

                SDBE.Entry(standard).State = EntityState.Modified;
                SDBE.SaveChanges();
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int? standardID)
        {
            if(standardID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Standard standard = SDBE.Standards.Find(standardID);

            if(standard == null)
            {
                return HttpNotFound();
            }

            SDBE.Standards.Remove(standard);
            SDBE.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}