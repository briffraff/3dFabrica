namespace Fabrica.Models
{
    using System;
    using System.Collections.Generic;

    public class Order
    {
        public Order()
        {
            this.Props = new HashSet<PropOrder>();
            this.MarvelousProps = new HashSet<MarvelousPropOrder>();
        }

        public string Id { get; set; }

        public string ClientId { get; set; }
        public FabricaUser Client { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public DateTime OrderedOn { get; set; }

        public ICollection<PropOrder> Props { get; set; }
        public ICollection<MarvelousPropOrder> MarvelousProps { get; set; }
    }
}