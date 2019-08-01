namespace Fabrica.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Models;

    public class OrderServiceModel : IMapWith<Order>
    {
        public string Id { get; set; }

        public string PropId { get; set; }
        public PropServiceModel Prop { get; set; }

        public string MarvelousPropId { get; set; }
        public MarvelousPropServiceModel MarvelousProp { get; set; }

        public string ClientId { get; set; }
        public FabricaUserServiceModel Client { get; set; }

        public DateTime OrderedOn { get; set; }
    }
}
