﻿namespace Fabrica.Services.Models
{
    using Infrastructure.Mapping;
    using Fabrica.Models;
    using Fabrica.Models.enums;

    public class PropServiceModel : IMapWith<Prop>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public PropType Type { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public string Hashtags { get; set; }

        public string Description { get; set; }
        
        public bool IsDeleted { get; set; }

        public FabricaUser PropOwner { get; set; }

        //TODO Add Orders
    }
}
