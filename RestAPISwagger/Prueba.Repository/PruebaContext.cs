using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Prueba.Domain;
using System.Collections.Generic;

namespace Prueba.Repository
{
    public class PruebaContext : DbContext
    {
        public PruebaContext(DbContextOptions<PruebaContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
		
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost\\SQLEXPRESS;database=Prueba;trusted_connection=true;"); //comment out this once test is done
        }

        // Entities mapped to EF Core will then be used along CQRS methods (RepositoryEntityFrameworkCQRS.cs)
        public DbSet<Region> Regiones { get; set; }
        public DbSet<Comuna> Comunas { get; set; }
    }
}
