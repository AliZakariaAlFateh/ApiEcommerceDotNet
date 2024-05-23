using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Configuration;
using WebApiCrud.Entyites;
using WebApiCrud.Extentions;
using WebApiCrud.Models;
using WebApiCrud.Repository.Abstract;
using WebApiCrud.Repository.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagerGenJwtAuth(); 
//builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFileServices, FileServices>();
//builder.Services.AddDbContext<MyDbContext>(options => {

//    options.UseSqlServer("Data Source=DESKTOP-RQTVQ31\\SQLEXPRESS;Initial Catalog=WebApi_MVC;Integrated Security=True;Encrypt=False;");
//});
builder.Services.AddCors(CorsOptions => {

    CorsOptions.AddPolicy("MyPolicy", corsPolicyOptions =>
    {
        corsPolicyOptions.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        //corsPolicyOptions.WithOrigins("").WithMethods().WithHeaders("");
    });

});
builder.Services.AddDbContext<MyDbContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<MyDbContext>();
/////////////////////////////////////////

//static void ConfigureServices(IServiceCollection services)
//{
//    services.AddIdentity<AppUser, IdentityRole>(options =>
//    {
//        // Add custom validation rules for the username
//        options.User.RequireUniqueEmail = true;
//        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -._@+";
//        // Add other configuration options as needed
//    })
//    .AddEntityFrameworkStores<MyDbContext>()
//    .AddDefaultTokenProviders();

//    // Add other services configuration
//}




//////////////////////////////


//ForToken......
builder.Services.AddCustomJwtAuth(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = "/Resources"

});

app.UseCors("MyPolicy");
app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
