﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Factory;
using BhuInfo.Data.Factory.BusinessFactory;
using BhuInfo.Data.Objects.Entities;
using BhuInfo.Data.Service.Enums;

namespace BhuInfoWeb.Controllers.BhuWebControllers
{
    public class NewsController : Controller
    {
        private NewsDataContext db = new NewsDataContext();
        private NewsComentDataContext dbc = new NewsComentDataContext();
        

        // GET: News
        public ActionResult Index()
        {
            var news = new NewsDataFactory().GetAllNews();
            var newsOrder = news.OrderByDescending(n => n.DateCreated);
            return View(newsOrder);
        }

        // GET: News/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            var newsCategories = db.NewsCategories.Select(c=> new 
            {
                NewsCategoryId = c.NewsCategoryId,
                Name = c.Name
            }).ToList();
            ViewBag.Categories = new SelectList(newsCategories, "NewsCategoryId", "Name");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsId,Title,Content")] News news,FormCollection collectedValues,HttpPostedFileBase file)
        {
          var user = Session["bhuinfologgedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    news.DateCreated = DateTime.Now;
                    news.DateLastModified = DateTime.Now;
                    news.NewsCategoryId = long.Parse(collectedValues["NewsCategory"]);
                    news.CreatedById = user.AppUserId;
                    news.LastModifiedById = user.AppUserId;
                     file = Request.Files["uploadedFile"];
                    news.Image = new FileUploader().UploadFile(file, UploadType.NewsImage);
                    db.News.Add(news);
                    db.SaveChanges();
                    TempData["news"] = "The article has been created Succesfully!";
                    TempData["notificationtype"] = NotificationType.Success.ToString();
                }
                else
                {
                    TempData["news"] = "Your session has expired,Login Again!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: News/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var newsCategories = db.NewsCategories.Select(c => new
            {
                NewsCategoryId = c.NewsCategoryId,
                Name = c.Name
            }).ToList();
            ViewBag.Categories = new SelectList(newsCategories, "NewsCategoryId", "Name");
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsId,Title,Content,Image,CreatedById,LastModifiedById,DateLastModified")] News news,FormCollection collectedValues)
        {
            var user = Session["bhuinfologgedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    news.LastModifiedById = user.AppUserId;
                    news.DateLastModified = DateTime.Now;
                    db.Entry(news).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["news"] = "This article has been modified Succesfully!";
                    TempData["notificationtype"] = NotificationType.Success.ToString();
                }
                else
                {
                    TempData["news"] = "Your session has expired,Login Again!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: News/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            TempData["news"] = "This article has been deleted Succesfully!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }


        // POST: News/CreateNewsComments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewsComment([Bind(Include = "CommentBy,Comment,Email")] NewsComment newsComments, FormCollection collectedValues)
        {
            if (ModelState.IsValid)
            {
                newsComments.DateCreated = DateTime.Now;
                newsComments.NewsId = long.Parse(collectedValues["NewsId"]);
                dbc.NewsComments.Add(newsComments);
                dbc.SaveChanges();
                return RedirectToAction("ViewNewsDetails","Home",new {Id = newsComments.NewsId});
            }

            return View(newsComments.News);
        }
    }
}
