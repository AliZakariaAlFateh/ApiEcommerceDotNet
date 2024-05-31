using WebApiCrud.Models;

namespace WebApiCrud.ViewModels
{
    public class DtoOrder
    {
        public int id { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public List<DtoOrderItem> OrderItems { get; set; }


    }
}
