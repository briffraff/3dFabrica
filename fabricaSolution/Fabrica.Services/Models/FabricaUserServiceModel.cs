namespace Fabrica.Services.Models
{
    using Fabrica.Models.Enums;
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class FabricaUserServiceModel : IdentityUser, IMapWith<FabricaUser>
    {
        public string FullName { get; set; }

        public GenderType Gender { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Prop> CreatedProps { get; set; }
        public ICollection<MarvelousProp> MarvelousProps { get; set; }

        public string CreditAccountId { get; set; }
        public virtual CreditAccount CreditAccount { get; set; }

        public bool IsDeleted { get; set; }


    }
}
