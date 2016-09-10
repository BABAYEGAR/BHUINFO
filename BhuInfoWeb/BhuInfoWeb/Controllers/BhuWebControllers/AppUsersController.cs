﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
    public class AppUsersController : Controller
    {
        private readonly AppUserDataContext db = new AppUserDataContext();

        // GET: AppUsers
        public ActionResult Index()
        {
            return View(db.AppUsers.ToList());
        }

        // GET: AppUsers/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var appUser = db.AppUsers.Find(id);
            if (appUser == null)
                return HttpNotFound();
            return View(appUser);
        }

        // GET: AppUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Firstname,Lastname,Email,Mobile,Password")] AppUser appUser,
            FormCollection collectedValues)
        {
            var loggedinuser = Session["bhuinfologgedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                if (loggedinuser != null)
                {
                    appUser.DateCreated = DateTime.Now;
                    appUser.DateLastModified = DateTime.Now;
                    appUser.CreatedById = loggedinuser.AppUserId;
                    appUser.LastModifiedById = loggedinuser.AppUserId;
                    appUser.Role = typeof(UserType).GetEnumName(int.Parse(collectedValues["Role"]));
                    var password = Membership.GeneratePassword(8, 1);
                    var hashPassword = new Md5Ecryption().ConvertStringToMd5Hash(password.Trim());
                    appUser.Password = new RemoveCharacters().RemoveSpecialCharacters(hashPassword);
                    var userExist = new AppUserFactory().CheckIfUserExist(appUser.Email.Trim());
                    if (userExist)
                    {
                        TempData["user"] = "This user email already exist,try a different email!";
                        TempData["notificationtype"] = NotificationType.Danger.ToString();
                        return View(appUser);
                    }
                    db.AppUsers.Add(appUser);
                    db.SaveChanges();
                    TempData["user"] = "A new user has been created!";
                    TempData["notificationtype"] = NotificationType.Success.ToString();
                    appUser.Password = password;
                    new MailerDaemon().NewUser(appUser);
                }

                else
                {
                    TempData["user"] = "Your session has expired,Login Again!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }

            return View(appUser);
        }

        // GET: AppUsers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var appUser = db.AppUsers.Find(id);
            if (appUser == null)
                return HttpNotFound();
            var roles = new SelectList(typeof(UserType).GetEnumNames());
            ViewBag.roles = roles;
            return View(appUser);
        }

        // POST: AppUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(
                 Include =
                     "AppUserId,Firstname,Lastname,Email,Mobile,Password,DateLastModified,CreatedById,LastModifiedById")
            ] AppUser appUser, FormCollection collectedValues)
        {
            var loggedinuser = Session["bhuinfologgedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                if (loggedinuser != null)
                {
                    appUser.DateCreated = Convert.ToDateTime(collectedValues["date"]);
                    appUser.DateLastModified = DateTime.Now;
                    appUser.CreatedById = long.Parse(collectedValues["createdby"]);
                    appUser.Password = collectedValues["password"];
                    appUser.LastModifiedById = loggedinuser.AppUserId;
                    appUser.Role = collectedValues["Role"];
                    db.Entry(appUser).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["user"] = "The user details has been modified successfully!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                }
                else
                {
                    TempData["user"] = "Your session has expired,Login Again!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return View(appUser);
        }

        // GET: AppUsers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var appUser = db.AppUsers.Find(id);
            if (appUser == null)
                return HttpNotFound();
            return View(appUser);
        }

        // POST: AppUsers/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var appUser = db.AppUsers.Find(id);
            db.AppUsers.Remove(appUser);
            db.SaveChanges();
            TempData["user"] = "This user has deleted succesfully!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}