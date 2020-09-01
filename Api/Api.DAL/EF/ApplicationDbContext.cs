using Api.BLL.Entity;
using Microsoft.EntityFrameworkCore;

namespace Api.DAL.EF
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ConnectionStringDto _connectionString;

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Coffee> Coffees { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }

        public ApplicationDbContext(ConnectionStringDto connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer(_connectionString.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasIndex(u => u.Email)
                    .IsUnique();
            builder.Entity<User>()
                .HasMany(o => o.Orders)
                .WithOne(u => u.Client)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
