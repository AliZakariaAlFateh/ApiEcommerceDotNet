using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApiCrud.Models
{
    public class Product
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



        [ForeignKey("category")]
        public int categoryid { get; set; }
        [JsonIgnore]
        public Category? category { get; set; }
        public string? imageName { get; set; }
        [NotMapped]
        public IFormFile? imagefile { get; set; }

        public int? ActualCount { get; set; }= 0;

        //work as hidden the item from users even tha all count exists...
        public int? isdeleted { get; set; } = 0;

        public int? soldCount { get; set; } = 0;



    }
}
