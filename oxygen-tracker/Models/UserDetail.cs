using oxygen_tracker.Constants;
using System;

namespace oxygen_tracker.Models
{
    public class UserDetail
    {
        public DefaultValues.ErrorCodes ErrorCodes { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsNewUser => String.IsNullOrEmpty(FirstName) && String.IsNullOrEmpty(PhoneNumber);
    }
}