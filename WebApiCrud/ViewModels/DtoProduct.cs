using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApiCrud.Models;
using System.Text.Json.Serialization;

namespace WebApiCrud.ViewModels
{
    public class DtoProduct
    {
        public int Id { get; set; }
        [Required]

        [MinLength(9)]
        public string ProductName { get; set; } = "";
        [Required]

        [RegularExpression(@"^(?!0+$)[1-9]\d*$|(\d+)(\.\d{2})", ErrorMessage = "This Field must be positive, not equal zero, not contain string or string completely , and accept only two digit after dot .")]
        public float Price { get; set; }
        [Required]

        [RegularExpression(@"^(?!0+$)[1-9]\d*$|(\d+)(\.\d{2})", ErrorMessage = "This Field must be positive, not equal zero, not contain string or string completely , and accept only two digit after dot .")]
        public int Qty { get; set; }

        public int categoryid { get; set; }

        public string? imageName { get; set; }
        //[NotMapped]
        public IFormFile? imagefile { get; set; }

    }
}
