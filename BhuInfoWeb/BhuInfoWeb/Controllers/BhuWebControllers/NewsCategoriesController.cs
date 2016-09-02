using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Factory;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfoWeb.Controllers.BhuWebControllers
{
    public class NewsCategoriesController : Controller
    {
        private NewsCategoryDataContext db = new NewsCategoryDataContext();

        // GET: NewsCategories
        public ActionResult Index()
        {
            return View(db.NewsCategory.ToList());
        }

        // GET: NewsCategories/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsCategory newsCategory = db.NewsCategory.Find(id);
            if (newsCategory == null)
            {
                return HttpNotFound();
            }
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
            if (ModelState.IsValid)
            {
                newsCategory.DateCreated = DateTime.Now;
                newsCategory.DateLastModified = DateTime.Now;
                db.NewsCategory.Add(newsCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newsCategory);
        }

        // GET: NewsCategories/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsCategory newsCategory = db.NewsCategory.Find(id);
            if (newsCategory == null)
            {
                return HttpNotFound();
            }
            return View(newsCategory);
        }

        // POST: NewsCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsCategoryId,Name,DateCreated,DateLastModified,CreatedById,LastModifiedById")] NewsCategory newsCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newsCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsCategory);
        }

        // GET: NewsCategories/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsCategory newsCategory = db.NewsCategory.Find(id);
            if (newsCategory == null)
            {
                return HttpNotFound();
            }
            return View(newsCategory);
        }

        // POST: NewsCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            NewsCategory newsCategory = db.NewsCategory.Find(id);
            db.NewsCategory.Remove(newsCategory);
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
