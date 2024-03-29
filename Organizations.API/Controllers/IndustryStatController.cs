﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organizations.Services.Statistics.Contracts;

namespace Organizations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustryStatController : ControllerBase
    {
        private readonly IIndustryStatisticService _industryStatisticService;
        public IndustryStatController(IIndustryStatisticService industryStatisticService) {
            _industryStatisticService = industryStatisticService;
        }
        [Authorize]
        [HttpGet]
        [Route("numberOfEmployeesForIndustries")]
        [ResponseCache(Duration = 60)]
        public IActionResult GetNumberOfEmployees()
        {
            string result = _industryStatisticService.GetNumberOfEmployeesForIndustries();
            if (result != null)
            {
                return Content(result);
            } else
            {
                return NotFound();
            }
        }
    }
}
