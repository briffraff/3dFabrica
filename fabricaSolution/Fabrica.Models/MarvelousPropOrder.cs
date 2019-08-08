namespace Fabrica.Models
{
    public class MarvelousPropOrder
    {
        public string MarvelousPropId { get; set; }
        public MarvelousProp MarvelousProp  { get; set; }

        public string  OrderId { get; set; }
        public Order Order { get; set; }
    }
}
