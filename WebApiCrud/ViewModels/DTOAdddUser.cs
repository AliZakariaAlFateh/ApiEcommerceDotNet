using System.ComponentModel.DataAnnotations;

namespace WebApiCrud.ViewModels
{
    public class DTOAdddUser
    {
        [Required]
        
        //public string userName { get; set; }
        public string? fullName { get; set; }
        [Required]
        public string password { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string phoneNumber { get; set; }

        public IFormFile? imagefile { get; set; }
        public string? imageName { get; set; }

        public string? roleName { get; set; }
    }
}
