using System.ComponentModel.DataAnnotations.Schema;
using WebApiCrud.Models;

namespace WebApiCrud.ViewModels
{
    public class DtoCategory
    {
        public string? categoryName { get; set; } = "";

        public List<Product>? Product { get; set; }
        public string? catimageName { get; set; }
        public IFormFile? catimagefile { get; set; }
    }
}
