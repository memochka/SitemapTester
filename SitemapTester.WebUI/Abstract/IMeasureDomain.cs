using System.Collections.Generic;
using SitemapTester.WebUI.Models;

namespace SitemapTester.WebUI.Abstract
{
    public interface IMeasureDomain
    {
        int NumberOfRequests { get; set; }

        List<PageMeasurement> GetDomainMeasurements(HashSet<string> urls);
    }
}
