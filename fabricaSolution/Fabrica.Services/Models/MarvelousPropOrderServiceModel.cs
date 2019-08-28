namespace Fabrica.Services.Models
{
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Models;

    public class MarvelousPropOrderServiceModel : IMapWith<MarvelousPropOrder>
    {
        public string MarvelousPropId { get; set; }
        public MarvelousProp MarvelousProp { get; set; }

        public string OrderId { get; set; }
        public Order Order { get; set; }
    }
}
