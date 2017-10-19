using EF.SupplyData.Domain;
using System.Data.Entity;

namespace EF.SupplyData {
    class SupplyDbContext : DbContext {
        DbSet<Supply> Supplies { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<Part> Parts { get; set; }
        DbSet<Shipper> Shippers { get; set; }
    }
}
