using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Organizations.DbProvider.Repositories.Contracts;
using Organizations.Models.Models;
using Organizations.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Services.Implementations
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMemoryCache _memoryCache;
        public OrganizationService(IOrganizationRepository organizationRepository, IMemoryCache memoryCache)
        {
            _organizationRepository = organizationRepository;
            _memoryCache = memoryCache;
        }
        public string GetPagedOrganizations(int page, int pageSize)
        {
            List<Organization> organizations = CheckInMemory();
            int startIndex = (page - 1) * pageSize;

            if (organizations != null)
            {
                List<Organization> organizationsPaged = organizations.Skip(startIndex).Take(pageSize).ToList();
                return JsonConvert.SerializeObject(organizationsPaged);
            }
            else
            {
                organizations = _organizationRepository.GetAll().ToList();
                _memoryCache.Set("AllOrganizations", organizations, TimeSpan.FromMinutes(5));
                List<Organization> organizationsPaged = organizations.Skip(startIndex).Take(pageSize).ToList();
                return JsonConvert.SerializeObject(organizationsPaged);
            }
        }

        public Organization GetOrganizationById(string organizationId)
        {
            return _organizationRepository.GetById(organizationId);
        }

        public bool Delete(string organizationId)
        {
            return _organizationRepository.DeleteById(organizationId);
        }

        private List<Organization> CheckInMemory()
        {
            if (_memoryCache.TryGetValue("AllOrganizations", out List<Organization> cachedData))
            {
                return cachedData;
            }
            return null;
        }
    }
}
