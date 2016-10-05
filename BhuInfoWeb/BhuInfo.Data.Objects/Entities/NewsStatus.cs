using System;
using System.ComponentModel.DataAnnotations;

namespace BhuInfo.Data.Objects.Entities
{
    public class NewsStatus
    {
        public long NewsStatusId { get; set; }
        public string Status { get; set; }
        public long LoggedInUserId { get; set; }
        public long NewsId { get; set; }
    }
}