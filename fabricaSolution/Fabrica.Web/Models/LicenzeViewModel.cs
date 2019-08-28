namespace Fabrica.Web.Models
{
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Services.Models;
    using System.ComponentModel.DataAnnotations;

    public class LicenzeViewModel : IMapWith<LicenzeServiceModel>
    {
        [Key]
        public string LicenzeId { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
