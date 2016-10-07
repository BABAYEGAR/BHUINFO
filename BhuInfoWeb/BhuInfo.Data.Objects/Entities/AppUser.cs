﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BhuInfo.Data.Objects.Entities
{
    public class AppUser
    {
        public long AppUserId { get; set; }
        [Required(ErrorMessage = "This field is compulsory")]
        [MaxLength(100, ErrorMessage = "This field is does not support more than 100 characters")]
        [RegularExpression("[a-zA-Z ]*$")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "This field is compulsory")]
        [MaxLength(100, ErrorMessage = "This field is does not support more than 100 characters")]
        [RegularExpression("^[a-zA-Z ]*$")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "This field is compulsory")]
        [MaxLength(100, ErrorMessage = "This field is does not support more than 100 characters")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "This Email is invalid!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is compulsory")]
        [MaxLength(100, ErrorMessage = "This field is does not support more than 100 characters")]
        [RegularExpression("^[0-9]*$")]
        public string Mobile { get; set; }
        public string MatricNumber { get; set; }

        public string Password { get; set; }
        public string Role { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateLastModified { get; set; }

        public long CreatedById { get; set; }

        public long LastModifiedById { get; set; }
        public string AppUserImage { get; set; }

        public string DisplayName
            => Firstname + " " + Lastname;
    }
}