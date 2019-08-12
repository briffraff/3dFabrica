namespace Fabrica.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Models.Enums;
    using Fabrica.Services.Models;

    public class LicenzeViewModel : IMapWith<LicenzeServiceModel>
    {
        [Key]
        public string LicenzeId { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
