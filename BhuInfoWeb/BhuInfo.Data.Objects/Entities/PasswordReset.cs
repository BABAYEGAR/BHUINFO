using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BhuInfo.Data.Objects.Entities
{
    public class PasswordReset
    {
        public long PasswordResetId { get; set; }
		public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
		public long AppUserId { get; set; }
    }
}