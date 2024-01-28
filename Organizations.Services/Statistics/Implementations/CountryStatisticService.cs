using Newtonsoft.Json;
using Organizations.DbProvider.Queries.Contracts;
using Organizations.Models.DTO;
using Organizations.Services.Statistics.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Services.Statistics.Implementations
{
    public class CountryStatisticService : ICountryStatisticService
    {
        private readonly ICountryQuery _countryQuery;
        
        public CountryStatisticService(ICountryQuery countryQuery)
        {
            _countryQuery = countryQuery;
        }
        public string GetCountryOrganizations()
        {
            return JsonConvert.SerializeObject(_countryQuery.GetOrganizationsForCountries());
        }
    }
}
