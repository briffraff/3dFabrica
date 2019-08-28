namespace Fabrica.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreditAccount
    {
        [Key]
        public string AccountId { get; set; }

        public string CardNumber { get; set; }

        public string AuthNumber { get; set; }

        public int Points { get; set; }

        public double Cash { get; set; }

        public string AccountOwnerId { get; set; }
        public virtual FabricaUser AccountOwner { get; set; }
    }
}
