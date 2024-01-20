using AutoMapper;
using Newtonsoft.Json;
using Organizations.DbProvider.Repositories.Contracts;
using Organizations.Models.DTO;
using Organizations.Models.Models;
using Organizations.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Services.Implementations
{
    public class IndustryService : IIndustryService
    {
        private readonly IIndustryRepostiory _industryRepository;
        private readonly IMapper _mapper;
        public IndustryService(IIndustryRepostiory industryRepository, IMapper mapper)
        {
            _industryRepository = industryRepository;
            _mapper = mapper;
        }

        public string GetAll()
        {
            List<IndustryDTO> industryDtos = _mapper.Map<List<IndustryDTO>>(_industryRepository.GetAll());
            return JsonConvert.SerializeObject(industryDtos);
        }

        public int AddIndustry(Industry industry)
        {
            int existingIndustryId = _industryRepository.GetIndustryIdByName(industry.Name);

            if (existingIndustryId != -1)
            {
                return existingIndustryId;
            }
            else
            {
                return _industryRepository.AddIndustry(industry);
            }
        }

        public void AddIndustries(HashSet<Industry> industries)
        {
            foreach (var industry in industries)
            {
                int existingIndustryId = _industryRepository.GetIndustryIdByName(industry.Name);

                if (existingIndustryId == -1)
                {
                    _industryRepository.AddIndustry(industry);
                }
            }
        }

        public int GetIndustryIdByName(string name)
        {
            return _industryRepository.GetIndustryIdByName(name);
        }

        public Industry GetIndustryById(string industryId)
        {
            return _industryRepository.GetById(industryId);
        }

        public bool Delete(string industryId)
        {
            Industry industry = GetIndustryById(industryId);
            if (industry != null)
            {
                return _industryRepository.DeleteById(industryId);
            }
            return false;
        }
    }
}
