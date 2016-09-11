using System;
using System.ComponentModel.DataAnnotations;

namespace BhuInfo.Data.Objects.Entities
{
    public class Event
    {
        public long EventId { get; set; }
        [Required(ErrorMessage = "This field is compulsory")]
        [MaxLength(100, ErrorMessage = "This field is does not support more than 100 characters")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "This field is compulsory")]
        [MaxLength(100, ErrorMessage = "This field is does not support more than 100 characters")]
        public string Venue { get; set; }

        [Required(ErrorMessage = "This field is compulsory")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "This field is compulsory")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "This field is compulsory")]
        [MaxLength(100, ErrorMessage = "This field is does not support more than 100 characters")]
        public string Organizer { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public DateTime DateCreated { get; set; }
        public long CreatedById { get; set; }
        public long LastModifiedById { get; set; }
        public DateTime DateLastModified { get; set; }
    }
}