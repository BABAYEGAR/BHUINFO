using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Objects.Entities;
using BhuInfo.Data.Service.Enums;

namespace BhuInfoWeb.Controllers.BhuWebControllers
{
    public class NewsCategoriesController : Controller
    {
        private readonly NewsCategoryDataContext db = new NewsCategoryDataContext();

        // GET: NewsCategories
        public ActionResult Index()
        {
            return View(db.NewsCategories.ToList());
        }

        // GET: NewsCategories/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var newsCategory = db.NewsCategories.Find(id);
            if (newsCategory == null)
                return HttpNotFound();
            return View(newsCategory);
        }

        // GET: NewsCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsCategoryId,Name")] NewsCategory newsCategory)
        {
            var loggedinuser = Session["bhuinfologgedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                if (loggedinuser != null)
                {
                    newsCategory.DateCreated = DateTime.Now;
                    newsCategory.DateLastModified = DateTime.Now;
                    newsCategory.CreatedById = loggedinuser.AppUserId;
                    newsCategory.LastModifiedById = loggedinuser.AppUserId;
                    db.NewsCategories.Add(newsCategory);
                    db.SaveChanges();
                    TempData["category"] = "This category has been created successfully!";
                    TempData["notificationtype"] = NotificationType.Success.ToString();
                }
                else
                {
                    TempData["category"] = "Your session has expired,Login and try again!";
                    TempData["notificationtype"] = NotificationType.Danger.ToString();
                    RedirectToAction("Create");
                }
                return RedirectToAction("Index");
            }

            return View(newsCategory);
        }

        // GET: NewsCategories/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var newsCategory = db.NewsCategories.Find(id);
            if (newsCategory == null)
                return HttpNotFound();
            return View(newsCategory);
        }

        // POST: NewsCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsCategoryId,Name,DateCreated,DateLastModified,CreatedById,LastModifiedById")] NewsCategory newsCategory,FormCollection collectedValues)
        {
            var loggedinuser = Session["bhuinfologgedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                if (loggedinuser != null)
                {
                    newsCategory.DateLastModified = DateTime.Now;
                    newsCategory.LastModifiedById = loggedinuser.AppUserId;
                    newsCategory.CreatedById = long.Parse(collectedValues["createdby"]);
                    newsCategory.DateCreated = Convert.ToDateTime(collectedValues["date"]);
                    db.Entry(newsCategory).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["category"] = "This category has been modified successfully!";
                    TempData["notificationtype"] = NotificationType.Danger.ToString();
                }
                else
                {
                    TempData["category"] = "Your session has expired,Login and try again!";
                    TempData["notificationtype"] = NotificationType.Danger.ToString();
                    RedirectToAction("Edit");
                }
                return RedirectToAction("Index");
            }
            return View(newsCategory);
        }

        // GET: NewsCategories/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var newsCategory = db.NewsCategories.Find(id);
            if (newsCategory == null)
                return HttpNotFound();
            return View(newsCategory);
        }

        // POST: NewsCategories/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var newsCategory = db.NewsCategories.Find(id);
            db.NewsCategories.Remove(newsCategory);
            db.SaveChanges();
            TempData["category"] = "This category has deleted succesfully!";
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