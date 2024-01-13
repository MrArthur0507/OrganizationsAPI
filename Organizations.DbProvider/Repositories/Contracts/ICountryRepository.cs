using Microsoft.Data.Sqlite;
using Organizations.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Repositories.Contracts
{
    public interface ICountryRepository
    {
        public int GetCountryIdByName(string name);

        public int AddCountry(Country country);

        public void AddCountries(HashSet<Country> countries);
    }
}
