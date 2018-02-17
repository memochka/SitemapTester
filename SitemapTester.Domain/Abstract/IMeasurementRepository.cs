using SitemapTester.Domain.Concrete;

namespace SitemapTester.Domain.Abstract
{
    public interface IMeasurementRepository
    {
        EfDbContext Context { get; }
    }
}
