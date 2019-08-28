namespace Fabrica.Web.Models
{
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Models;
    using System.ComponentModel.DataAnnotations;

    public class LoadCashViewModel : IMapWith<CreditAccount>
    {
        [Required]
        public double Cash { get; set; }
    }
}
