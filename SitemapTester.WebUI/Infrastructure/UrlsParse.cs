using System.Collections.Generic;
using HtmlAgilityPack;
using SitemapTester.WebUI.Abstract;

namespace SitemapTester.WebUI.Infrastructure
{
    public class UrlsParse : IUrlsParse
    {
        public HashSet<string> AllUrls { get; set; }

        public HashSet<string> AllUrlsOnCurrentLayer { get; set; }

        public HashSet<string> AllUrlsOnCurrentPage { get; set; }

        public int DepthOfUrlsSearch { get; set; } 

        public string DomainUrl { get; set; }
        
        public UrlsParse()
        {
            AllUrls = new HashSet<string>();
            AllUrlsOnCurrentLayer = new HashSet<string>();
            AllUrlsOnCurrentPage = new HashSet<string>();
        }

        /// <summary>
        /// Method gets all urls from requested domain
        /// </summary>
        public HashSet<string> CollectAllUrls()
        {
            TrimUlrNameIfNeeded(DomainUrl);

            for (int i = 0; i < DepthOfUrlsSearch; i++)
            {
                ParseUrlsFromCurrentLayer();
                SaveCurrentPageUrlsToLayerUrlCollection();
                SaveCurrentLayerToAllUrlsCollection();
            }

            AllUrlsOnCurrentPage.Clear();
            AllUrlsOnCurrentLayer.Clear();     
            return AllUrls;
        }

        /// <summary>
        /// Method deletes redundant char '/' from end of url
        /// </summary>
        private void TrimUlrNameIfNeeded(string domainUrl)
        {
            if (domainUrl.EndsWith("/"))
            {
                DomainUrl = domainUrl.TrimEnd('/');
            }
        }

        /// <summary>
        /// Method takes all links from embedded layer
        /// </summary>
        private void ParseUrlsFromCurrentLayer()
        {
            AllUrlsOnCurrentPage.Clear();

            if (AllUrls.Count == 0)
            {
                AllUrlsOnCurrentLayer.Add(DomainUrl);
            }

            foreach (var url in AllUrlsOnCurrentLayer)
            {
                if (url.StartsWith("/"))
                {
                    GetCurrentPageUrls(DomainUrl + url);
                }
                else
                {
                    GetCurrentPageUrls(url);
                }
            }

            AllUrlsOnCurrentLayer.Clear();
        }

        /// <summary>
        /// Method parse all unique urls from current page
        /// </summary>
        private void GetCurrentPageUrls(string currentPage)
        {
            var web = new HtmlWeb();
            HtmlDocument doc = web.Load(currentPage);

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                string hrefValue = node.GetAttributeValue("href", string.Empty);
                
                if (hrefValue.StartsWith("/") || hrefValue.StartsWith(DomainUrl) && !hrefValue.StartsWith("//"))
                {
                    if (hrefValue.StartsWith("/"))
                    {
                        AllUrlsOnCurrentPage.Add(DomainUrl + hrefValue);
                    }
                    else
                    {
                        AllUrlsOnCurrentPage.Add(hrefValue);
                    }
                }
            }
        }

        /// <summary>
        /// Method save all urls from current page to AllUrlsOnCurrentLayer
        /// </summary>
        private void SaveCurrentPageUrlsToLayerUrlCollection()
        {
            AllUrlsOnCurrentLayer.UnionWith(AllUrlsOnCurrentPage);
        }

        /// <summary>
        /// Method save all urls from current layer to AllUrls
        /// </summary>
        private void SaveCurrentLayerToAllUrlsCollection()
        {
            foreach (var url in AllUrlsOnCurrentLayer)
            {
                AllUrls.Add(url);
            }
        }
    }
}