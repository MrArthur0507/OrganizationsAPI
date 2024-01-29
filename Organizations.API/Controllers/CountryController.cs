using Microsoft.AspNetCore.Authorization;
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


        [HttpGet]
        [Route("getAll")]
        [ResponseCache(Duration = 60)]
        public ActionResult<string> GetAll()
        {
            return _countryService.GetAll();
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

        [HttpPut]
        [Route("updateCountry")]
        public IActionResult UpdateCountry([FromQuery]Country country)
        {
            bool result = _countryService.UpdateCountry(country);
            if (result)
            {
                return Ok();
            } else
            {
                return BadRequest();
            }
        }

        [Authorize("AdminPolicy")]
        [HttpDelete]
        [Route("deleteCountry")]
        public ActionResult DeleteCountry(string id)
        {
            bool success = _countryService.Delete(id);
            if (success)
            {
                return NoContent(); 
            }
            else
            {
                return NotFound("Country not found");
            }
        }
    }
}
