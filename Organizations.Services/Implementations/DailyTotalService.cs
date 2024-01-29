using Organizations.DbProvider.Queries.Contracts;
using Organizations.Models.DTO;
using Organizations.Services.Interfaces;
using Organizations.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Services.Implementations
{
    public class DailyTotalService : IDailyTotalService
    {
        private readonly ITotalCount _totalCount;
        private readonly IDateGetter _dateGetter;
        public DailyTotalService(ITotalCount totalCount, IDateGetter dateGetter) { 
            _totalCount = totalCount;
            _dateGetter = dateGetter;
        }

        public DailyReport GetDailyReport()
        {
            return new DailyReport()
            {
                Date = _dateGetter.GetDate(),
                TotalCountries = _totalCount.GetTotalCountriesAddedToday(),
                TotalIndustries = _totalCount.GetTotalIndustriesAddedToday(),
                TotalOrganizations = _totalCount.GetTotalOrganizationsAddedToday(),
            };
        }
    }
}
