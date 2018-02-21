using SitemapTester.Domain.Abstract;
using SitemapTester.Domain.Entities;
using SitemapTester.WebUI.Abstract;
using SitemapTester.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitemapTester.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IUrlsParse _urlParse;
        private IMeasureDomain _measureDomain;
        private ISaveMeasurementsToDb _saveToDb;
        private IMeasurementRepository _repository;
        private DomainMeasurementsDto _measurementsDto;

        public HomeController(IUrlsParse urlParse, IMeasureDomain measureDomain,
            ISaveMeasurementsToDb saveToDb, IMeasurementRepository repository)
        {
            _urlParse = urlParse;
            _measureDomain = measureDomain;
            _saveToDb = saveToDb;
            _repository = repository;
            _measurementsDto = new DomainMeasurementsDto();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetDomainMeasurements(string url)
        {
            SettingMeasurementConfiguration();

            _urlParse.DomainUrl = url;

            HashSet<string> allUrl = _urlParse.CollectAllUrls();
            List<PageMeasurement> pagesMeasurements = _measureDomain.GetDomainMeasurements(allUrl);

            _measurementsDto.DomainName = url;
            _measurementsDto.MeasurementGuid = Guid.NewGuid();
            _measurementsDto.MeasurementsDate = DateTime.Now;
            _measurementsDto.PagesMeasurements = pagesMeasurements;

            _saveToDb.SaveMeasurementsToDatabase(_measurementsDto);

            return RedirectToAction("Test", _measurementsDto);
        }

        private void SettingMeasurementConfiguration()
        {
            _urlParse.DepthOfUrlsSearch = 1;
            _measureDomain.NumberOfRequests = 2;
        }

        public ActionResult History()
        {
            var archive = _repository.Context.MeasurementArchives
                .Select(x => x).OrderBy(x => x.DomainName);

            return View(archive);
        }

        public ViewResult Test(Guid measurementGuid)
        {
            return View(_repository.Context.DomainMeasurements
                .Where(x => x.MeasurementGuid == measurementGuid)
                .Select(x => x)
                .OrderByDescending(x => x.MaxResponseTime));
        }

        public ActionResult GetChart(DomainMeasurement measurement)
        {
            return Json(_repository.Context.DomainMeasurements
                .Where(x => x.MeasurementGuid == measurement.MeasurementGuid)
                .Select(x => new { x.PageUrl, x.MinResponseTime }),
                JsonRequestBehavior.AllowGet);
        }
    }
}