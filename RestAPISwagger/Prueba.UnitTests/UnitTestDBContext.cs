using Microsoft.EntityFrameworkCore;
using Prueba.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prueba.UnitTests
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected DatabaseContext()
        {
        }
        public DbSet<Region> Users { get; set; }
    }
}
