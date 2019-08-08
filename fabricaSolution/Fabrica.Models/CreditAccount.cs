using System.ComponentModel.DataAnnotations.Schema;

namespace Fabrica.Models
{
    using System;

    public class CreditAccount
    {

        public string Id { get; set; }

        public string CardNumber { get; set; }
        
        public int Points { get; set; }

        public double Cash { get; set; }

        public string AccountOwnerId { get; set; }
        public virtual FabricaUser AccountOwner { get; set; }

    }
}
