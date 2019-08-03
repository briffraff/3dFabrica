namespace Fabrica.Services.Models
{
    using Fabrica.Models;
    using Fabrica.Models.enums;
    using Infrastructure.Mapping;
    using System.Collections.Generic;

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

        public string PropCreatorId { get; set; }
        public FabricaUser PropCreator { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
