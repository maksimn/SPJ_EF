using EF.SupplyData;
using EF.SupplyData.Domain;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;

[SetUpFixture]
public class TestsSetupClass {
    SupplyDbContext supplyDbContext = null;

    [OneTimeSetUp]
    public void GlobalSetup() {
        GlobalTeardown();
        using (supplyDbContext = new SupplyDbContext()) {
            DbSet<Shipper> shippers = supplyDbContext.Shippers;
            Shipper shipper1 = new Shipper() { Name = "Smith", Status = 20, City = "London" };
            Shipper shipper2 = new Shipper() { Name = "Jones", Status = 10, City = "Paris" };
            Shipper shipper3 = new Shipper() { Name = "Blake", Status = 30, City = "Paris" };
            Shipper shipper4 = new Shipper() { Name = "Clarke", Status = 20, City = "London" };
            Shipper shipper5 = new Shipper() { Name = "Adams", Status = 30, City = "Athens" };

            Shipper[] shipperArray = new Shipper[] { shipper2, shipper3, shipper4, shipper5 };

            DbSet<Part> parts = supplyDbContext.Parts;
            Part part1 = new Part() { Name = "Nut", Color = "Red", Weight = 12.0, City = "London" };
            Part part2 = new Part() { Name = "Bolt", Color = "Green", Weight = 17.0, City = "Paris" };
            Part part3 = new Part() { Name = "Screw", Color = "Blue", Weight = 17.0, City = "Oslo" };
            Part part4 = new Part() { Name = "Screw", Color = "Red", Weight = 14.0, City = "London" };
            Part part5 = new Part() { Name = "Cam", Color = "Blue", Weight = 12.0, City = "Paris" };
            Part part6 = new Part() { Name = "Cog", Color = "Rad", Weight = 19.0, City = "London" };
            Part[] partArray = new Part[] { part1, part2, part3, part4, part5, part6 };

            DbSet<Project> projects = supplyDbContext.Projects;
            Project project1 = new Project() { Name = "Sorter", City = "Paris" };
            Project project2 = new Project() { Name = "Display", City = "Rome" };
            Project project3 = new Project() { Name = "OCR", City = "Athens" };
            Project project4 = new Project() { Name = "Console", City = "Athens" };
            Project project5 = new Project() { Name = "PAID", City = "London" };
            Project project6 = new Project() { Name = "EDS", City = "Oslo" };
            Project project7 = new Project() { Name = "Tape", City = "London" };

            DbSet<Supply> supplies = supplyDbContext.Supplies;
            Supply supply1 = new Supply() { Shipper = shipper1, Part = part1, Project = project1, Quantity = 200 };
            Supply supply2 = new Supply() { Shipper = shipper1, Part = part1, Project = project4, Quantity = 700 };
            Supply supply3 = new Supply() { Shipper = shipper2, Part = part3, Project = project1, Quantity = 400 };
            Supply supply4 = new Supply() { Shipper = shipper2, Part = part3, Project = project2, Quantity = 200 };
            Supply supply5 = new Supply() { Shipper = shipper2, Part = part3, Project = project3, Quantity = 200 };
            Supply supply6 = new Supply() { Shipper = shipper2, Part = part3, Project = project4, Quantity = 500 };
            Supply supply7 = new Supply() { Shipper = shipper2, Part = part3, Project = project5, Quantity = 600 };
            Supply supply8 = new Supply() { Shipper = shipper2, Part = part3, Project = project6, Quantity = 400 };
            Supply supply9 = new Supply() { Shipper = shipper2, Part = part3, Project = project7, Quantity = 800 };
            Supply supply10 = new Supply() { Shipper = shipper2, Part = part5, Project = project2, Quantity = 100 };
            Supply supply11 = new Supply() { Shipper = shipper3, Part = part3, Project = project1, Quantity = 200 };
            Supply supply12 = new Supply() { Shipper = shipper3, Part = part4, Project = project2, Quantity = 500 };
            Supply supply13 = new Supply() { Shipper = shipper4, Part = part6, Project = project3, Quantity = 300 };
            Supply supply14 = new Supply() { Shipper = shipper4, Part = part6, Project = project7, Quantity = 300 };
            Supply supply15 = new Supply() { Shipper = shipper5, Part = part2, Project = project2, Quantity = 200 };
            Supply supply16 = new Supply() { Shipper = shipper5, Part = part2, Project = project4, Quantity = 100 };
            Supply supply17 = new Supply() { Shipper = shipper5, Part = part5, Project = project5, Quantity = 500 };
            Supply supply18 = new Supply() { Shipper = shipper5, Part = part5, Project = project7, Quantity = 100 };
            Supply supply19 = new Supply() { Shipper = shipper5, Part = part6, Project = project2, Quantity = 200 };
            Supply supply20 = new Supply() { Shipper = shipper5, Part = part1, Project = project4, Quantity = 100 };
            Supply supply21 = new Supply() { Shipper = shipper5, Part = part3, Project = project4, Quantity = 200 };
            Supply supply22 = new Supply() { Shipper = shipper5, Part = part4, Project = project4, Quantity = 800 };
            Supply supply23 = new Supply() { Shipper = shipper5, Part = part5, Project = project4, Quantity = 400 };
            Supply supply24 = new Supply() { Shipper = shipper5, Part = part6, Project = project4, Quantity = 500 };

            shippers.Add(shipper1);
            shippers.AddRange(shipperArray);

            parts.AddRange(partArray);
            projects.AddRange(new Project[] { project1, project2, project3, project4, project5, project6, project7 });
            supplies.AddRange(new Supply[] { supply1, supply2, supply3, supply4, supply5, supply6, supply7, supply8,
                    supply9, supply10, supply11, supply12, supply13, supply14, supply15, supply16,
                    supply17, supply18, supply19, supply20, supply21, supply22, supply23, supply24 });

            supplyDbContext.SaveChanges();
        }
    }

    [OneTimeTearDown]
    public void GlobalTeardown() {
        using (supplyDbContext = new SupplyDbContext()) {
            string deleteAllTables = @"DELETE FROM [dbo].Shippers; 
                                       DELETE FROM [dbo].Parts; 
                                       DELETE FROM [dbo].Supplies; 
                                       DELETE FROM [dbo].Projects;";
            supplyDbContext.Database.ExecuteSqlCommand(deleteAllTables);
        }
    }
}

[TestFixture]
class SupplyDataTests {
    SupplyStore store;

    [SetUp]
    public void SetUp() {
        store = new SupplyStore();
    }

    [Test]
    public void GetProjects_Test() {
        List<Project> projects = store.GetProjects();

        Assert.AreEqual(7, projects.Count);
    }

    [Test]
    public void GetProjectsLinq_Test() {
        List<Project> projects = store.GetProjectsLinq();

        Assert.AreEqual(7, projects.Count);
    }

    [Test]
    public void GetProjectsInCity_Test() {
        List<Project> projects = store.GetProjectsInCity("London");

        Assert.AreEqual(2, projects.Count);
    }

    [Test]
    public void GetProjectsInCityLinq_Test() {
        List<Project> projects = store.GetProjectsInCityLinq("London");

        Assert.AreEqual(2, projects.Count);
    }

    [Test]
    public void GetShipperIdsForProjectId_Test() {
        Project project = store.GetProjectWithName("Sorter");
        List<int> shipperIds = store.GetShipperIdsForProjectId(project.Id);

        Assert.AreEqual(3, shipperIds.Count);
    }

    [Test]
    public void GetShipperIdsForProjectIdLinq_Test() {
        Project project = store.GetProjectWithName("Sorter");
        List<int> shipperIds = store.GetShipperIdsForProjectIdLinq(project.Id);

        Assert.AreEqual(3, shipperIds.Count);
    }
}