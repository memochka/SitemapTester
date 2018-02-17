using System;
using System.Collections.Generic;

namespace SitemapTester.WebUI.Models
{
    public class DomainMeasurementsDto
    {
        public List<PageMeasurement> PagesMeasurements { get; set; }

        public Guid MeasurementGuid { get; set; }

        public string DomainName { get; set; }

        public DateTime MeasurementsDate { get; set; }
    }
}