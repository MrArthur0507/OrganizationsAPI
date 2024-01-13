using Organizations.DbProvider.Repositories.Contracts;
using Organizations.Models.Models;
using Organizations.ReaderApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.ReaderApp.Services.Implementations
{
    public class DbSeeder : IDbSeeder
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IIndustryRepostiory _industryRepostiory;
        private readonly IOrganizationRepository _organizationRepository;

        public DbSeeder(ICountryRepository countryRepository, IIndustryRepostiory industryRepostiory, IOrganizationRepository organizationRepository)
        {
            _countryRepository = countryRepository;
            _industryRepostiory = industryRepostiory;
            _organizationRepository = organizationRepository;
        }

        public void Seed(IList<Organization> organizations)
        {
            HashSet<string> countries = organizations.Select(x => x.Country).Distinct().ToHashSet();
            HashSet<string> industries = organizations.Select(x => x.Industry).Distinct().ToHashSet();
            HashSet<Country> countryList = new HashSet<Country>();
            HashSet<Industry> industryList = new HashSet<Industry>();
            foreach (var item in countries)
            {
                countryList.Add(new Country() { Name = item });
            }
            foreach (var item in industries)
            {
                industryList.Add(new Industry() { Name = item });
            }
            _countryRepository.AddCountries(countryList);
            _industryRepostiory.AddIndustries(industryList);
            _organizationRepository.AddOrganizations(organizations.ToHashSet());
        }
    }
}
