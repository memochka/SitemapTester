using SitemapTester.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitemapTester.Domain.Concrete
{
    public class EfDbContext : DbContext
    {
        public DbSet<DomainMeasurement> DomainMeasurements { get; set; }

        public DbSet<MeasurementArchive> MeasurementArchives { get; set; }
    }
}
