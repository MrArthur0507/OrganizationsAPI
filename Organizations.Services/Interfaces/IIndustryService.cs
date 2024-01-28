using Organizations.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Services.Interfaces
{
    public interface IIndustryService
    {
        public string GetAll();
        public int AddIndustry(Industry industry);
        public int GetIndustryIdByName(string name);
        public Industry GetIndustryById(string industryId);

        public bool UpdateIndustry(Industry industry);
        public bool Delete(string industryId);
    }
}
