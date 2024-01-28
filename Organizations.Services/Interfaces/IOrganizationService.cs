using Organizations.Models.DTO;
using Organizations.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Services.Interfaces
{
    public interface IOrganizationService
    {
        public string GetPagedOrganizations(int page, int pageSize);

        public OrganizationResponse GetOrganizationById(string Organizationid);

        public bool UpdateOrganization(Organization organization);
        public bool Delete(string OrganizationId);
    }
}
