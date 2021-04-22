using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao.Entitys;

namespace TeamProject_Manager_Api.dao.Configurations {
    public class UserConfiguration : IEntityTypeConfiguration<User> {
        public void Configure(EntityTypeBuilder<User> builder) {

            builder.Property(u => u.FirstName)
                .IsRequired();
            
            builder.Property(u => u.LastName)
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
