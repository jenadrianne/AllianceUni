using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AllianceUniversity.DAL;
using AllianceUniversity.Models;
using AllianceUniversity.ViewModel;
using PagedList;

namespace AllianceUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Students
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParam = sortOrder == "Date" ? "date_desc" : "Date";
            var students = from s in db.Students
                           select s;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString)
                                            || s.FirstName.Contains(searchString)
                                           );
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName,FirstName,EnrollmentDate")] Students students)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Students.Add(students);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save record!");
            }
             


            return View(students);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }

            populateEnrolledCourse(students);
            return View(students);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LastName,FirstName,EnrollmentDate")] Students students, string[] selectedCourses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(students).State = EntityState.Modified;
                UpdateEnrollment(selectedCourses, students);
                 
                db.SaveChanges();
              
                return RedirectToAction("Index");
            }
            return View(students);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Students students = db.Students.Find(id);
            db.Students.Remove(students);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void populateEnrolledCourse (Students student)
        {
            var allCourses = db.Course; 
            var studentCourses = new HashSet<int>(student.Enrollments.Select(c => c.CourseId));
            var viewModel = new List< EnrolledCourse > ();

            foreach(var course in allCourses)
            {
                viewModel.Add(new EnrolledCourse
                {
                    CourseId = course.CourseId,
                    Title = course.Title, 
                    Enrolled = studentCourses.Contains(course.CourseId)
                });
            }

            ViewBag.Courses = viewModel; 
        }

        private void UpdateEnrollment (string[] selectedCourses, Students studentToUpdate)
        {
            Students students = db.Students
                                .Include(i => i.Enrollments)
                                .SingleOrDefault(x => x.Id == studentToUpdate.Id);
            studentToUpdate.Enrollments = students.Enrollments;

            foreach (Course course in db.Course)
            {
                if (selectedCourses.Contains(course.CourseId.ToString()))
                {
                    var enrollment = studentToUpdate.Enrollments.Where(
                        s => s.CourseId == course.CourseId).FirstOrDefault();

                    if(enrollment == null)
                    {
                        studentToUpdate.Enrollments.Add(
                            new Enrollment
                            {
                                CourseId = course.CourseId,
                                StudentId = studentToUpdate.Id,
                                Grade = null
                            }
                        );
                    }
                }else
                {
                    var enrollment = studentToUpdate.Enrollments.Where(
                       s => s.CourseId == course.CourseId).FirstOrDefault();

                    if (enrollment != null)
                    {
                        db.Enrollment.Remove(enrollment);
                    }
                }
            }
        }
    }
}
