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


            modelBuilder.Entity<Property>(p =>
            {
                p.HasKey(p => p.Id);    

                p.HasOne(p => p.SellType).WithMany( s => s.Properties).IsRequired(true).HasForeignKey(p => p.SellTypeId);
                p.HasOne(p => p.PropertyType).WithMany( s => s.Properties).IsRequired(true).HasForeignKey(p => p.PropertyTypeId);
                p.HasMany(p => p.PropertyPerks).WithOne( s => s.Property).IsRequired(true).HasForeignKey(p => p.PropertyId);
                p.HasMany(p => p.PropertyImages).WithOne( s => s.Property).IsRequired(true).HasForeignKey(p => p.PropertyId);
                p.HasMany(p => p.FavoriteUsersProperties).WithOne( s => s.Property).IsRequired(true).HasForeignKey(p => p.PropertyId);

                p.HasIndex(p => p.SellTypeId).IsClustered(false);
                p.HasIndex(p => p.PropertyTypeId).IsClustered(false);

                p.Property(p => p.PropertyCode).IsRequired(true);
                p.Property(p => p.Description).IsRequired(true);
                p.Property(p => p.AmountOfBathrooms).IsRequired(true);
                p.Property(p => p.AmountOfBedrooms).IsRequired(true);
                p.Property(p => p.SizeInMeters).IsRequired(true);
            });
            modelBuilder.Entity<SellType>(s =>
            {
                s.HasKey(s => s.Id);
                s.HasMany(s => s.Properties).WithOne(p => p.SellType).IsRequired(true).HasForeignKey(p => p.SellTypeId);

                s.Property(p => p.Name).IsRequired(true);
            });
            modelBuilder.Entity<PropertyType>(p =>
            {
                p.HasKey(p => p.Id);
                p.HasMany(p => p.Properties).WithOne(s => s.PropertyType).IsRequired(true).HasForeignKey(p => p.PropertyTypeId);

                p.Property(p => p.Name);
            });
            modelBuilder.Entity<Perk>(p =>
            {
                p.HasKey(p => p.Id);

                p.HasMany(p => p.PropertyPerks).WithOne(p => p.Perk).IsRequired(true).HasForeignKey(p => p.PerkId).OnDelete(DeleteBehavior.Cascade);

                p.Property(p => p.Name);
            });
            modelBuilder.Entity<PropertyImage>(p =>
            {
                p.HasKey(p => p.Id);

                p.HasOne(p => p.Property).WithMany(p => p.PropertyImages).IsRequired(true).HasForeignKey(p => p.PropertyId);

                p.HasIndex(p => p.PropertyId).IsClustered(false);
                p.Property(p => p.ImgUrl).IsRequired(true);
                p.
            });
            modelBuilder.Entity<PropertyPerk>(p =>
            {
                p.HasKey(p => p.Id);
                p.HasOne(p => p.Property).WithMany(p => p.PropertyPerks).IsRequired(true).HasForeignKey(p => p.PropertyId);
                p.HasOne(p => p.Perk).WithMany(p => p.PropertyPerks).IsRequired(true).HasForeignKey(p => p.PerkId);


                p.HasIndex(p => p.PropertyId).IsClustered(false);
                p.HasIndex(p => p.PerkId).IsClustered(false);

            });

            modelBuilder.Entity<FavoriteUserProperty>(f =>
            {
                f.HasKey(f => f.Id);
                f.HasOne(f => f.Property).WithMany(p => p.FavoriteUsersProperties).IsRequired(true).HasForeignKey(p => p.PropertyId);

                f.HasIndex(f => f.PropertyId).IsClustered(true)
                ;
            });
        }
    }
}
