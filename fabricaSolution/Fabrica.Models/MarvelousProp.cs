namespace Fabrica.Models
{
    using enums;
    using System.Collections;
    using System.Collections.Generic;

    public class MarvelousProp
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public MarvelousType Type { get; set; }

        public int Points { get; set; }

        public string ImageUrl { get; set; }

        public string Hashtags { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public string MarvelousCreatorId { get; set; }
        public FabricaUser MarvelousCreator { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
