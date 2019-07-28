namespace Fabrica.Infrastructure
{

    public class GlobalConstants
    {
        //Roles 
        public const string AdminRoleName = "Admin";
        public const string UserRoleName = "User";

        //Password
        public const int PasswordMin = 3;
        public const int PasswordMax = 50;
        public const string PasswordEr = "The {0} must be at least {2} and at max {1} characters long.";
        public const string ConfirmPasswordEr = "The password and confirmation password do not match.";

        //Logger
        public const string RegisterUserConfirm = "User created a new account with password.";
        public const string LogoutUserConfirm = "User created a new account with password.";

        //Register,Login,Logout
        public const string RegisterRedirectTo = "~/";
        public const string LoginRedirectTo = "~/";
        public const string LogoutRedirectTo = "~/Identity/Account/Logout";

        //Identity options
        public const string AllowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        public const int UniqueChars = 0;

        //External files TODO
        //// DROPBOX
        //// FONTAWSOME
        ///



    }
}
