using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace BhuInfo.Data.Objects.Entities
{
    public class News
    {
        public long NewsId { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "This field is compulsory")]
        public string Title { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "This field is compulsory")]
        public string Content { get; set; }
        [DisplayName("First Image")]
        public string Image { get; set; }
        [DisplayName("Second Image")]
        public string SecondImage { get; set; }
        [DisplayName("Third Image")]
        public string ThirdImage { get; set; }
        [DisplayName("Created By")]
        public long CreatedById { get; set; }
        public long NewsCategoryId { get; set; }

        [ForeignKey("NewsCategoryId")]
        public virtual NewsCategory NewsCategory { get; set; }
        [DisplayName("Modified By")]
        public long LastModifiedById { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public int NewsView { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public virtual ICollection<NewsComment> NewComments { get; set; }
    }
}