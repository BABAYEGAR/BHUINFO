using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BhuInfo.Data.Objects.Entities
{
    public class NewsComment
    {
        public long NewsCommentId { get; set; }
        [Required]
        public string CommentBy { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Comment { get; set; }
        public long NewsId { get; set; }

        [ForeignKey("NewsId")]
        public virtual News News { get; set; }

        public DateTime DateCreated { get; set; }
    }
}