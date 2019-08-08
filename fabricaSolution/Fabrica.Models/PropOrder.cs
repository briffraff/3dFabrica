namespace Fabrica.Models
{
    public class PropOrder
    {
        public string PropId { get; set; }
        public Prop Prop { get; set; }

        public string OrderId { get; set; }
        public Order Order { get; set; }
    }
}
