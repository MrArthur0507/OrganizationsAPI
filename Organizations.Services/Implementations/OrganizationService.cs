using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Organizations.DbProvider.Repositories.Contracts;
using Organizations.Models.DTO;
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
        private readonly IMapper _mapper;
        public OrganizationService(IOrganizationRepository organizationRepository, IMemoryCache memoryCache, IMapper mapper)
        {
            _organizationRepository = organizationRepository;
            _memoryCache = memoryCache;
            _mapper = mapper;
        }
        public string GetPagedOrganizations(int page, int pageSize)
        {
            List<Organization> organizations = CheckInMemory();
            int startIndex = (page - 1) * pageSize;

            if (organizations != null)
            {
                List<Organization> organizationsPaged = organizations.Skip(startIndex).Take(pageSize).ToList();
                return JsonConvert.SerializeObject(_mapper.Map<List<OrganizationResponse>>(organizationsPaged));
            }
            else
            {
                organizations = _organizationRepository.GetAll().ToList();
                _memoryCache.Set("AllOrganizations", organizations, TimeSpan.FromMinutes(5));
                List<Organization> organizationsPaged = organizations.Skip(startIndex).Take(pageSize).ToList();
                return JsonConvert.SerializeObject(_mapper.Map<List<OrganizationResponse>>(organizationsPaged));
            }
        }

        public OrganizationResponse GetOrganizationById(string organizationId)
        {
            return _mapper.Map<OrganizationResponse>(_organizationRepository.GetById(organizationId));
            
        }

        public bool UpdateOrganization(Organization organization)
        {
            return _organizationRepository.UpdateOrganization(organization);
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
