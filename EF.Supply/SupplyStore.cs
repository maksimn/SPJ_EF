using EF.SupplyData.Domain;
using System.Collections.Generic;
using System.Linq;

namespace EF.SupplyData {
    public class SupplyStore {
        public List<Project> GetProjects() {
            using (SupplyDbContext context = new SupplyDbContext()) {
                List<Project> projects = context.Projects.ToList();

                return projects;
            }
        }

        public List<Project> GetProjectsLinq() {
            using (SupplyDbContext context = new SupplyDbContext()) {
                IQueryable<Project> projectsQuery = 
                    from project 
                    in context.Projects
                    select project;

                List<Project> projects = projectsQuery.ToList();

                return projects;
            }
        }
    }
}
