using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreExamples.OneToOne
{
    public class WorkerContext : DbContext
    {
        public DbSet<Worker> Worker { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Server=.\SQLEXPRESS;initial catalog=[DATABASE];user id=[USERNAME];password=[PASSWORD];");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WorkerConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}