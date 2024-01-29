using Organizations.DbProvider.Queries.Contracts;
using Organizations.Models.DTO;
using Organizations.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IPdfService _pdfService;
        private readonly ICountryQuery _countryQuery;
        public ReportService(ICountryQuery countryQuery, IPdfService pdfService) { 
            _pdfService = pdfService;
            _countryQuery = countryQuery;
        }
        public byte[] GenerateReport()
        {
            List<CountryOrganization> countryOrganizations = _countryQuery.GetOrganizationsForCountries();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in countryOrganizations)
            {
                stringBuilder.AppendLine($"Country: {item.CountryName} : Number of orgranizations {item.OrganizationCount}");

            }
            _pdfService.GenerateAndSavePdf("report.pdf", stringBuilder.ToString());
            return File.ReadAllBytes("report.pdf");
        }
    }
}
