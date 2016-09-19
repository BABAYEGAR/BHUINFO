using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BhuInfo.Data.Objects.Entities
{
    public class SchoolDiscussionComment
    {
        public long SchoolDiscussionCommentId { get; set; }
        [Required(ErrorMessage = "This field is compulsory")]
        public string CommentBy { get; set; }
        [Required(ErrorMessage = "This field is compulsory")]
        [EmailAddress(ErrorMessage = "Invalid email address")]

        public string Email { get; set; }
        [Required(ErrorMessage = "This field is compulsory")]
        public string Comment { get; set; }
        public long SchoolDiscussionId { get; set; }

        [ForeignKey("SchoolDiscussionId")]
        public virtual SchoolDiscussion SchoolDiscussion { get; set; }

        public DateTime DateCreated { get; set; }
    }
}