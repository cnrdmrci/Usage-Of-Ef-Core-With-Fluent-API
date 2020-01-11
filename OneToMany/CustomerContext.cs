using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCoreExamples.OneToOne;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreExamples.OneToMany
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;initial catalog=[DATABASE];user id=[USERNAME];password=[PASSWORD];");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}