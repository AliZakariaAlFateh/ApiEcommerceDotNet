using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WebApiCrud.Models;

namespace WebApiCrud.Entyites
{
    public class MyDbContext:IdentityDbContext<AppUser>
    {
        public MyDbContext() { }


        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        //public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer("Data Source=DESKTOP-P3J7SRH\\SQLEXPRESS;Initial Catalog=GraduationProjectDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        //    base.OnConfiguring(optionsBuilder);
        //}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=db7581.public.databaseasp.net;Database=db7581;User Id=db7581;Password=2a?Tg+W4!5zY;Encrypt=False;MultipleActiveResultSets=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>()
                .HasMany(rsp => rsp.products)
                .WithOne(ps => ps.category)
                  .OnDelete(DeleteBehavior.Cascade);

            //Add two Roles MAnually..........
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "User", NormalizedName = "USER" }
                );


            builder.Entity<Order>()
                    .HasOne(o => o.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<OrderItem>()
                    .HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

        }



    }
}
