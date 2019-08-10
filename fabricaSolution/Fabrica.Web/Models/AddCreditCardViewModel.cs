namespace Fabrica.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Models;

    public class AddCreditCardViewModel : IMapWith<CreditAccount>
    {

        public string AccountId { get; set; }

        [Required]
        public string CardNumber { get; set; }

        public int Points { get; set; }

        [Required]
        public double Cash { get; set; }

        public string AccountOwnerId { get; set; }
        public virtual FabricaUser AccountOwner { get; set; }
    }
}
