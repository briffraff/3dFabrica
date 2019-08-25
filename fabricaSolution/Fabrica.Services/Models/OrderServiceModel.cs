namespace Fabrica.Services.Models
{
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class OrderServiceModel : IMapWith<Order>
    {
        [Key]
        public string Id { get; set; }

        public string ClientId { get; set; }
        public FabricaUser Client { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public string OrderedOn { get; set; }

        public List<MarvelousPropOrder> MarvelousProps { get; set; }
        public List<PropOrder> Props { get; set; }
    }
}
