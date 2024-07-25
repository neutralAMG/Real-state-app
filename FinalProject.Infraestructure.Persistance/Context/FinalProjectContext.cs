using FinalProject.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Infraestructure.Persistance.Context
{
    public class FinalProjectContext : DbContext
    {
        public DbSet<Property> Properties { get; set; }
        public DbSet<SellType> SellTypes { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Perk> Perks { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<PropertyPerk> PropertyPerks { get; set; }
        public DbSet<FavoriteUserProperty> FavoriteUserProperties { get; set; }


        public FinalProjectContext() { }
        public FinalProjectContext(DbContextOptions<FinalProjectContext> options) : base(options) 
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-LL4GL68; Database=FinalProject; Integrated Security=true; TrustServerCertificate=true;", m => m.MigrationsAssembly(typeof(FinalProjectContext).Assembly.FullName));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}
