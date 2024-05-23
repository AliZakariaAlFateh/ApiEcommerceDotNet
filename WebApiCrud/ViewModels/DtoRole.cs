using System.ComponentModel.DataAnnotations;

namespace WebApiCrud.ViewModels
{
    public class DtoRole
    {
        [Required]
        public string? roleName { get; set; }
    }
}
