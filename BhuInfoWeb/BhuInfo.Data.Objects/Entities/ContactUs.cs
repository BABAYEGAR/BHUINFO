using System;

namespace BhuInfo.Data.Objects.Entities
{
    public class ContactUs
    {
        public long ContactUsId { get; set; }
        public string SenderName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
    }
}