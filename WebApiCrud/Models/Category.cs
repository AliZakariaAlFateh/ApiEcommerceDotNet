using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiCrud.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MinLength(12)]
        public string? categoryName { get; set; } = "";

        public virtual List<Product>? products { get; set; }

        public string? catimageName { get; set; }
        [NotMapped]
        public IFormFile? catimagefile { get; set; }
    }
}
