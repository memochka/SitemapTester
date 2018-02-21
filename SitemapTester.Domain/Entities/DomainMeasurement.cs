using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitemapTester.Domain.Entities
{
    public class DomainMeasurement
    {
        public int Id { get; set; }

        public Guid MeasurementGuid { get; set; }

        public string PageUrl { get; set; }

        public int MinResponseTime { get; set; }

        public int MaxResponseTime { get; set; }
    }
}
