namespace Fabrica.Services.Models
{
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Models;

    public class PropOrderServiceModel : IMapWith<PropOrder>
    {
        public string PropId { get; set; }
        public Prop Prop { get; set; }

        public string OrderId { get; set; }
        public Order Order { get; set; }
    }
}
