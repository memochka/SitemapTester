using SitemapTester.Domain.Abstract;

namespace SitemapTester.Domain.Concrete
{
    public class MeasurementRepository :IMeasurementRepository
    {
        public EfDbContext Context { get; }

        public MeasurementRepository()
        {
            Context = new EfDbContext();
        }
    }
}
