using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organizations.Models.DTO;
using Organizations.Models.Models;
using Organizations.Services.Interfaces;
using System.Net;

namespace Organizations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet]
        [Route("getByPage")]
        public IActionResult GetByPage(int page, int pageSize)
        {
            return Content(_organizationService.GetPagedOrganizations(page, pageSize));
        }

        [HttpGet]
        [Route("getById")]
        public IActionResult GetById(string id)
        {
            OrganizationResponse org = _organizationService.GetOrganizationById(id);
            if (org != null)
            {
                return Ok(org);
            }
            else
            {
                return NotFound();
            }

        }
        [Authorize("AdminPolicy")]
        [HttpPut]
        [Route("updateOrganization")]
        public IActionResult UpdateOrganization([FromQuery]Organization organization)
        {
            bool isSuccess = _organizationService.UpdateOrganization(organization);
            if (isSuccess)
            {
                return Ok();
            } else
            {
                return NotFound();
            }
        }
        [Authorize("AdminPolicy")]
        [HttpDelete]
        [Route("deleteOrganization")]
        public IActionResult DeleteOrganization(string id)
        {
            bool isSuccess = _organizationService.Delete(id);
            if (isSuccess)
            {
                return Ok();
            } else
            {
                return BadRequest();
            }
        }
    }
}
