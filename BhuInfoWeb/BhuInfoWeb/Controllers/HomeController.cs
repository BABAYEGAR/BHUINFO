using System;
using System.Data.Entity;
using System.Web.Mvc;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Factory.BusinessFactory;
using BhuInfo.Data.Objects.Entities;
using BhuInfo.Data.Service.Enums;

namespace BhuInfoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly NewsDataContext db = new NewsDataContext();
        private readonly ContactUsDataContext dbc = new ContactUsDataContext();

        public ActionResult Index()
        {
            var news = new NewsDataFactory().GetTopNthMostRecentNews(5);
            return View(news);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            var generalNews =
                new NewsDataFactory().GetTopFiveMostRecentNewsByCategory(NewsCategoryEnum.General.ToString());
            return View(generalNews);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactUs([Bind(Include = "SenderName,Message,Email")] ContactUs contact)
        {
            if (ModelState.IsValid)
            {
                contact.DateCreated = DateTime.Now;
                dbc.Contact.Add(contact);
                dbc.SaveChanges();
                return RedirectToAction("Contact", "Home");
            }
            return RedirectToAction("Contact", "Home");
        }

        public ActionResult ViewNewsDetails(long Id)
        {
            var news = new NewsDataFactory().GetNewsById(Id);
            var newsUpdate = db.News.Find(Id);
            newsUpdate.NewsView = newsUpdate.NewsView + 1;
            db.Entry(newsUpdate).State = EntityState.Modified;
            db.SaveChanges();
            return View("ViewNewsDetails", news);
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
        public ActionResult SrcTeam()
        {
            return View("SrcTeam");
        }
    }
}