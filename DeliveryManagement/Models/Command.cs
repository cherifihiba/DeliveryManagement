namespace DeliveryManagement.Models
{
    public class Command
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Product { get; set; }
        public DateTime Date { get; set; }
        public int? DelivererId { get; set; }
        public Deliverer? Deliverer { get; set; }
    }
}
