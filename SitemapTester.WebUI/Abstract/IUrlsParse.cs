using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitemapTester.WebUI.Abstract
{
    public interface IUrlsParse
    {
        int DepthOfUrlsSearch { get; set; }

        string DomainUrl { get; set; }

        HashSet<string> CollectAllUrls();
    }
}
