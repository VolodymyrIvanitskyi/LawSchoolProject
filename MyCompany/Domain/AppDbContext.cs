using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCompany.Domain.Entities;

namespace MyCompany.Domain
{
    public class AppDbContext:IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ) : base(options) { }

        public DbSet<TextField> TextFields { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "659AFCC4-39A1-485B-87E6-8D93E68DDA1D",
                Name = "admin",
                NormalizedName = "ADMIN"
            }
            );

            modelbuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "06501945-32A8-49FC-A2AA-FDF369B634C3",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@email.com",
                NormalizedEmail = "ADMIN@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "1107PaSsWoRd1107"),
                SecurityStamp = string.Empty
            }) ;

            modelbuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "659AFCC4-39A1-485B-87E6-8D93E68DDA1D",
                UserId = "06501945-32A8-49FC-A2AA-FDF369B634C3"
            });

            modelbuilder.Entity<TextField>().HasData(new TextField 
            {
                Id = new System.Guid("1BE4C802-4A10-4132-BA4A-8167155C8B57"),
                CodeWord = "PageIndex",
                Title = "Головна"
            });

            modelbuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new System.Guid("E8D0E0B8-3A16-428B-937D-A92B7710AA36"),
                CodeWord = "PageServices",
                Title = "Наші послуги"
            });

            modelbuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new System.Guid("C3964B12-383A-491A-B542-334243A7B341"),
                CodeWord = "PageContacts",
                Title = "Контакти"
            });
        }

    }
}
