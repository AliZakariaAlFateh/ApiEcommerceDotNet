namespace WebApiCrud.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
