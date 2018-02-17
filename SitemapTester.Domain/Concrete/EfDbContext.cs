using System.Data.Entity;
using SitemapTester.Domain.Entities;

namespace SitemapTester.Domain.Concrete
{
    public class EfDbContext : DbContext
    {
        public DbSet<DomainMeasurement> DomainMeasurements { get; set; }

        public DbSet<MeasurementArchive> MeasurementArchives { get; set; }
    }
}
