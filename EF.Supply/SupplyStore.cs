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

        public List<Tuple<int, int, int>> GetShipperIdPartIdProjectIdWhereTheyAreInTheSameCityLinq() {
            using (SupplyDbContext context = new SupplyDbContext()) {
                var query = from supply in context.Supplies
                            where supply.Shipper.City == supply.Part.City && supply.Shipper.City == supply.Project.City
                            select Tuple.Create(supply.Shipper.Id, supply.Part.Id, supply.Project.Id);

                return query.ToList();
            }
        }

        public List<Part> GetPartsWhichShipperFromCityLinq(string city) {
            using (SupplyDbContext context = new SupplyDbContext()) {
                var partsWithDuplicates = from supply in context.Supplies
                            where supply.Shipper.City == city
                            select supply.Part;
                
                var parts = from part in partsWithDuplicates
                                  group part by part.Id into distinctParts
                                  select distinctParts.FirstOrDefault();

                return parts.ToList();
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
