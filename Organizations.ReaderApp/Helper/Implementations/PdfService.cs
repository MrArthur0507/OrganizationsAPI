using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Organizations.DbProvider.Queries.Contracts;
using Organizations.DbProvider.Queries.Implementations;
using Organizations.Models.DTO;
using Organizations.ReaderApp.Helper.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.ReaderApp.Helper.Implementations
{
    public class PdfService : IPdfService
    {
        private readonly ICountryQuery _countryQuery;
        public PdfService(ICountryQuery countryQuery) {
            _countryQuery = countryQuery;
        }
        public void GenerateAndSavePdf(string filePath)
        {
            List<CountryOrganization> countryOrganizations = _countryQuery.GetOrganizationsForCountries();
            using (PdfWriter writer = new PdfWriter(filePath))
            {
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    Document document = new Document(pdf);
                    
                    document.Add(new Paragraph("Hello, this is a sample PDF document."));
                    document.Close();
                }
            }
        }
    }
}
