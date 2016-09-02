using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Factory.BusinessFactory
{
    public class NewsCommentFactory
    {
        private readonly NewsComentDataContext db = new NewsComentDataContext();

        //This method retrives all the comments for a news from the database
        public IEnumerable<NewsComment> GetNewsComments(int newsId)
        {
            var comments = db.NewsComments.ToList();
            var newsComments = comments.Where(n => n.NewsId == newsId);
            var orderComments = newsComments.OrderBy(n => n.DateCreated);
            return orderComments;
        }
    }
}
