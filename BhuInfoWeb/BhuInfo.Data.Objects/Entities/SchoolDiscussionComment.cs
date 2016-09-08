using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BhuInfo.Data.Objects.Entities
{
    public class SchoolDiscussionComment
    {
        public long SchoolDiscussionCommentId { get; set; }
        [Required(ErrorMessage = "This field is compulsory")]
        [MaxLength(100, ErrorMessage = "This field is does not support more than 100 characters")]
        [RegularExpression("^[A-Z][a-zA-Z]*$")]
        public string CommentBy { get; set; }
        [Required(ErrorMessage = "This field is compulsory")]
        [MaxLength(100, ErrorMessage = "This field is does not support more than 100 characters")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
        public string Email { get; set; }
        [Required]
        public string Comment { get; set; }
        public long SchoolDiscussionId { get; set; }

        [ForeignKey("SchoolDiscussionId")]
        public virtual SchoolDiscussion SchoolDiscussion { get; set; }

        public DateTime DateCreated { get; set; }
    }
}