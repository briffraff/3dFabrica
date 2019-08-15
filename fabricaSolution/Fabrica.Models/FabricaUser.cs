namespace Fabrica.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;
    using Enums;
    using System;

    public class FabricaUser : IdentityUser
    {
        public FabricaUser()
        {
            this.Orders = new HashSet<Order>();
            this.CreatedProps = new HashSet<Prop>();
            this.MarvelousProps = new HashSet<MarvelousProp>();
        }

        public string FullName { get; set; }

        public override string SecurityStamp => Guid.NewGuid().ToString();

        public GenderType Gender { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Prop> CreatedProps { get; set; }
        public ICollection<MarvelousProp> MarvelousProps { get; set; }
        
        public string CreditAccountId { get; set; }
        public virtual CreditAccount CreditAccount { get; set; }

        public bool IsDeleted { get; set; }

    }
}
