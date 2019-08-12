namespace Fabrica.Web.Models
{
    using Fabrica.Infrastructure;
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Models;
    using System.ComponentModel.DataAnnotations;

    public class AddCreditCardViewModel : IMapWith<CreditAccount>
    {

        public string AccountId { get; set; }

        [Required]
        [RegularExpression("^[0-9]{4}-[0-9]{4}-[0-9]{4}-[0-9]{4}$",ErrorMessage = GlobalConstants.CreditCardErr)]
        public string CardNumber { get; set; }

        public int Points { get; set; }

        [Required]
        public double Cash { get; set; }

        public string AccountOwnerId { get; set; }
        public virtual FabricaUser AccountOwner { get; set; }
    }
}
