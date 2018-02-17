using System;

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
