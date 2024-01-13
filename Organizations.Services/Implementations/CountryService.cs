using Organizations.DbProvider.Repositories.Contracts;
using Organizations.DbProvider.Repositories.Implementations;
using Organizations.Models.Models;
using Organizations.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Services.Implementations
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public int AddCountry(Country country)
        {
            int existingCountryId = _countryRepository.GetCountryIdByName(country.Name);

            if (existingCountryId != -1)
            {
                return existingCountryId;
            }
            else
            {
                return _countryRepository.AddCountry(country);
            }
        }

        public void AddCountries(HashSet<Country> countries)
        {
            foreach (var country in countries)
            {
                int existingCountryId = _countryRepository.GetCountryIdByName(country.Name);

                if (existingCountryId == -1)
                {
                    _countryRepository.AddCountry(country);
                }
            }
        }

        public int GetCountryIdByName(string name)
        {
            return _countryRepository.GetCountryIdByName(name);
        }

        public Country GetCountryById(string countryId)
        {
            return _countryRepository.GetById(countryId);
        }
    }
}
