namespace Fabrica.Web.Models
{
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Services.Models;
    using System.ComponentModel.DataAnnotations;

    public class MarvelousPropEditViewModel : IMapWith<MarvelousPropServiceModel>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Hashtags { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
