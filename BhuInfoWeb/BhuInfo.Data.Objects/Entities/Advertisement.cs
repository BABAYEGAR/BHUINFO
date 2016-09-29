using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BhuInfo.Data.Objects.Entities
{
    public class Advertisement
    {
        public long AdvertisementId { get; set; }
        [DisplayName("Advert Image")]
        public string AdvertImage { get; set; }
        [DisplayName("Advert Content")]
        public string AdvertText { get; set; }
        [DisplayName("Comapany Name")]
        public string AdvertCompanyName { get; set; }
        public string AccessCode { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        [DisplayName("Advert Type")]
        public string AdvertType { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }

        public DateTime DateLastModified { get; set; }

        public long CreatedById { get; set; }

        public long LastModifiedById { get; set; }
    }
}