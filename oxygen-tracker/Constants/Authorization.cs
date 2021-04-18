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

        public const string default_username = "user";
        public const string default_email = "user@gmail.com";
        public const string default_password = "password";
        public const Roles default_role = Roles.User;
    }
}