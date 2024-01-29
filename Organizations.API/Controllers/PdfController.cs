﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Tls;
using Organizations.Services.Interfaces;

namespace Organizations.API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IReportService _reportService;
        public PdfController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("pdfReport")]
        public IActionResult ReturnStream()
        {
            var file = _reportService.GenerateReport();
            return File(file, "application/pdf", "report.pdf");
        }
    }
}
