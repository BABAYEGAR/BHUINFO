using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Factory.BusinessFactory;
using BhuInfo.Data.Objects.Entities;
using BhuInfo.Data.Service.EmailService;
using BhuInfo.Data.Service.Encryption;
using BhuInfo.Data.Service.Enums;
using BhuInfo.Data.Service.TextFormatter;

namespace BhuInfoWeb.Controllers.BhuWebControllers
{
    public class StudentsController : Controller
    {
        private StudentDataContext db = new StudentDataContext();

        // GET: Students
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(long? id)
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
        public ActionResult Create([Bind(Include = "StudentId,Firstname,Lastname,Email,Mobile,MatricNo")] Student student)
        {
            if (ModelState.IsValid)
            {
                var password = Membership.GeneratePassword(8, 1);
                var hashPassword = new Md5Ecryption().ConvertStringToMd5Hash(password.Trim());
                student.Password = new RemoveCharacters().RemoveSpecialCharacters(hashPassword);
                var userExist = new StudentFactory().CheckIfStudentExist(student.Email.Trim(),student.MatricNo.Trim());
                if (userExist)
                {
                    TempData["student"] = "This student email/matric number already exist,try a different email!";
                    TempData["notificationtype"] = NotificationType.Danger.ToString();
                    return View(student);
                }
                student.DateCreated = DateTime.Now;
                student.DateLastModified = DateTime.Now;
                db.Students.Add(student);
                db.SaveChanges();
                TempData["student"] = "A new user has been created!";
                TempData["notificationtype"] = NotificationType.Success.ToString();
                student.Password = password;
                new MailerDaemon().NewStudent(student);
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(long? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,Firstname,Lastname,Email,Mobile,MatricNo,Password,DateCreated")] Student student,FormCollection collectedValues)
        {
            if (ModelState.IsValid)
            {
                var loggedinuser = Session["bhuinfologgedinuser"] as Student;
                if (loggedinuser != null)
                {
                    student.DateCreated = Convert.ToDateTime(collectedValues["date"]);
                    student.DateLastModified = DateTime.Now;
                    student.Password = collectedValues["password"];
                    student.MatricNo = collectedValues["matric"];
                    db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["student"] = "The user details has been modified successfully!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                }
                else
                {
                    TempData["student"] = "Your session has expired,Login Again!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(long? id)
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

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
    }
}
