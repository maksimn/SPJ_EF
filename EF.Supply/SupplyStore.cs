using EF.SupplyData.Domain;
using System.Collections.Generic;
using System.Linq;
using System;

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

        public List<Project> GetProjectsInCity(string city) {
            using (SupplyDbContext context = new SupplyDbContext()) {
                IQueryable<Project> query = context.Projects.Where(p => p.City == city);

                return query.ToList();
            }
        }

        public List<Project> GetProjectsInCityLinq(string city) {
            using (SupplyDbContext context = new SupplyDbContext()) {
                IQueryable<Project> query =
                    from project
                    in context.Projects
                    where project.City == city
                    select project;

                return query.ToList();
            }
        }

        public List<int> GetShipperIdsForProjectId(int projectId) {
            using (SupplyDbContext context = new SupplyDbContext()) {
                IQueryable<int> query = 
                    context.Supplies.Where(supply => supply.Project.Id == projectId)
                                    .Select(supply => supply.Shipper.Id);

                return query.ToList();
            }
        }

        public List<int> GetShipperIdsForProjectIdLinq(int projectId) {
            using (SupplyDbContext context = new SupplyDbContext()) {
                IQueryable<int> query =
                    from supply
                    in context.Supplies
                    where supply.Project.Id == projectId
                    select supply.Shipper.Id;

                return query.ToList();
            }
        }

        // Auxilliary for tests
        public Project GetProjectWithName(string projectName) {
            using (SupplyDbContext context = new SupplyDbContext()) {
                var query =
                    context.Projects.Where(project => project.Name == projectName);

                return query.FirstOrDefault();
            }
        }
    }
}
