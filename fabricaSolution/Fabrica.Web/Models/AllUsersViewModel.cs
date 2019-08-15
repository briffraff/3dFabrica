namespace Fabrica.Web.Models
{
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Models;
    using Microsoft.AspNetCore.Identity;

    public class AllUsersViewModel : IdentityUser, IMapWith<FabricaUser>
    {
        public string Username { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public bool IsDeleted { get; set; }
    }
}
