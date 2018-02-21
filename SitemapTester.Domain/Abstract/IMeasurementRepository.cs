using SitemapTester.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitemapTester.Domain.Abstract
{
    public interface IMeasurementRepository
    {
        EfDbContext Context { get; }

    }
}
