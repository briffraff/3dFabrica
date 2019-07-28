using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Fabrica.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Fabrica.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<FabricaUser> signInManager;
        private readonly ILogger<LogoutModel> logger;

        public LogoutModel(SignInManager<FabricaUser> signInManager, ILogger<LogoutModel> logger)
        {
            this.signInManager = signInManager;
            this.logger = logger;
        }

        public async Task<IActionResult> OnGet(string returnUrl = null)
        {
            await this.signInManager.SignOutAsync();
            this.logger.LogInformation("User logged out.");

            returnUrl = "/Identity/Account/Logout";

            if (returnUrl != null)
            {
                return this.Redirect(returnUrl);
            }
            else
            {
                return Page();
            }
        }

    }
}