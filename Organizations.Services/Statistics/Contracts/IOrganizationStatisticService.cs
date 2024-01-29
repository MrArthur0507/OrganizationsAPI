using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Services.Statistics.Contracts
{
    public interface IOrganizationStatisticService
    {
        public string GetOrganizationsByEmployees(int top);
    }
}
