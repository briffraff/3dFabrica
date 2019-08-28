namespace Fabrica.Models
{
    using Enums;
    using System.Collections.Generic;

    public class MarvelousProp
    {
        public MarvelousProp()
        {
            this.Orders = new HashSet<MarvelousPropOrder>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public MarvelousType PropType { get; set; }

        public int Points { get; set; }

        public string ImageUrl { get; set; }

        public string Hashtags { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public string MarvelousCreatorId { get; set; }
        public FabricaUser MarvelousCreator { get; set; }

        public ICollection<MarvelousPropOrder> Orders { get; set; }
    }
}
