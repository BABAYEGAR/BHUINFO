using System.Collections.Generic;
using System.Linq;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Objects.Entities;
using BhuInfo.Data.Service.Enums;

namespace BhuInfo.Data.Factory.BusinessFactory
{
    public class SchoolDiscussionDataFactory
    {
        private readonly SchoolDiscussionDataContext db = new SchoolDiscussionDataContext();

        //This method retrives all the news int the database
        public IEnumerable<SchoolDiscussion> GetAllDiscussions()
        {
            var discussion = db.SchoolDiscussions.ToList();
            return discussion;
        }
        /// <summary>
        ///     This method returns a discussion by its ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public SchoolDiscussion GetDiscussionById(long Id)
        {
            var discussion = db.SchoolDiscussions.Find(Id);
            return discussion;
        }

    }
}