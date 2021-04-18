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
            //to do
        }
    }
}
