using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Queries.Contracts
{
    public interface ITotalCount
    {
        public long GetTotalIndustriesAddedToday();

        public long GetTotalCountriesAddedToday();

        public long GetTotalOrganizationsAddedToday();
    }
}
