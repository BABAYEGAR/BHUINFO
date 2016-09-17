using System;
using System.ComponentModel.DataAnnotations;

namespace BhuInfo.Data.Objects.Entities
{
    public class ContactUs
    {
        public long ContactUsId { get; set; }
        [Required(ErrorMessage = "This field is compulsory")]
        [MaxLength(100, ErrorMessage = "This field is does not support more than 100 characters")]
        [RegularExpression("^[a-zA-Z][a-zA-Z\\s]+$",ErrorMessage = "Name/Title consist of only letters")]

        public string SenderName { get; set; }
        [Required(ErrorMessage = "This field is compulsory")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is compulsory")]
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
    }
}