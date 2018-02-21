using SitemapTester.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitemapTester.WebUI.Abstract
{
    public interface IMeasureDomain
    {
        int NumberOfRequests { get; set; }

        List<PageMeasurement> GetDomainMeasurements(HashSet<string> urls);
    }
}
