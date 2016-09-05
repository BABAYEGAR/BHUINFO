using System.Collections.Generic;
using System.Linq;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Factory.BusinessFactory
{
    public class NewsCategoryDataFactory
    {
        private readonly NewsCategoryDataContext db = new NewsCategoryDataContext();

        public IEnumerable<NewsCategory> GetAllNewsCategories()
        {
            var newsCategories = db.NewsCategories.ToList();
            return newsCategories;
        }
    }
}