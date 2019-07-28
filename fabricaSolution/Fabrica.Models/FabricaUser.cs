namespace Fabrica.Models
{
    using Microsoft.AspNetCore.Identity;
    using enums;

    public class FabricaUser : IdentityUser
    {
        public string FullName { get; set; }

        public GenderType Gender { get; set; }
    }
}
