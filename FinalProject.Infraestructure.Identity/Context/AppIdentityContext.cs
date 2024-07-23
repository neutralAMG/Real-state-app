using FinalProject.Infraestructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Infraestructure.Identity.Context
{
    internal class AppIdentityContext :IdentityDbContext<ApplicationUser>
    {
        public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("", m => m.MigrationsAssembly(typeof(AppIdentityContext).Assembly.FullName));
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
