﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BhuInfo.Data.Objects.Entities
{
    public class News
    {
        public long NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public long CreatedById { get; set; }
        public long NewsCategoryId { get; set; }

        [ForeignKey("NewsCategoryId")]
        public virtual NewsCategory NewsCategory { get; set; }

        public long LastModifiedById { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public int NewsView { get; set; }

        public virtual ICollection<NewsComment> NewComments { get; set; }
    }
}