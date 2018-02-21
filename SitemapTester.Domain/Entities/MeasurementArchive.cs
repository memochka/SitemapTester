using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitemapTester.Domain.Entities
{
   public class MeasurementArchive
    {
        public int Id { get; set; }

        public Guid MeasurementGuid { get; set; }

        public string DomainName { get; set; }

        public DateTime MeasurementDate { get; set; }
    }
}
