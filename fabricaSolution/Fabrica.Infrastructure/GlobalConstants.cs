﻿namespace Fabrica.Infrastructure
{

    public class GlobalConstants
    {
        //Project info
        public const string ProjectName = "3dFabrica";
        public const string ProjectAuthor = "Borislav Borisov";
        public const string ProjectDescription = "Retail store for 3d props";

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

        //ConfigureServices
        public const string connectionName = "DefaultConnection";

        //Configure
        public const string exceptionHandlerPath = "/Home/Error";
        public const string mvcMapRouteName = "default";
        public const string mvcMapRouteTemplate = "{controller=Home}/{action=Index}/{id?}";
        
        //Theme files
        //Custom fonts for this theme
        public const string montserrat = "https://fonts.googleapis.com/css?family=Montserrat:400,700";
        public const string lato = "https://fonts.googleapis.com/css?family=Lato:400,700,400italic,700italic";

        //External files
        //// DROPBOX
        public const string fabricaLogo = "https://dl.dropboxusercontent.com/s/een9dlze2wrlgl5/3dfabrica_logo.png?dl=0";              
        public const string favicon = "https://dl.dropboxusercontent.com/s/qzdqf284xzxuo43/favicon.png?dl=0";
        public const string avtr = "https://dl.dropboxusercontent.com/s/w0tz0f31nanjged/avtr.svg?dl=0";
        public const string freelancerCss ="https://dl.dropboxusercontent.com/s/irvyzwdu5rgk6e7/freelancer.css?dl=0";
        public const string freelancerMinCss = "https://dl.dropboxusercontent.com/s/w08h4dfknatay0r/freelancer.min.css?dl=0";
        public const string styleCss = "https://dl.dropboxusercontent.com/s/5a3bkopg1ezfi0j/style.css?dl=0";
        public const string animateMinCss = "https://dl.dropboxusercontent.com/s/din5n5w1mb1bx9s/animate.min.css?dl=0";
        public const string fontAwsomeAllCss = "https://dl.dropboxusercontent.com/s/zm4lyu6udsbewfx/all.css?dl=0";
        public const string fontAwsomeAllMinCss = "https://dl.dropboxusercontent.com/s/0kjsqsqz7b3wn2s/all.min.css?dl=0";
        public const string wowMinJs = "https://dl.dropboxusercontent.com/s/63caqyc6vk3tx2o/wow.min.js?dl=0";
        public const string mainJs = "https://dl.dropboxusercontent.com/s/z4z5zlsbxbbtp3q/main.js?dl=0"; 
        public const string freelancerMinJs = "https://dl.dropboxusercontent.com/s/jnrwfjboicmqev5/freelancer.min.js?dl=0"; 
        public const string jqBootstrapValidationJs= "https://dl.dropboxusercontent.com/s/98m2ksdihh582bq/jqBootstrapValidation.js?dl=0"; 
        public const string jqueryEasingMinJs = "https://dl.dropboxusercontent.com/s/n0ajtzyz8t3duit/jquery.easing.min.js?dl=0";
        public const string bootstrapBundleMinJs = "https://dl.dropboxusercontent.com/s/o5n47hw9rj25kov/bootstrap.bundle.min.js?dl=0";
        public const string jqueryMinJs = "https://dl.dropboxusercontent.com/s/s90p7wjc63yh9uq/jquery.min.js?dl=0";

        //// FONTAWSOME
        ///

        //// Social media
        public const string facebook = "https://facebook.com";
        public const string instagram = "https://instagram.com";

    }
}
