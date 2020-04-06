using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCoreExamples.OneToOne
{
    /*
     * Computed: ValueGeneratedOnAddOrUpdate
     * Identity: ValueGeneratedOnAdd    //auto increment
     * None: ValueGeneratedNever
     */
    public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.HasKey(x => x.WorkerId);

            builder
                .HasOne(x => x.ContactInfo)
                .WithOne(x => x.Worker)
                .HasForeignKey<ContactInfo>(x => x.WorkerId);

            builder.Property(x => x.FirstName).IsRequired();
            //builder.Property(x => x.LastName).HasColumnName("SoyAd").HasColumnType("varchar(max)").HasMaxLength(50);
            //builder.Property(x=>x.FirstName)
            //.HasComputedColumnSql("[LastName] + ', ' + [FirstName]");
        }
    }
}