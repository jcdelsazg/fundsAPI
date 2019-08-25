using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using funds_api.Implementation.Models;

namespace funds_api.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Fund> Funds { get; set; }
        public DbSet<ValueFund> Values { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Fund>().ToTable("Funds");
            builder.Entity<Fund>().HasKey(p => p.Id);
            builder.Entity<Fund>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Fund>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Fund>().Property(p => p.Description).IsRequired().HasMaxLength(255);

            /*builder.Entity<Fund>().HasData(
                new Fund { Id = 1, Name = "Fund1", Description = "First fund" },
                new Fund { Id = 2, Name = "Fund2", Description = "Second fund" }
                );*/

            builder.Entity<ValueFund>().ToTable("ValueFund");
            builder.Entity<ValueFund>().HasKey(p => p.Id);
            builder.Entity<ValueFund>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<ValueFund>().Property(p => p.DateFund).IsRequired();
            builder.Entity<ValueFund>().Property(p => p.Value).IsRequired();

            /*builder.Entity<ValueFund>().HasData(
                new ValueFund { Id = 1, DateFund = DateTime.UtcNow, Value = 100, FundId = 1},
                new ValueFund { Id = 2, DateFund = DateTime.UtcNow, Value = 200, FundId = 2 }
                );*/
        }
    }
}
