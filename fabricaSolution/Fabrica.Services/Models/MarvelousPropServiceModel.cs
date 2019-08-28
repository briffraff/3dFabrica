namespace Fabrica.Services.Models
{
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Models;
    using Fabrica.Models.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class MarvelousPropServiceModel : IMapWith<MarvelousProp>
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Type")]
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
