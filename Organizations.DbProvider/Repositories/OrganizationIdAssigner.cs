using Organizations.DbProvider.Repositories.Contracts;
using Organizations.Models.DTO;
using Organizations.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Repositories
{
    public class OrganizationIdAssigner : IOrganizationIdAssigner
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IIndustryRepostiory _industryRepository;
        public OrganizationIdAssigner(ICountryRepository countryRepository, IIndustryRepostiory industryRepository)
        {
            _countryRepository = countryRepository;
            _industryRepository = industryRepository;
        }

        public HashSet<Organization> AssignIdsToOrganizations(HashSet<OrganizationDTO> organizations)
        {
            HashSet<Organization> organizationsWithIds = new HashSet<Organization>();
            HashSet<Industry> industries = _industryRepository.GetAll().ToHashSet();
            HashSet<Country> countries = _countryRepository.GetAll().ToHashSet();

            foreach (OrganizationDTO organization in organizations)
            {
                Country country = countries.FirstOrDefault(c => c.Name == organization.Country);
                Industry industry = industries.FirstOrDefault(c => c.Name == organization.Industry);

                int countryId = country.CountryId;
                int industryId = industry.IndustryId;

                organizationsWithIds.Add(new Organization
                {
                    Index = organization.Index,
                    Name = organization.Name,
                    Website = organization.Website,
                    CountryId = countryId,
                    Description = organization.Description,
                    Founded = organization.Founded,
                    IndustryId = industryId,
                    NumberOfEmployees = organization.NumberOfEmployees
                });
            }

            return organizationsWithIds;
        }
    }
}
