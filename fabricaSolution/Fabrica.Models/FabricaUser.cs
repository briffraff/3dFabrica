namespace Fabrica.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;
    using enums;
    using System;

    public class FabricaUser : IdentityUser
    {
        public string FullName { get; set; }

        public GenderType Gender { get; set; }

        public ICollection<Order> Orders { get; set; }
        
    }
}
