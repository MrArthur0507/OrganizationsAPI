using Organizations.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Queries.Contracts
{
    public interface IIndustryQuery
    {
        public List<IndustryEmployee> GetTotalEmployeesForIndustry();
    }
}
