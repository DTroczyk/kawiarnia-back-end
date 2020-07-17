using Api.BLL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.DAL.EF
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString = "Data Source=s30home.ddns.net\\BASTION-1603\\UCZELNIA,12345; Initial Catalog=Kawiarnia; User Id=Coffe; Password=coffe";

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Coffe> Coffes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasIndex(u => u.Email)
                    .IsUnique();
        }
    }
}
