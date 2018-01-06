using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            modelBuilder.Entity<Call>().HasKey(c => new { c.CallerId, c.CustomerId });
            modelBuilder.Entity<Call>().HasOne(c => c.Callee).WithMany(cu => cu.Calls).HasForeignKey(c => c.CustomerId);
            modelBuilder.Entity<Call>().HasOne(c => c.Caller).WithMany(ca => ca.Calls).HasForeignKey(c => c.CallerId);
            modelBuilder.Entity<Call>().OwnsOne(c => c.Time);

            modelBuilder.Entity<Customer>().HasAlternateKey(cu => cu.PhoneNumber);
            modelBuilder.Entity<Customer>().HasAlternateKey(cu => cu.Email);

            modelBuilder.Entity<Caller>().HasAlternateKey(ca => ca.Login);
        }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public ICollection<Call> Calls { get; set; }
    }

    public class Caller
    {
        public int CallerId { get; set; }
        [Required]
        public string Login { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public ICollection<Call> Calls { get; set; }        
    }

    public class Call
    {
        public CallTime Time { get; set; }
        public int CustomerId { get; set; }
        public Customer Callee { get; set; }
        public int CallerId { get; set; }
        public Caller Caller { get; set; }
    }

    public class CallTime
    {
        public CallTime()
        {
            this.StartTime = DateTime.Now;
            this.EndTime = DateTime.Now;
        }
        
        public CallTime(DateTime StartTime, DateTime EndTime)
        {
            this.StartTime = StartTime;
            this.EndTime = EndTime;
        }

        public DateTime StartTime { get; protected set; }
        public DateTime EndTime { get; protected set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Duration => EndTime.Subtract(StartTime).TotalSeconds;
        public bool Success => StartTime != EndTime;
        public override bool Equals(object obj)
        {
            var callTime = obj as CallTime;

            if (callTime == null)
            {
                return false;
            }

            return StartTime.Equals(callTime.StartTime) && EndTime.Equals(callTime.EndTime);
        }
        public override int GetHashCode()
        {
            return StartTime.GetHashCode() ^ EndTime.GetHashCode();
        }
    }
}