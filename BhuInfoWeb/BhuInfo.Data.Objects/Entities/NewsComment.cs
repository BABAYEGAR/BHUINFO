using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BhuInfo.Data.Objects.Entities
{
    public class NewsComment
    {
        public long NewsCommentId { get; set; }
        public string CommentBy { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is compulsory")]
        public string Comment { get; set; }
        public long NewsId { get; set; }

        [ForeignKey("NewsId")]
        public virtual News News { get; set; }

        public DateTime DateCreated { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}