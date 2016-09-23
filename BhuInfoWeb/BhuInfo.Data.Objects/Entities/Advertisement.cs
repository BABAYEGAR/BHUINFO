using System.ComponentModel.DataAnnotations;

namespace BhuInfo.Data.Objects.Entities
{
    public class Advertisement
    {
        public long AdvertId { get; set; }
        [Required(ErrorMessage = "This Field is compulsory")]
        public string AdvertImage { get; set; }
        [Required(ErrorMessage = "This Field is compulsory")]
        public string AdvertText { get; set; }
        [Required(ErrorMessage = "This Field is compulsory")]
        public string AdvertCompanyName { get; set; }
        public string AccessCode { get; set; }
        public string Status { get; set; }
    }
}