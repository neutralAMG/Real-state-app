using FinalProject.Infraestructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.Infraestructure.Identity.Context
{
    public class AppIdentityContext :IdentityDbContext<ApplicationUser>
    {
        public AppIdentityContext() { }
        public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-LL4GL68; Database=FinalProjectUsers; Integrated Security=true; TrustServerCertificate=true;", m => m.MigrationsAssembly(typeof(AppIdentityContext).Assembly.FullName));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("Identity");

            builder.Entity<ApplicationUser>(u =>
            {
                u.ToTable(name: "Users");
            });
            builder.Entity<IdentityUserRole<string>>(r =>
            {
                r.ToTable(name: "Roles");
            });

            builder.Entity<IdentityUserLogin<string>>(u =>
            {
                u.ToTable(name: "UsersLogins");
            });

        }
    }
}
