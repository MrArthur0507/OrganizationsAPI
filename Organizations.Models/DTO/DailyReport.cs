using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Models.DTO
{
    public class DailyReport
    {
        public DateTime Date { get; set; }

        public long TotalOrganizations { get; set; }

        public long TotalIndustries { get; set; }

        public long TotalCountries { get; set; }
    }
}
