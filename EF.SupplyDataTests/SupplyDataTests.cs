using EF.SupplyData;
using EF.SupplyData.Domain;
using NUnit.Framework;
using System.Data.Entity;

namespace EF.SupplyDataTests {
    [SetUpFixture]
    public class TestsSetupClass {
        SupplyDbContext supplyDbContext = null;

        [OneTimeSetUp]
        public void GlobalSetup() {
            using (supplyDbContext = new SupplyDbContext()) {
                DbSet<Shipper> shippers = supplyDbContext.Shippers;
                Shipper shipper1 = new Shipper() { Name = "Smith", Status = 20, City = "London" };
                Shipper shipper2 = new Shipper() { Name = "Jones", Status = 10, City = "Paris" };
                Shipper shipper3 = new Shipper() { Name = "Blake", Status = 30, City = "Paris" };
                Shipper shipper4 = new Shipper() { Name = "Clarke", Status = 20, City = "London" };
                Shipper shipper5 = new Shipper() { Name = "Adams", Status = 30, City = "Athens" };

                Shipper[] shipperArray = new Shipper[] { shipper2, shipper3, shipper4, shipper5 };

                shippers.Add(shipper1);
                shippers.AddRange(shipperArray);

                supplyDbContext.SaveChanges();
            }
        }

        [OneTimeTearDown]
        public void GlobalTeardown() {
            using (supplyDbContext = new SupplyDbContext()) {
                string deleteAllTablesData = @"DELETE FROM [dbo].Shippers; 
                                               DELETE FROM [dbo].Parts; 
                                               DELETE FROM [dbo].Supplies; 
                                               DELETE FROM [dbo].Projects;";
                supplyDbContext.Database.ExecuteSqlCommand(deleteAllTablesData);
            }
        }
    }

    [TestFixture]
    class SupplyDataTests {
        [Test]
        public void SomeTest() {
            Assert.AreEqual(1, 2);
        }
    }
}
