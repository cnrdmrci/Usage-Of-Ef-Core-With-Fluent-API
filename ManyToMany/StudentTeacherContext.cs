using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreExamples.ManyToMany
{
    public class StudentTeacherContext : DbContext
    {
        public DbSet<Student> Student { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<StudentTeacher> StudentTeacher { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;initial catalog=[DATABASE];user id=[USERNAME];password=[PASSWORD];");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentTeacherConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
