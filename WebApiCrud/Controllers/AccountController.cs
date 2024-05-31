using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using WebApiCrud.Entyites;
using WebApiCrud.Models;
using WebApiCrud.Repository.Abstract;
using WebApiCrud.ViewModels;

namespace WebApiCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _usermanger;
        private readonly IConfiguration configuration;

        private readonly IFileServices FileRepo;

        private readonly MyDbContext _db;

        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> usermanger, IConfiguration configuration,
            IFileServices _FileRepo, MyDbContext db, RoleManager<IdentityRole> roleManager)
        {
            _usermanger = usermanger;
            this.configuration = configuration;
            this.FileRepo = _FileRepo;
            _db = db;
            _roleManager = roleManager;
        }
        [HttpPost("Register")]
        //[FromForm]
        public async Task<IActionResult> UserRegister([FromForm] DTOAdddUser NewUser) {

            string? ImageName = null;
            //if (model.imagefile != null)
            ImageName = FileRepo.UploadImageAPI(NewUser.imagefile, null);

            if (ModelState.IsValid)
            {
                AppUser user = new AppUser() {
                    //To cut the first part and assign in UserName To aviod an error ...
                    UserName = new MailAddress(NewUser.email).User,
                    fullName = NewUser.fullName,
                    Email = NewUser.email,
                    PhoneNumber = NewUser.phoneNumber,
                    roleName = NewUser.roleName,
                    imageName =ImageName
                };
                IdentityResult result=await _usermanger.CreateAsync(user, NewUser.password);
                if (result.Succeeded)
                {
                    //return Ok(user);
                    //Add Role To Try manually ..............
                    await _usermanger.AddToRoleAsync(user, NewUser.roleName);
                    return StatusCode(200, new { message = "User Added successfully"});
                }
                else { 
                    foreach(var errors in result.Errors)
                    {
                        ModelState.AddModelError("errorModel", errors.Description);

                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        //[FromForm] 
        public async Task<IActionResult> UserLogin(DTOLoginUser userlogin) {

            if (ModelState.IsValid)
            {
                //AppUser? user = await _usermanger.FindByNameAsync(userlogin.userName);
                AppUser? user = await _usermanger.FindByEmailAsync(userlogin.email);
                if (user != null)
                {
                    if (await _usermanger.CheckPasswordAsync(user, userlogin.password))
                    {
                        //return Ok("token");
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Email, user.Email));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier,user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));
                        var roles = await _usermanger.GetRolesAsync(user);
                        foreach(var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                        }

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
                        var sc=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            claims: claims,
                            issuer: configuration["JWT:Issuer"],
                            audience: configuration["JWT:Audience"],
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: sc);

                        var _token=new{ 
                           
                            token=new JwtSecurityTokenHandler().WriteToken(token),
                            expiration=token.ValidTo,
                        };
                        var User = new
                        {
                            email = user.Email,
                            fullName = user.fullName,
                            phoneNumber = user.PhoneNumber,
                            roleName=user.roleName,
                            UserId=user.Id
                            
                        };
                        return Ok(new {tokenn= _token , user= User });
                    }
                    else {

                        //401
                        //return Unauthorized();
                        ModelState.AddModelError("Unuthorized", " Passowrd inccorect ..");
                    }
                }
                else {
                    ModelState.AddModelError("Email", " Email is Invalid ..");
                }
            }

           return BadRequest(ModelState);
        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles() { 
        
        
            var roles= _roleManager.Roles.Select(r => new { 
               
                roleName=r.Name,
                roleId= r.Id
            }).ToList();

            return Ok(roles?? new() );
        }

    }
}
