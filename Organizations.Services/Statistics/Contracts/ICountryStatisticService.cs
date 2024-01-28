using Organizations.DbProvider.Queries.Contracts;
using Organizations.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Services.Statistics.Contracts
{
    public interface ICountryStatisticService
    {
        public string GetCountryOrganizations();
    }
}
