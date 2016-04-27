using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mytinycollege.DAL;
using mytinycollege.Models;
using PagedList;
using mytinycollege.ViewModel;

namespace mytinycollege.Controllers
{
    public class StudentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Students
        //public ActionResult Index()
        //{
        //    return View(db.Students.ToList());
        //}
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;//get current sort from UI
            ViewBag.LNameSortParm = string.IsNullOrEmpty(sortOrder) ? "lname_desc" : "";
            ViewBag.FNameSortParm = sortOrder == "fname" ? "fname_desc" : "fname";
            ViewBag.DateSortParm = sortOrder == "date" ? "date_desc" : "date";
            ViewBag.EmailSortParm = sortOrder == "email" ? "email_desc" : "email";

            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;


            //Let's get our student data
            var students = from s in db.Students select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString) ||
                    s.FirstName.Contains(searchString));
            }

            //Apply the sort order 
            switch (sortOrder)
            {

                //FirstName Asc
                case "fname":
                    students = students.OrderBy(s => s.FirstName);
                    break;

                //FirstName Desc
                case "fname_desc":
                    students = students.OrderByDescending(s => s.FirstName);
                    break;

                //EnrollmentDate Asc
                case "date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;

                //Email Asc
                case "email":
                    students = students.OrderBy(s => s.Email);
                    break;

                //Email Desc
                case "email_desc":
                    students = students.OrderByDescending(s => s.Email);
                    break;
                //LastName Desc
                case "lname_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;

                //Default LastName Asc
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }
            //return the students object as a enumerable (list)
            //return View(students.ToList());

            //Setup Pager
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(students.ToPagedList(pageNumber, pageSize));


        }

            //return View(db.Students.ToList());

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName,FirstName,Email,EnrollmentDate")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception /*ex*/)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again later!");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var studentToUpdate = db.Students.Find(id);
            if (TryUpdateModel(studentToUpdate, "", new string[] { "LastName", "FirstName", "EnrollmentDate", "Email" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }
            }
            return View(studentToUpdate);
        }
        //public ActionResult Edit([Bind(Include = "ID,LastName,FirstName,Email,EnrollmentDate")] Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(student).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(student);
        //}

            // GET: Students/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed please try again";
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Student student = db.Students.Find(id);
                db.People.Remove(student);
                db.SaveChanges();

            }
            catch (Exception)
            {

                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }

        public ActionResult Stats()
        {
            IQueryable<ViewModel.EnrollmentDateGroup> data =
                from student in db.Students
                group student by student.EnrollmentDate into dateGroup
                select new ViewModel.EnrollmentDateGroup()
                {
                    EnrollmentDate = dateGroup.Key,
                    StudentCount = dateGroup.Count()
                };
            return View(data.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
