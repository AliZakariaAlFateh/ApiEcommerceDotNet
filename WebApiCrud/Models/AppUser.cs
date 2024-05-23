using Microsoft.AspNetCore.Identity;

namespace WebApiCrud.Models
{
    public class AppUser:IdentityUser
    {
        //public override string UserName
        //{
        //    get => base.UserName;
        //    set => base.UserName = value;
        //}
        public string? fullName { get; set; }
        public string? imageName { get; set; }
        public string? roleName { get; set; }
    }
}
