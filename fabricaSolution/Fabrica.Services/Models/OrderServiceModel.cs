namespace Fabrica.Services.Models
{
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Models;
    using System;
    using System.Collections.Generic;

    public class OrderServiceModel : IMapWith<Order>
    {
        public string Id { get; set; }

        public string ClientId { get; set; }
        public FabricaUser Client { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public DateTime OrderedOn { get; set; }

        public ICollection<MarvelousPropOrder> MarvelousProps { get; set; }
        public ICollection<PropOrder> Props { get; set; }
    }
}
