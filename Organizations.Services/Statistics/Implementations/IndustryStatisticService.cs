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
    public class IndustryStatisticService : IIndustryStatisticService
    {
        private readonly IIndustryQuery _industryQuery;

        public IndustryStatisticService(IIndustryQuery industryQuery)
        {
            _industryQuery = industryQuery;
        }
        public string GetNumberOfEmployeesForIndustries()
        {
            List<IndustryEmployee> industryEmployees = _industryQuery.GetTotalEmployeesForIndustry();
            return JsonConvert.SerializeObject(industryEmployees);
        }
    }
}
