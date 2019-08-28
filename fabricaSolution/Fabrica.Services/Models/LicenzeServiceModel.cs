namespace Fabrica.Services.Models
{
    using System.ComponentModel.DataAnnotations;
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Models;
    using Fabrica.Models.Enums;

    public class LicenzeServiceModel : IMapWith<Licenze>
    {
        [Key]
        public string LicenzeId { get; set; }

        public string Name { get; set; }

        public LicenzeType Type { get; set; }

        public double Price { get; set; }

        public int bonusPoints { get; set; }
    }
}
