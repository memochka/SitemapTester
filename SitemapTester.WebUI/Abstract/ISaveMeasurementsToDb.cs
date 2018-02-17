using SitemapTester.WebUI.Models;

namespace SitemapTester.WebUI.Abstract
{
    public interface ISaveMeasurementsToDb
    {
        void SaveMeasurementsToDatabase(DomainMeasurementsDto measurementsDto);
    }
}