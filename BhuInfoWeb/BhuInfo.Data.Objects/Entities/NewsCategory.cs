using System;
using System.Collections.Generic;

namespace BhuInfo.Data.Objects.Entities
{
    public class NewsCategory
    {
        public long NewsCategoryId { get; set; }
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public long CreatedById { get; set; }
        public long LastModifiedById { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}