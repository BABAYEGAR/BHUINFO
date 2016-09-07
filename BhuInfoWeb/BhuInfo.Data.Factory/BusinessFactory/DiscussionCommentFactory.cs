using System.Collections.Generic;
using System.Linq;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Factory.BusinessFactory
{
    public class DiscussionCommentFactory
    {
        private readonly SchoolDiscussionCommentDataContext _db = new SchoolDiscussionCommentDataContext();

        //This method retrives all the comments for a discussion from the database
        public IEnumerable<SchoolDiscussionComment> GetDiscussionComments(int discussionId)
        {
            var comments = _db.SchoolDiscussionComments.ToList();
            var discussionComments = comments.Where(n=>n.SchoolDiscussionId == discussionId).ToList();
            var orderComments = discussionComments.OrderBy(n => n.DateCreated);
            return orderComments;
        }
    }
}