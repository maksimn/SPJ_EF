using EF.SupplyData.Domain;
using System.Data.Entity;

namespace EF.SupplyData {
    public class SupplyDbContext : DbContext {
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
    }
}
