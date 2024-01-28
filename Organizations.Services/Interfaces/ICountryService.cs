using Organizations.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Services.Interfaces
{
    public interface ICountryService
    {
        public string GetAll();
        public int AddCountry(Country country);

        public int GetCountryIdByName(string name);

        public Country GetCountryById(string countryId);

        public bool UpdateCountry(Country country);
        public bool Delete(string countryId);
    }
}
