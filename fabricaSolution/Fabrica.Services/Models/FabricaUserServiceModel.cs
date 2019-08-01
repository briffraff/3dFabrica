namespace Fabrica.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Models;
    using Microsoft.AspNetCore.Identity;
    using Fabrica.Models.enums;

    public class FabricaUserServiceModel : IdentityUser, IMapWith<FabricaUser>
    {
        public string FullName { get; set; }

        public GenderType Gender { get; set; }

        public ICollection<OrderServiceModel> Orders { get; set; }
    }
}
