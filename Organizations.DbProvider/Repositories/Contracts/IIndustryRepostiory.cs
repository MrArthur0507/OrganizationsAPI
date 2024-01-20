using Microsoft.Data.Sqlite;
using Organizations.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Repositories.Contracts
{
    public interface IIndustryRepostiory : IGenericRepository<Industry>
    {
        public int AddIndustry(Industry industry);

        public void AddIndustries(HashSet<Industry> industries);

        public int GetIndustryIdByName(string name);
    }
}
