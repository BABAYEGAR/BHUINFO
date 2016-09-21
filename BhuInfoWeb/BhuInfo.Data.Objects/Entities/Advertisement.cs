using System;
using System.ComponentModel.DataAnnotations;

namespace BhuInfo.Data.Objects.Entities
{
    public class Advertisement
    {
        public long AdvertId { get; set; }
        public string AdvertLogo { get; set; }
        public string AdvertText { get; set; }
        public string AdvertCompanyName { get; set; }
    }
}