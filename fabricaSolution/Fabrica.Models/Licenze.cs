namespace Fabrica.Models
{
    using System.ComponentModel.DataAnnotations;
    using Fabrica.Models.Enums;

    public class Licenze
    {
        [Key]
        public string LicenzeId { get; set; }

        public string Name { get; set; }

        public LicenzeType Type { get; set; }

        public double Price { get; set; }

        public int bonusPoints { get; set; }
    }
}
