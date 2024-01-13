using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Models.Models
{
    public class Organization
    {
        [Name("Index")]
        public string Index { get; set; }

        [Name("Organization Id")]
        public string OrganizationId { get; set; }

        [Name("Name")]
        public string Name { get; set; }

        [Name("Website")]
        public string Website { get; set; }

        [Name("Country")]
        public string Country { get; set; }

        [Name("Description")]
        public string Description { get; set; }

        [Name("Founded")]
        public int Founded { get; set; }

        [Name("Industry")]
        public string Industry { get; set; }

        [Name("Number of employees")]
        public int NumberOfEmployees { get; set; }
    }
}
