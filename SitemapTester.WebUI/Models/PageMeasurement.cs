using System.Collections.Generic;

namespace SitemapTester.WebUI.Models
{
    public class PageMeasurement
    {
        public string PageUrl { get; set; }

        public List<int> PageResponseTime { get; set; } 
    }
}