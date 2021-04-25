﻿using oxygen_tracker.Constants;
using oxygen_tracker.Registery;
using System.ComponentModel.DataAnnotations;

namespace oxygen_tracker.Models
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Phonenumber { get; set; }

        public string LastName { get; set; }

        public string Email => Phonenumber + DefaultValues.Domain;

        public string OTP { get; set; }

        public string Password => Phonenumber + "@" + FirstName.ToFirstUpperCase();

        public string Username => Phonenumber;
    }
}