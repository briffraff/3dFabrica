using Fabrica.Infrastructure;

namespace Fabrica.Web.Models
{
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Models;
    using System.ComponentModel.DataAnnotations;

    public class PropEditViewModel : IMapWith<Prop>
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9''-'\s]{5,40}$",ErrorMessage = GlobalConstants.NameErr)]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [Range(1.0,5000, ErrorMessage = GlobalConstants.PriceErr)]
        public double Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [RegularExpression(@"^#[#a-zA-Z0-9''-'\s]{5,50}$",ErrorMessage = GlobalConstants.HashtagErr)]
        public string Hashtags { get; set; }

        [Required]
        [MaxLength(450,ErrorMessage = GlobalConstants.DescriptionErr)]
        public string Description { get; set; }

        public FabricaUser PropCreator { get; set; }

    }
}
