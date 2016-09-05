using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhuInfo.Data.Objects.Entities
{
    public class ContactUs
    {
        public long ContactUsId { get; set; }
        public string SenderName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public  DateTime DateCreated { get; set; }

    }
}
