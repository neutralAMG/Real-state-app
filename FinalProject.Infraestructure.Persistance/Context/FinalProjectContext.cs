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
