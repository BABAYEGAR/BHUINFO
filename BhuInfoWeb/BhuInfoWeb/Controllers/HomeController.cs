using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Factory;
using BhuInfo.Data.Factory.BusinessFactory;
using BhuInfo.Data.Service.Enums;

namespace BhuInfoWeb.Controllers
{
    public class HomeController : Controller
    {
        private NewsDataContext db = new NewsDataContext();
        public ActionResult Index()
        {
            var news = new NewsDataFactory().GetTopNthMostRecentNews(5);
            return View(news);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            var generalNews = new NewsDataFactory().GetTopFiveMostRecentNewsByCategory(NewsCategoryEnum.General.ToString());
            return View(generalNews);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ViewNewsDetails(long Id)
        {
            var news = new NewsDataFactory().GetNewsById(Id);
            var newsUpdate = db.News.Find(Id);
            newsUpdate.NewsView = newsUpdate.NewsView + 1;
            db.Entry(newsUpdate).State = EntityState.Modified;
            db.SaveChanges();
            return View("ViewNewsDetails",news);

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
    }
}