namespace Fabrica.Models
{
    using System;

    public class CreditAccount
    {
        public string Id { get; set; }

        public string CardNumber { get; set; }

        public DateTime ActiveSince { get; set; }

        public int Points { get; set; }

        public double Cash { get; set; }

        public FabricaUser AccountOwner { get; set; }

    }
}
