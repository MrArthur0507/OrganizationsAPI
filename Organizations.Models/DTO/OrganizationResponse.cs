using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Models.DTO
{
    public class OrganizationResponse
    {
        public string Name { get; set; }

        public string Website { get; set; }
        public int CountryId { get; set; }
        public string Description { get; set; }

        public int Founded { get; set; }

        public int IndustryId { get; set; }

        public int NumberOfEmployees { get; set; }
    }
}
