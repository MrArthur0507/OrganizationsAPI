using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organizations.Models.Models;
using Organizations.Services.Interfaces;


namespace Organizations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpPost]
        [Route("addCountry")]
        public ActionResult<int> AddCountry([FromBody] Country country)
        {
            int countryId = _countryService.AddCountry(country);
            return Ok(countryId);
        }

        

        [HttpGet]
        [Route("getIdByName")]
        public ActionResult<int> GetCountryIdByName(string name)
        {
            int countryId = _countryService.GetCountryIdByName(name);
            if (countryId != -1)
            {
                return Ok(countryId);
            }
            else
            {
                return NotFound("Country not found");
            }
        }
        
        [HttpGet]
        [Route("getById")]
        public ActionResult<Country> GetCountryById(int id)
        {
            Country country = _countryService.GetCountryById(id.ToString());
            if (country != null)
            {
                return Ok(country);
            }
            else
            {
                return NotFound("Country not found");
            }
        }
    }
}
