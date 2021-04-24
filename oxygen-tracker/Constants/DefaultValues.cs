namespace oxygen_tracker.Constants
{
    public class DefaultValues
    {
        public static string IndianCode => "+91-";
        public static string Domain => "@testdomain.com";

        public enum ErrorCodes
        {
            None,
            OtpIncorrect,
            UserNotFound,
            SomethingWentWrong
        }
    }
}