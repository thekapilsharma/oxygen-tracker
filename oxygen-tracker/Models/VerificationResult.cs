using oxygen_tracker.Constants;

namespace oxygen_tracker.Models
{
    public class VerificationResult
    {
        public VerificationResult(string sid)
        {
            Sid = sid;
            IsValid = true;
        }

        public VerificationResult(DefaultValues.ErrorCodes errors)
        {
            Errors = errors;
            IsValid = false;
        }

        public bool IsValid { get; set; }

        public string Sid { get; set; }

        public DefaultValues.ErrorCodes Errors { get; set; }
    }
}