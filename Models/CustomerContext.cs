using Microsoft.EntityFrameworkCore;

namespace CallsCRM.Models
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options): base(options){ }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Caller> Callers { get; set; }
        public DbSet<Call> Calls { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Call>().HasKey(c => c.CallId);
            modelBuilder.Entity<Call>().HasOne(c => c.Callee).WithMany(cu => cu.Calls).HasForeignKey(c => c.CustomerId);
            modelBuilder.Entity<Call>().HasOne(c => c.Caller).WithMany(ca => ca.Calls).HasForeignKey(c => c.CallerId);
            modelBuilder.Entity<Call>().OwnsOne(c => c.Time);

            modelBuilder.Entity<Customer>().HasIndex(cu => cu.PhoneNumber).IsUnique();
            modelBuilder.Entity<Customer>().HasIndex(cu => cu.Email).IsUnique();

            modelBuilder.Entity<Caller>().HasIndex(ca => ca.Login).IsUnique();
        }
    }
}