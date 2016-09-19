using System;
using System.ComponentModel.DataAnnotations;

namespace BhuInfo.Data.Objects.Entities
{
    public class VisitorUser
    {
        public string Key { get; set; }
        public bool IsValid { get; set; }
        public string LikeStatus { get; set; }
        public string DisikeStatus { get; set; }
        public string CommentLikeStatus { get; set; }
        public string CommentDisikeStatus { get; set; }

    }
}