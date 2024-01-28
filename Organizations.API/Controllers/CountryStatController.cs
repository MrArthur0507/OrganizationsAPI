using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organizations.Services.Statistics.Contracts;

namespace Organizations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryStatController : ControllerBase
    {
        private readonly ICountryStatisticService _countryStatisticService;
        public CountryStatController(ICountryStatisticService countryStatisticService) { 
            _countryStatisticService = countryStatisticService;
        }
        [HttpGet]
        [Route("getOrganizationsForCountries")]
        public IActionResult GetOrganizationsForCountries()
        {
            string result = _countryStatisticService.GetCountryOrganizations();
            return Ok(result);
        }
    }
}
