using Api.BLL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.DAL.EF
{
    public class ApplicationDbContext : DbContext
    {
        private static string _connectionString;

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Coffe> Coffes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public static void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }

        //public ApplicationDbContext(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
