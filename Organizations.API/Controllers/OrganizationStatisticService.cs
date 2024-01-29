using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organizations.Services.Statistics.Contracts;

namespace Organizations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationStatisticService : ControllerBase
    {
        private readonly IOrganizationStatisticService _organizationStatisticService;
        public OrganizationStatisticService(IOrganizationStatisticService organizationStatisticService)
        {
            _organizationStatisticService = organizationStatisticService;
        }
        [Authorize]
        [HttpGet]
        [Route("getOrganizationsByNumberOfEmployees")]
        public IActionResult GetTopOrganizations(int top)
        {
            return Content(_organizationStatisticService.GetOrganizationsByEmployees(top));
        }

    }
}
