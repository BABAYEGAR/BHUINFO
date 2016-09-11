using System.Collections.Generic;
using System.Linq;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Factory.BusinessFactory
{
    public class NewsCategoryDataFactory
    {
        private readonly NewsCategoryDataContext db = new NewsCategoryDataContext();
        /// <summary>
        /// This e=mt=ethod retrives all the news categories 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NewsCategory> GetAllNewsCategories()
        {
            var newsCategories = db.NewsCategories.ToList();
            return newsCategories;
        }
    }
}