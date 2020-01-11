using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCoreExamples.ManyToMany
{
    public class StudentTeacherConfiguration : IEntityTypeConfiguration<StudentTeacher>
    {
        public void Configure(EntityTypeBuilder<StudentTeacher> builder)
        {
            builder.HasKey(st => st.StudentTeacherId);
            builder.HasKey(st => new {st.StudentId, st.TeacherId});

            builder
                .HasOne<Student>(sc => sc.Student)
                .WithMany(s => s.StudentTeacher)
                .HasForeignKey(sc => sc.StudentId);

            builder
                .HasOne<Teacher>(sc => sc.Teacher)
                .WithMany(s => s.StudentTeacher)
                .HasForeignKey(sc => sc.TeacherId);
        }
    }
}
