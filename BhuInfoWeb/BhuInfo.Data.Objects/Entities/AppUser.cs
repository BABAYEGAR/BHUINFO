using System;

namespace BhuInfo.Data.Objects.Entities
{
    public class AppUser
    {
        public long AppUserId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateLastModified { get; set; }

        public long CreatedById { get; set; }

        public long LastModifiedById { get; set; }

        public string DisplayName
            => Firstname + " " + Lastname;
    }
}