namespace Fabrica.Models
{
    using enums;

    public class MarvelousProp
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public MarvelousType Type { get; set; }

        public int Points { get; set; }

        public string ImageUrl { get; set; }

        public string Hashtags { get; set; }

        public string Description { get; set; }

        public FabricaUser MarvelousOwner { get; set; }
    }
}
