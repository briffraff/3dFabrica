namespace Fabrica.Models
{
    using System;

    public class Order
    {
        public string Id { get; set; }

        public string PropId { get; set; }
        public Prop Prop { get; set; }

        public string MarvelousPropId { get; set; }
        public MarvelousProp MarvelousProp { get; set; }

        public string ClientId { get; set; }
        public FabricaUser Client { get; set; }

        public DateTime OrderedOn { get; set; }
    }
}
