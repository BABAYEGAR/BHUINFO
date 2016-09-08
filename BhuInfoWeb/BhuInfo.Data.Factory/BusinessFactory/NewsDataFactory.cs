using System.Collections.Generic;
using System.Linq;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Objects.Entities;
using BhuInfo.Data.Service.Enums;

namespace BhuInfo.Data.Factory.BusinessFactory
{
    public class NewsDataFactory
    {
        private readonly NewsDataContext db = new NewsDataContext();

        //This method retrives all the news int the database
        public IEnumerable<News> GetAllNews()
        {
            var news = db.News.ToList();
            return news;
        }

        //This method retrieves top nth most recent news article
        public IEnumerable<News> GetTopNthMostRecentNews(int number)
        {
            var news = db.News.ToList();
            var latestNews = news.OrderByDescending(n => n.DateCreated);
            var topFiveNews = latestNews.Take(number);
            return topFiveNews;
        }

        //This method retrieves top five most recent news article by a category
        public IEnumerable<News> GetTopFiveMostRecentNewsByCategory(string name)
        {
            var news = db.News.ToList();
            var latestNews = news.OrderByDescending(n => n.DateCreated).Where(n => n.NewsCategory.Name == name);
            var topFiveNews = latestNews.Take(4);
            return topFiveNews;
        }

        //This method retrives a list of news by its category
        public IEnumerable<News> GetLatestNewsByCategory(NewsCategoryEnum categoryType, int number)
        {
            var news = db.News.Where(n => n.NewsCategory.Name == categoryType.ToString());
            var newList = news.OrderByDescending(n => n.DateCreated);
            var latestNews = newList.Take(number);
            return latestNews;
        }
        //This method retrives the latest news by its category
        public News GetMostRecentSingleNewsByCategory(NewsCategoryEnum categoryType)
        {
            var newsList = db.News.Where(n => n.NewsCategory.Name == categoryType.ToString());
            var latestNews = newsList.OrderByDescending(n => n.DateCreated).FirstOrDefault();
            return latestNews;
        }
        /// <summary>
        ///     This method returns a news by its ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public News GetNewsById(long Id)
        {
            var news = db.News.Find(Id);
            return news;
        }

        /// <summary>
        ///     This method returns a list in descending order by the most viewed to the list viewed
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public IEnumerable<News> GetTopNthPopularNews(int number)
        {
            var newsList = db.News.ToList();
            var orderedNews = newsList.OrderByDescending(n => n.NewsView);
            var news = orderedNews.Take(number);

            return news;
        }

        public IEnumerable<News> GetTopNthPopularNewsForCategory(NewsCategoryEnum categoryTyp, int number)
        {
            var newsList = db.News.ToList();
            var newsCategory = newsList.Where(n => n.NewsCategory.Name == categoryTyp.ToString());
            var orderedNews = newsCategory.OrderByDescending(n => n.NewsView);
            var news = orderedNews.Take(number);

            return news;
        }
    }
}