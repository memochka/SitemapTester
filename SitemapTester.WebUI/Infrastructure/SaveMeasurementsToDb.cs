using SitemapTester.Domain.Concrete;
using SitemapTester.Domain.Entities;
using SitemapTester.WebUI.Abstract;
using SitemapTester.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitemapTester.WebUI.Infrastructure
{
    public class SaveMeasurementsToDb : ISaveMeasurementsToDb
    {
        /// <summary>
        /// Method saves measurements to database
        /// </summary>
        public void SaveMeasurementsToDatabase(DomainMeasurementsDto measurementsDto)
        {
            SaveMeasurementArchive(measurementsDto);
            SaveDomainMeasurement(measurementsDto);
        }

        /// <summary>
        /// Method saves domain name and measurement date 
        ///  </summary>
        private void SaveMeasurementArchive(DomainMeasurementsDto measurementsDto)
        {
            using (var context = new EfDbContext())
            {
                MeasurementArchive archive = new MeasurementArchive
                {
                    DomainName = measurementsDto.DomainName,
                    MeasurementDate = measurementsDto.MeasurementsDate,
                    MeasurementGuid = measurementsDto.MeasurementGuid
                };

                context.MeasurementArchives.Add(archive);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Method saves domain measurements to database
        /// </summary>
        private void SaveDomainMeasurement(DomainMeasurementsDto measurementsDto)
        {
            using (var context = new EfDbContext())
            {
                foreach (var measurement in measurementsDto.PagesMeasurements)
                {
                    if (measurement == null) continue;

                    DomainMeasurement domainMeasurement = new DomainMeasurement
                    {
                        PageUrl = measurement.PageUrl,
                        MinResponseTime = measurement.PageResponseTime.Min(),
                        MaxResponseTime = measurement.PageResponseTime.Max(),
                        MeasurementGuid = measurementsDto.MeasurementGuid
                    };

                    context.DomainMeasurements.Add(domainMeasurement);
                }

                context.SaveChanges();
            }
        }
    }
}