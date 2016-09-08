using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BhuInfo.Data.Objects.Entities
{
    public class NewsCategory
    {
        public long NewsCategoryId { get; set; }
        [Required(ErrorMessage = "This field is compulsory")]
        [MaxLength(100, ErrorMessage = "This field is does not support more than 100 characters")]
        [RegularExpression("^[A-Z][a-zA-Z]*$")]
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public long CreatedById { get; set; }
        public long LastModifiedById { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}