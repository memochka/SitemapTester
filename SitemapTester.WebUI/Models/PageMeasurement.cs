using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitemapTester.WebUI.Models
{
    public class PageMeasurement
    {
        public string PageUrl { get; set; }

        public List<int> PageResponseTime { get; set; }
    }
}