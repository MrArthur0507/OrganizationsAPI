using Organizations.Models.DTO;
using Organizations.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Repositories
{
    public interface IOrganizationIdAssigner
    {
        public HashSet<Organization> AssignIdsToOrganizations(HashSet<OrganizationDTO> organizations);
    }
}
