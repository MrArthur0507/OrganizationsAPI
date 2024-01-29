using Newtonsoft.Json;
using Organizations.DbProvider.Queries.Contracts;
using Organizations.DbProvider.Repositories;
using Organizations.Services.Statistics.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Services.Statistics.Implementations
{
    public class OrganizationStatisticService : IOrganizationStatisticService
    {
        private readonly IOrganizationQuery _organizationQuery;
        public OrganizationStatisticService(IOrganizationQuery organizationQuery)
        {
            _organizationQuery = organizationQuery;
        }
        public string GetOrganizationsByEmployees(int top)
        {
            return JsonConvert.SerializeObject(_organizationQuery.GetTopOrganizationsByEmployees(top));
        }
    }
}
