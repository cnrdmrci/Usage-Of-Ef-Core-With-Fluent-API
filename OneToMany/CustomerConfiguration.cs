using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCoreExamples.OneToMany
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.CustomerId);

            builder
                .HasMany(x => x.Order)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);

            builder.Property(x => x.Name).IsRequired();
        }
    }
}