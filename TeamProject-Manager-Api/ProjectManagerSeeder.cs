using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;


//to fill up database with data
namespace TeamProject_Manager_Api {
    public class ProjectManagerSeeder {

        private readonly ProjectManagerDbContext context;

        public ProjectManagerSeeder(ProjectManagerDbContext context) {
            this.context = context;
        }

        public void Seed() {
            
        }
    }
}
