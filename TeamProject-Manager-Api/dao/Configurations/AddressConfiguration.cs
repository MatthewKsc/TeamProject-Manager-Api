using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao.Entitys;

namespace TeamProject_Manager_Api.dao.Configurations {
    public class AddressConfiguration : IEntityTypeConfiguration<Address> {
        public void Configure(EntityTypeBuilder<Address> builder) {


            builder.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.PostalCode)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(85);

            builder.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(60);
        }
    }
}
