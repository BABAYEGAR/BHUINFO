using System;
using System.ComponentModel.DataAnnotations;

namespace BhuInfo.Data.Objects.Entities
{
    public class CommentStatus
    {
        public long CommentStatusId { get; set; }
        public string Status { get; set; }
        public long LoggedInUserId { get; set; }
        public long CommentId { get; set; }
    }
}