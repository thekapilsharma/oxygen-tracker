﻿using oxygen_tracker.Constants;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace oxygen_tracker.Models
{
    public class AuthenticationModel

    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }

        public List<string> Roles { get; set; }

        public string Token { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiration { get; set; }

        public DefaultValues.ErrorCodes ErrorCodes { get; set; }
    }
}