namespace Fabrica.Models
{
    using enums;
    using System.Collections.Generic;

    public class Prop
    {
        public string  Id { get; set; }

        public string Name { get; set; }

        public PropType Type { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public string Hashtags { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public string PropOwnerId { get; set; }
        public FabricaUser PropOwner { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
