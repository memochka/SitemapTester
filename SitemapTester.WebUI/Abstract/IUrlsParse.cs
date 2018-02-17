using System.Collections.Generic;

namespace SitemapTester.WebUI.Abstract
{
    public interface IUrlsParse
    {
        int DepthOfUrlsSearch { get; set; }

        string DomainUrl { get; set; }

        HashSet<string> CollectAllUrls();
    }
}
