using Newtonsoft.Json;
using SchoolMVC.Models;
using SchoolMVC.Models.StandardModels;
using SchoolMVC.Models.StudentModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SchoolMVC.Controllers
{
    public class StudentController : Controller
    {
        SchoolDBEntities SDBE = new SchoolDBEntities();
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Table(DataTableParam param)  //Load Student's Table Via Server Side Processing
        {
            var StudentModel = (from i in SDBE.students
                                join x in SDBE.Standards on i.standardID equals x.standardID
                                select new 
                                {
                                    i.studentID, i.studentName, x.standardID, x.standardName, i.rowVersion
                                }).ToList();

            if (!String.IsNullOrEmpty(param.sSearch))  //search
            {
                StudentModel = StudentModel.Where(i => i.studentName.Contains(param.sSearch) || 
                                                       i.standardName.Contains(param.sSearch) ||
                                                       i.rowVersion.Contains(param.sSearch)).ToList();
            }

            switch (param.iSortCol_0)  //column sorting
            {
                case 0:
                    StudentModel = param.sSortDir_0 == "asc" ? StudentModel.OrderBy(c => c.studentID).ToList() : StudentModel.OrderByDescending(c => c.studentID).ToList();
                    break;
                case 1:
                    StudentModel = param.sSortDir_0 == "asc" ? StudentModel.OrderBy(c => c.studentName).ToList() : StudentModel.OrderByDescending(c => c.studentName).ToList();
                    break;
                case 2:
                    StudentModel = param.sSortDir_0 == "asc" ? StudentModel.OrderBy(c => c.standardName).ToList() : StudentModel.OrderByDescending(c => c.standardName).ToList();
                    break;
                case 3:
                    StudentModel = param.sSortDir_0 == "asc" ? StudentModel.OrderBy(c => c.rowVersion).ToList() : StudentModel.OrderByDescending(c => c.rowVersion).ToList();
                    break;
            }

            //pagination
            var displayResult = StudentModel.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();

            return Json(new {
                aaData = displayResult,
                param.sEcho,
                iTotalRecords = StudentModel.Count(),
                iTotalDisplayRecords = StudentModel.Count()
            }, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<SelectListItem> GetStandardListItems()
        {
            List<SelectListItem> StandardListTempStorage = new List<SelectListItem>();
            var standard = SDBE.Standards.ToList();
            foreach (var item in standard)
            {
                StandardListTempStorage.Add(new SelectListItem
                {
                    Value = item.standardID.ToString(),
                    Text = item.standardName
                });
            }

            return StandardListTempStorage;
        }

        public ActionResult NewStudentModal(StudentModel studentModel)
        {
            studentModel.SelectListStandard = GetStandardListItems();
            return PartialView("Partials/Modals/_NewStudentModal", studentModel);
        }

        public ActionResult AddressModal(int? addressID)
        {
            if (addressID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            address address = SDBE.addresses.Find(addressID);

            if (address == null)
            {
                return HttpNotFound();
            }
            return PartialView("Partials/Modals/_ViewAddressModal", address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveNew(StudentModel studentModel)
        {
            student student = new student();
            if (ModelState.IsValid)
            {
                student.studentID = studentModel.StudentID;
                student.studentName = studentModel.StudentName;
                student.standardID = studentModel.StandardID;
                student.rowVersion = studentModel.RowVersion;
                student.address = new address
                {
                    addressID = studentModel.StudentID,  //foreign key
                    address1 = studentModel.Address.Address1,
                    address2 = studentModel.Address.Address2,
                    city = studentModel.Address.City,
                    state = studentModel.Address.State
                };
                SDBE.students.Add(student);
                SDBE.SaveChanges();
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateDetails(int? studentID)
        {
            if (studentID == null)
            {
                return HttpNotFound();
            }

            var model = (from s in SDBE.students
                         join a in SDBE.addresses on s.studentID equals a.addressID
                         join i in SDBE.Standards on s.standardID equals i.standardID
                         select new ViewModel
                         {
                             Student = s,
                             Address = a,
                             Standard = i
                         }).Where(s => s.Student.studentID == studentID).FirstOrDefault();

            ViewBag.StandardItems = GetStandardListItems();
            return PartialView("Partials/Modals/_DetailsModal", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModel studentModel)
        {
            student student = new student();
            if (ModelState.IsValid)
            {
                //track and include address to update...
                student = SDBE.students
                              .Where(s => s.studentID == studentModel.Student.studentID)
                              .Include(a => a.address)
                              .SingleOrDefault();
                //assign new value
                student.studentID = studentModel.Student.studentID;
                student.studentName = studentModel.Student.studentName;
                student.standardID = studentModel.Standard.standardID;
                student.rowVersion = studentModel.Student.rowVersion;
                student.address = new address
                {
                    address1 = studentModel.Student.address.address1,
                    address2 = studentModel.Student.address.address2,
                    city = studentModel.Student.address.city,
                    state = studentModel.Student.address.state
                };

                SDBE.Entry(student).State = EntityState.Modified;
                SDBE.SaveChanges();
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int? studentID)
        {
            if(studentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            student student = SDBE.students.Find(studentID);
            if(student == null)
            {
                return HttpNotFound();
            }

            SDBE.students.Remove(student);
            SDBE.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}