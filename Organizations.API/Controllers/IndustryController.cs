using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organizations.Models.Models;
using Organizations.Services.Interfaces;

namespace Organizations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustryController : ControllerBase
    {
        private readonly IIndustryService _industryService;

        public IndustryController(IIndustryService industryService)
        {
            _industryService = industryService;
        }

        [HttpGet]
        [Route("getAll")]
        public ActionResult<string> GetAll()
        {
            return _industryService.GetAll();
        }

        [HttpPost]
        [Route("addIndustry")]
        public ActionResult<int> AddIndustry([FromBody] Industry industry)
        {
            int industryId = _industryService.AddIndustry(industry);
            return Ok(industryId);
        }

        [HttpGet]
        [Route("getIdByName")]
        public ActionResult<int> GetIndustryIdByName(string name)
        {
            int industryId = _industryService.GetIndustryIdByName(name);
            if (industryId != -1)
            {
                return Ok(industryId);
            }
            else
            {
                return NotFound("Industry not found");
            }
        }

        [HttpGet]
        [Route("getById")]
        public ActionResult<Industry> GetIndustryById(int id)
        {
            Industry industry = _industryService.GetIndustryById(id.ToString());
            if (industry != null)
            {
                return Ok(industry);
            }
            else
            {
                return NotFound("Industry not found");
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("deleteIndustry")]
        public ActionResult DeleteIndustry(string id)
        {
            bool success = _industryService.Delete(id);
            if (success)
            {
                return NoContent();
            }
            else
            {
                return NotFound("Industry not found");
            }
        }
    }
}
