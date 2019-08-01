namespace Fabrica.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Fabrica.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        [Authorize]
        public IActionResult Profile()
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public IActionResult All()
        {
            return this.View();
        }
    }
}
