namespace Fabrica.Web.Models
{
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Models;
    using Fabrica.Services.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class OrderListViewModel : IMapWith<OrderServiceModel>
    {
        [Key]
        public string Id { get; set; }

        public string ClientId { get; set; }
        public FabricaUser Client { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public string OrderedOn { get; set; }

        public ICollection<MarvelousPropOrder> MarvelousProps { get; set; }
        public ICollection<PropOrder> Props { get; set; }

    }
}
