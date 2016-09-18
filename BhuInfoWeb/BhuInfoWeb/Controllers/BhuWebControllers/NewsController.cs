using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Factory.BusinessFactory;
using BhuInfo.Data.Objects.Entities;
using BhuInfo.Data.Service.Enums;

namespace BhuInfoWeb.Controllers.BhuWebControllers
{
    public class NewsController : Controller
    {
        private readonly NewsDataContext db = new NewsDataContext();
        private readonly NewsComentDataContext dbc = new NewsComentDataContext();


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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var news = db.News.Find(id);
            if (news == null)
                return HttpNotFound();
            return View(news);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            var newsCategories = db.NewsCategories.Select(c => new
            {
                c.NewsCategoryId,
                c.Name
            }).ToList();
            ViewBag.Categories = new SelectList(newsCategories, "NewsCategoryId", "Name");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "NewsId,Title,Content")] News news, FormCollection collectedValues)
        {
            var user = Session["bhuinfologgedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["uploadedFile"];
                if (user != null)
                {
                    if (file != null)
                    {
                        news.DateCreated = DateTime.Now;
                        news.DateLastModified = DateTime.Now;
                        news.NewsCategoryId = long.Parse(collectedValues["NewsCategory"]);
                        news.CreatedById = user.AppUserId;
                        news.LastModifiedById = user.AppUserId;
                        news.Likes = 0;
                        news.Dislikes = 0;
                        news.NewsView = 0;
                        news.LastModifiedById = user.AppUserId;
                        news.Image = new FileUploader().UploadFile(file, UploadType.NewsImage);
                        db.News.Add(news);
                        db.SaveChanges();
                        TempData["news"] = "The article has been created Succesfully!";
                        TempData["notificationtype"] = NotificationType.Success.ToString();
                    }
                    else
                    {
                        TempData["news"] = "Please Upload an image and try again!";
                        TempData["notificationtype"] = NotificationType.Danger.ToString();
                    }
                }
                else
                {
                    TempData["news"] = "Your session has expired,Login Again!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            TempData["news"] = "Make sure you fill all the fields!";
            TempData["notificationtype"] = NotificationType.Danger.ToString();
            return RedirectToAction("Create","News",news);
        }

        // GET: News/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var newsCategories = db.NewsCategories.Select(c => new
            {
                c.NewsCategoryId,
                c.Name
            }).ToList();
            ViewBag.Categories = new SelectList(newsCategories, "NewsCategoryId", "Name");
            var news = db.News.Find(id);
            if (news == null)
                return HttpNotFound();
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "NewsId,Title,Content,Image,CreatedById")] News news,
            FormCollection collectedValues)
        {
            var user = Session["bhuinfologgedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    news.LastModifiedById = user.AppUserId;
                    news.DateLastModified = DateTime.Now;
                    news.DateCreated = Convert.ToDateTime(collectedValues["date"]);
                    news.NewsCategoryId = long.Parse(collectedValues["NewsCategory.NewsCategoryId"]);
                    news.CreatedById = long.Parse(collectedValues["createdby"]);
                    news.NewsView = int.Parse(collectedValues["newsview"]);
                    news.Likes = int.Parse(collectedValues["likes"]);
                    news.Dislikes = int.Parse(collectedValues["dislikes"]);
                    news.LastModifiedById = user.AppUserId;
                    news.Image = collectedValues["image"];
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var news = db.News.Find(id);
            if (news == null)
                return HttpNotFound();
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var news = db.News.Find(id);
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
        public ActionResult CreateNewsComment([Bind(Include = "CommentBy,Comment,Email")] NewsComment newsComments,
            FormCollection collectedValues)
        {
            var newsId = long.Parse(collectedValues["NewsId"]);
            if (ModelState.IsValid)
            {
                string[] words = { "fuck", "Fuck", "4kin","idiot","pussy","dick","blowjob","bastard","stupid" };
                var comment = collectedValues["Comment"].ToLower();
                foreach (var item in words)
                    if (comment.Contains(item))
                    {
                        TempData["news"] =
                            "Please check your words again and make sure your arent using any vulgar word!";
                        TempData["notificationtype"] = NotificationType.Danger.ToString();
                        return RedirectToAction("ViewNewsDetails","Home", new {Id = newsId});
                    }
                newsComments.DateCreated = DateTime.Now;
                newsComments.NewsId = long.Parse(collectedValues["NewsId"]);
                newsComments.Likes = 0;
                newsComments.Dislikes = 0;
                dbc.NewsComments.Add(newsComments);
                dbc.SaveChanges();
                var news = db.News.Find(long.Parse(collectedValues["NewsId"]));
                return PartialView("Comment",news);
            }
           
            TempData["news"] = "All fields are compulsory!";
            TempData["notificationtype"] = NotificationType.Danger.ToString();
            return RedirectToAction("ViewNewsDetails", "Home", new { Id = newsId });
        }
        public ActionResult LikeOrDislikeANewsComments(long Id, string actionType)
        {
          
            var newsComments = dbc.NewsComments.Find(Id);
            var newsToRedirect = dbc.News.Find(newsComments.NewsId);
            if (ModelState.IsValid)
            {
              
                if (actionType == NewsActionType.Like.ToString())
                {
                    newsComments.Likes = newsComments.Likes + 1;
                }
                else if (actionType == NewsActionType.Dislike.ToString())
                {
                    newsComments.Dislikes = newsComments.Dislikes + 1;
                }
                dbc.Entry(newsComments).State = EntityState.Modified;
                dbc.SaveChanges();
                return PartialView("_LikeOrDislikeCommentPartial",newsComments);

            }
            
            return RedirectToAction("ViewNewsDetails", "Home",newsToRedirect);
        }
    }
}