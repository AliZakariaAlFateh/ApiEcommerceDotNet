using System.ComponentModel.DataAnnotations;

namespace WebApiCrud.ViewModels
{
    public class DTOLoginUser
    {
        //[Required]
        //public string userName { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
