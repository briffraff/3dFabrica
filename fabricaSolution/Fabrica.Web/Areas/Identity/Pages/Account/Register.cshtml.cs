namespace Fabrica.Web.Areas.Identity.Pages.Account
{
    using Fabrica.Models;
    using Fabrica.Models.Enums;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<FabricaUser> userManager;
        private readonly SignInManager<FabricaUser> signInManager;
        private readonly ILogger<RegisterModel> logger;

        public RegisterModel(
            UserManager<FabricaUser> userManager,
            SignInManager<FabricaUser> signInManager,
            ILogger<RegisterModel> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Gender")]
            public GenderType Gender { get; set; }

            [Required]
            [StringLength(GlobalConstants.PasswordMax, ErrorMessage = GlobalConstants.PasswordEr, MinimumLength = GlobalConstants.PasswordMin)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = GlobalConstants.ConfirmPasswordEr)]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content(GlobalConstants.RegisterRedirectTo);
            if (this.ModelState.IsValid)
            {
                var user = new FabricaUser
                {
                    UserName = this.Input.Username,
                    Email = this.Input.Email,
                    Gender = this.Input.Gender,
                    FullName = this.Input.FullName
                };

                var result = await this.userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    this.logger.LogInformation(GlobalConstants.RegisterUserConfirm);

                    if (this.userManager.Users.Count() == 1)
                    {
                        await this.userManager.AddToRoleAsync(user, GlobalConstants.AdminRoleName);
                    }
                    else
                    {
                        await this.userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);
                    }

                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
