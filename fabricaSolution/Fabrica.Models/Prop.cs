namespace Fabrica.Models
{
    using Enums;
    using System.Collections.Generic;

    public class Prop
    {
        public Prop()
        {
            this.Orders = new List<PropOrder>();
        }

        public string  Id { get; set; }

        public string Name { get; set; }

        public PropType PropType { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public string Hashtags { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public string PropCreatorId { get; set; }
        public FabricaUser PropCreator { get; set; }

        public ICollection<PropOrder> Orders { get; set; }
    }
}
