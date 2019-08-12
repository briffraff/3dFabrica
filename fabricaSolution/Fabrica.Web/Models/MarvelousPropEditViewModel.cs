
namespace Fabrica.Web.Models
{
    using Fabrica.Infrastructure;
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Services.Models;
    using System.ComponentModel.DataAnnotations;

    public class MarvelousPropEditViewModel : IMapWith<MarvelousPropServiceModel>
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9''-'\s]{5,40}$", ErrorMessage = GlobalConstants.NameErr)]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [Range(75, 2500, ErrorMessage = GlobalConstants.PointsErr)]
        public int Points { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [RegularExpression(@"^#[#a-zA-Z0-9''-'\s]{5,50}$", ErrorMessage = GlobalConstants.HashtagErr)]
        public string Hashtags { get; set; }

        [Required]
        [MaxLength(450, ErrorMessage = GlobalConstants.DescriptionErr)]
        public string Description { get; set; }
    }
}
