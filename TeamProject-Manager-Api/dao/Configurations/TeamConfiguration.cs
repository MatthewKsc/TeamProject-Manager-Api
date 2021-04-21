using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao.Entitys;

namespace TeamProject_Manager_Api.dao.Configurations {
    public class TeamConfiguration : IEntityTypeConfiguration<Team> {
        public void Configure(EntityTypeBuilder<Team> builder) {

            builder.Property(t => t.NameOfTeam)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
