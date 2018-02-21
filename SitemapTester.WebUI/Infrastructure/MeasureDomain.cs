using SitemapTester.WebUI.Abstract;
using SitemapTester.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;

namespace SitemapTester.WebUI.Infrastructure
{
    public class MeasureDomain : IMeasureDomain
    {
        private bool _isServerResponseCodeOk;

        public int NumberOfRequests { get; set; }

        public List<PageMeasurement> PagesMeasurements { get; set; }

        public MeasureDomain()
        {
            _isServerResponseCodeOk = false;
            PagesMeasurements = new List<PageMeasurement>();
        }

        /// <summary>
        /// Method measures response time for all urls
        /// </summary>

        public List<PageMeasurement> GetDomainMeasurements(HashSet<string> urls)
        {
            urls.AsParallel().ForAll(x =>
            {
                var responseTime = new List<int>();
                _isServerResponseCodeOk = true;

                for (int i = 0; i < NumberOfRequests; i++)
                {
                    responseTime.Add(GetResponseTime(x));
                }

                if (_isServerResponseCodeOk)
                {
                    PagesMeasurements.Add(new PageMeasurement { PageUrl = x, PageResponseTime = responseTime });
                }
            });

            return PagesMeasurements;
        }

        /// <summary>
        /// Method make request to url, and measures response time
        /// </summary>
        private int GetResponseTime(string url)
        {
            if (GetServerResponseStatusCode(url) != (int)HttpStatusCode.OK)
            {
                _isServerResponseCodeOk = false;
                return 0;
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            Stopwatch timer = new Stopwatch();

            timer.Start();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Close();

            timer.Stop();
            Thread.Sleep(100);
            return timer.Elapsed.Milliseconds;
        }

        /// <summary>
        /// Method returns server response code. 
        /// </summary>
        private int GetServerResponseStatusCode(string currentPage)
        {
            HttpWebResponse response = null;
            int statusCode = 0;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(currentPage);
                response = (HttpWebResponse)request.GetResponse();
                statusCode = (int)response.StatusCode;
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    response = (HttpWebResponse)e.Response;
                    statusCode = (int)response.StatusCode;
                }
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return statusCode;
        }
    }
}