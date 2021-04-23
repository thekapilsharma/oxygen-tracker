namespace oxygen_tracker.Constants
{
    public class Authorization
    {
        public enum Roles
        {
            Administrator,
            Moderator,
            User
        }

        public const string default_username = "8983625367";
        public const string default_email = "8983625367@testdomain.com";
        public const string default_password = "8983625367@123";
        public const Roles default_role = Roles.User;
        public const string default_phonenumber = "8983625367";
    }
}