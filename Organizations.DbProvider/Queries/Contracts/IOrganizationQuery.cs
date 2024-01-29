using Organizations.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Queries.Contracts
{
    public interface IOrganizationQuery
    {
        public List<Organization> GetTopOrganizationsByEmployees(int topCount);
    }
}
