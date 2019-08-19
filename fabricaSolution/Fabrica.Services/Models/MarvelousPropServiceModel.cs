namespace Fabrica.Services.Models
{
    using AutoMapper;
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Models;
    using Fabrica.Models.Enums;
    using System.Collections.Generic;

    public class MarvelousPropServiceModel : IMapWith<MarvelousProp>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public MarvelousType propType { get; set; }

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
