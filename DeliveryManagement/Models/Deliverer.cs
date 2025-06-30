using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DeliveryManagement.Models
{
    public class Deliverer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public List<Command> Commands { get; set; } = new List<Command>();
    }
}
