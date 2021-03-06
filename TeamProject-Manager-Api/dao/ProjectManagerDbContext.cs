using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao.Configurations;
using TeamProject_Manager_Api.dao.Entitys;

namespace TeamProject_Manager_Api.dao {
    public class ProjectManagerDbContext : DbContext {

        public ProjectManagerDbContext(DbContextOptions<ProjectManagerDbContext> options) : base(options) {

        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.ApplyConfiguration(new AddressConfiguration());

            modelBuilder.ApplyConfiguration(new CompanyConfiguration());

            modelBuilder.ApplyConfiguration(new TeamConfiguration());

            modelBuilder.ApplyConfiguration(new ProjectConfiguration());

            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new UserProjectConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
