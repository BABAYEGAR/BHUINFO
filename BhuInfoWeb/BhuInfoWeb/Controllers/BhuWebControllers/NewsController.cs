using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Factory.BusinessFactory;
using BhuInfo.Data.Objects.Entities;
using BhuInfo.Data.Service.Encryption;
using BhuInfo.Data.Service.Enums;
using BhuInfo.Data.Service.FileUploader;

namespace BhuInfoWeb.Controllers.BhuWebControllers
{
    public class NewsController : Controller
    {
        private readonly NewsDataContext _db = new NewsDataContext();
        private readonly NewsComentDataContext _dbc = new NewsComentDataContext();
        private readonly CommentStatusDataContext _dbd = new CommentStatusDataContext();


        // GET: News
        public ActionResult Index()
        {
            var news = new NewsDataFactory().GetAllNews();
            var newsOrder = news.OrderByDescending(n => n.DateCreated);
            return View(newsOrder);
        }

        // GET: News/Details/5
        public ActionResult Details(string id)
        {
            var newsId = Convert.ToInt64(new Md5Ecryption().DecryptPrimaryKey(id, true));
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var news = _db.News.Find(newsId);
            if (news == null)
                return HttpNotFound();
            return View(news);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            var newsCategories = _db.NewsCategories.Select(c => new
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
                HttpPostedFileBase firstImage = Request.Files["firstimage"];
                HttpPostedFileBase secondImage = Request.Files["secondimage"];
                HttpPostedFileBase thirdImage = Request.Files["thirdimage"];
                if (user != null)
                {
                    if (thirdImage != null && (secondImage != null && (firstImage != null && (firstImage.FileName != "" || secondImage.FileName != "" || thirdImage.FileName != ""))))
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
                        if (firstImage.FileName != "")
                        {
                            news.Image = new FileUploader().UploadFile(firstImage, UploadType.NewsImage);
                        }
                        else
                        {
                            news.Image = null;
                        }
                        if (secondImage.FileName != "")
                        {
                            news.SecondImage = new FileUploader().UploadFile(secondImage, UploadType.NewsImage);
                        }
                        else
                        {
                            news.SecondImage = null;
                        }
                        if (thirdImage.FileName != "")
                        {
                            news.ThirdImage = new FileUploader().UploadFile(thirdImage, UploadType.NewsImage);
                        }
                        else
                        {
                            news.ThirdImage = null;
                        }
                        _db.News.Add(news);
                        _db.SaveChanges();
                        TempData["news"] = "The article has been created Succesfully!";
                        TempData["notificationtype"] = NotificationType.Success.ToString();
                    }
                    else
                    {
                        TempData["news"] = "Please Upload at least one image and try again!";
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
            return RedirectToAction("Create", "News", news);
        }

        // GET: News/Edit/5
        public ActionResult Edit(string id)
        {
            var newsId = Convert.ToInt64(new Md5Ecryption().DecryptPrimaryKey(id, true));
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var newsCategories = _db.NewsCategories.Select(c => new
            {
                c.NewsCategoryId,
                c.Name
            }).ToList();
            ViewBag.Categories = new SelectList(newsCategories, "NewsCategoryId", "Name");
            var news = _db.News.Find(newsId);
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
                    news.SecondImage = collectedValues["secondimage"];
                    news.ThirdImage = collectedValues["thirdimage"];
                    _db.Entry(news).State = EntityState.Modified;
                    _db.SaveChanges();
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
        public ActionResult Delete(string id)
        {
            var newsId = Convert.ToInt64(new Md5Ecryption().DecryptPrimaryKey(id, true));
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var news = _db.News.Find(newsId);
            if (news == null)
                return HttpNotFound();
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var newsId = Convert.ToInt64(new Md5Ecryption().DecryptPrimaryKey(id, true));
            var news = _db.News.Find(newsId);
            _db.News.Remove(news);
            _db.SaveChanges();
            TempData["news"] = "This article has been deleted Succesfully!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteNewsComment(long newsCommentId)
        {
            var newsComments = _dbc.NewsComments.Find(newsCommentId);
            var news = _db.News.Find(newsComments.NewsId);
            _dbc.NewsComments.Remove(newsComments);
            _dbc.SaveChanges();
            return PartialView("Comment", news);
        }

        // POST: News/CreateNewsComments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewsComment([Bind(Include = "Comment")] NewsComment newsComments,
            FormCollection collectedValues)
        {

            var newsId = long.Parse(collectedValues["NewsId"]);
            var news = _db.News.Find(newsId);
            var loggedinuser = Session["bhuinfologgedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                string[] words = { "fuck", "Fuck", "4kin", "idiot", "pussy", "dick", "blowjob", "bastard", "stupid" };
                var comment = collectedValues["Comment"].ToLower();
                foreach (var item in words)
                    if (comment.Contains(item))
                    {
                        TempData["news"] =
                            "Please check your words again and make sure your arent using any vulgar word!";
                        TempData["notificationtype"] = NotificationType.Danger.ToString();
                        return PartialView("Comment", news);
                    }
                newsComments.DateCreated = DateTime.Now;
                newsComments.NewsId = long.Parse(collectedValues["NewsId"]);
                newsComments.Likes = 0;
                newsComments.Dislikes = 0;
                if (loggedinuser != null)
                {
                    newsComments.CommentBy = loggedinuser.DisplayName;
                    newsComments.AppUserId = loggedinuser.AppUserId;
                    newsComments.Email = loggedinuser.Email;
                }
                _dbc.NewsComments.Add(newsComments);
                _dbc.SaveChanges();
                ModelState.Clear();
                return PartialView("Comment", news);
            }

            TempData["news"] = "All fields are compulsory!";
            TempData["notificationtype"] = NotificationType.Danger.ToString();
            return RedirectToAction("ViewNewsDetails", "Home", new { Id = newsId });
        }
        public ActionResult LikeOrDislikeANewsComments(long Id, string actionType)
        {
            var newsComments = _dbc.NewsComments.Find(Id);
            CommentStatus status = new CommentStatus();
            if (ModelState.IsValid)
            {
                var loggedinuser = Session["bhuinfologgedinuser"] as AppUser;
                if (actionType == NewsActionType.Like.ToString())
                {
                    newsComments.Likes = newsComments.Likes + 1;
                    status.CommentId = newsComments.NewsCommentId;
                    if (loggedinuser != null) status.LoggedInUserId = loggedinuser.AppUserId;
                    status.Status = NewsActionType.Like.ToString();
                }
                else if (actionType == NewsActionType.Dislike.ToString())
                {
                    newsComments.Dislikes = newsComments.Dislikes + 1;
                    status.CommentId = newsComments.NewsCommentId;
                    if (loggedinuser != null) status.LoggedInUserId = loggedinuser.AppUserId;
                    status.Status = NewsActionType.Dislike.ToString();
                }
                _dbc.Entry(newsComments).State = EntityState.Modified;
                _dbc.SaveChanges();
                _dbd.CommentStatuses.Add(status);
                _dbd.SaveChanges();
                return PartialView("_LikeOrDislikeCommentPartial", newsComments);

            }

            return PartialView("_LikeOrDislikeCommentPartial", newsComments);
        }
        [HttpGet]
        public ActionResult ReloadLikeDislikeCommentPartial(string Id)
        {
            var newsId = Convert.ToInt64(new Md5Ecryption().DecryptPrimaryKey(Id, true));
            var newsModel = new NewsCommentFactory().GetSingleNewsComments((int) newsId);
            return PartialView("_LikeOrDislikeCommentPartial", newsModel);
        }
    }
}