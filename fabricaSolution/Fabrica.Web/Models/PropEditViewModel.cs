namespace Fabrica.Web.Models
{
    using Fabrica.Infrastructure;
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Models;
    using System.ComponentModel.DataAnnotations;

    public class PropEditViewModel : IMapWith<Prop>
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9''-'\s]{5,40}$", ErrorMessage = GlobalConstants.NameErr)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string PropType { get; set; }

        [Required]
        [Range(1.0, 5000, ErrorMessage = GlobalConstants.PriceErr)]
        public double Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [RegularExpression(@"^#[#a-zA-Z0-9''-'\s]{5,50}$", ErrorMessage = GlobalConstants.HashtagErr)]
        public string Hashtags { get; set; }

        [Required]
        [MaxLength(450, ErrorMessage = GlobalConstants.DescriptionErr)]
        public string Description { get; set; }

        public string PropCreatorId { get; set; }
        public FabricaUser PropCreator { get; set; }

    }
}
