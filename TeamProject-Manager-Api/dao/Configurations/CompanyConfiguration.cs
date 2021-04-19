using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao.Entitys;

namespace TeamProject_Manager_Api.dao.Configurations {
    public class CompanyConfiguration : IEntityTypeConfiguration<Company> {
        public void Configure(EntityTypeBuilder<Company> builder) {

            builder.Property(c => c.CompanyName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.SizeOfComapny)
               .IsRequired();
        }
    }
}
