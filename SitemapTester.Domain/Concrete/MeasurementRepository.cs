using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SitemapTester.Domain.Abstract;


namespace SitemapTester.Domain.Concrete
{
    public class MeasurementRepository : IMeasurementRepository
    {
        public EfDbContext Context { get; }

        public MeasurementRepository()
        {
            Context = new EfDbContext();
        }
    }
}
