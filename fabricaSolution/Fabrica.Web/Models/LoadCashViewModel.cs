namespace Fabrica.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Models;

    public class LoadCashViewModel : IMapWith<CreditAccount>
    {
        [Required]
        public double Cash { get; set; }
    }
}
