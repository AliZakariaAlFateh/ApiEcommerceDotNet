using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApiCrud.Models;

namespace WebApiCrud.ViewModels
{
    public class ProductWithCategoryNameVM
    {
        public int? Id { get; set; }
        public string productName { get; set; } = "";
        public float price { get; set; }
        public int Qty { get; set; }
        public int categoryid { get; set; }
        public string categoryname { get; set; } = "";

        public string imageName { get; set; } = "";
        //public Category? category { get; set; }
    }
}
