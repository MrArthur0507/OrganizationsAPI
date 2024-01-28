using Organizations.Models.DTO;
using Organizations.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Repositories.Contracts
{
    public interface IOrganizationRepository : IGenericRepository<Organization>
    {
        public void AddOrganizations(HashSet<OrganizationDTO> organizations);
    }
}
