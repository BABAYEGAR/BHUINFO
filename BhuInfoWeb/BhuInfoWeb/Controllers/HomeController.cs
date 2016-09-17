using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Factory.BusinessFactory;
using BhuInfo.Data.Objects.Entities;
using BhuInfo.Data.Service.EmailService;
using BhuInfo.Data.Service.Encryption;
using BhuInfo.Data.Service.Enums;

namespace BhuInfoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly NewsDataContext db = new NewsDataContext();
        private readonly ContactUsDataContext dbc = new ContactUsDataContext();
        private readonly EventDataContext dbd = new EventDataContext();

        public ActionResult Index()
        {
            var news = new NewsDataFactory().GetTopNthMostRecentNews(5);
            return View(news);
        }

        public ActionResult About()
        {
            var generalNews =
                new NewsDataFactory().GetTopFiveMostRecentNewsByCategory(NewsCategoryEnum.General.ToString());
            return View(generalNews);
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactUs([Bind(Include = "SenderName,Message,Email")] ContactUs contact,
            FormCollection collectedValues)
        {
            if (ModelState.IsValid)
            {
                var sendername = collectedValues["SenderName"];
                var message = collectedValues["message"];
                var email = collectedValues["Email"];
                contact.DateCreated = DateTime.Now;
                dbc.Contact.Add(contact);
                dbc.SaveChanges();
                new MailerDaemon().ContactUs(sendername, message, email);
                return RedirectToAction("Contact", "Home");
            }
            return RedirectToAction("Contact", "Home");
        }

        public ActionResult ViewNewsDetails(long Id)
        {
            if (ModelState.IsValid)
            {
                var news = new NewsDataFactory().GetNewsById(Id);
                var newsUpdate = db.News.Find(Id);
                newsUpdate.NewsView = newsUpdate.NewsView + 1;
                db.Entry(newsUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return View("ViewNewsDetails", news);
            }
            var newsToRedirect = db.News.Find(Id);
            return View(newsToRedirect);
        }

        public ActionResult LikeOrDislikeANews(long Id, string actionType)
        {
            if (ModelState.IsValid)
            {
                var newsModel = new NewsDataFactory().GetNewsById(Id);
                var news = db.News.Find(Id);
                if (actionType == NewsActionType.Like.ToString())
                {
                    news.Likes = news.Likes + 1;
                }
                else if (actionType == NewsActionType.Dislike.ToString())
                {
                    news.Dislikes = news.Dislikes + 1;
                }
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return View("ViewNewsDetails", newsModel);

            }
            var newsToRedirect = db.News.Find(Id);
            return View("ViewNewsDetails",newsToRedirect);
        }
    

    public ActionResult SportsNews()
        {
            var news = new NewsDataFactory().GetTopNthMostRecentNews(5);
            return View("Sports", news);
        }

        public ActionResult FashionNews()
        {
            var news = new NewsDataFactory().GetTopNthMostRecentNews(5);
            return View("Fashion", news);
        }
        public ActionResult GeneralNews()
        {
            var news = new NewsDataFactory().GetTopNthMostRecentNews(5);
            return View("General", news);
        }
        public ActionResult UpcomingEvent()
        {
            var events = dbd.Events.ToList();
            return View("UpcomingEvent",events);
        }
        public ActionResult SrcTeam()
        {
            return View("SrcTeam");
        }
    }
}