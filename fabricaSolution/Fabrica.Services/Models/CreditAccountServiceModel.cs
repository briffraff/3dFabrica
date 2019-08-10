﻿namespace Fabrica.Services.Models
{
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Models;

    public class CreditAccountServiceModel : IMapWith<CreditAccount>
    {

        public string AccountId { get; set; }

        public string CardNumber { get; set; }

        public int Points { get; set; }

        public double Cash { get; set; }

        public string AccountOwnerId { get; set; }
        public virtual FabricaUser AccountOwner { get; set; }
    }
}
