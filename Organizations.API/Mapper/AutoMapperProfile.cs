using AutoMapper;
using Organizations.Models.DTO;
using Organizations.Models.Models;

namespace Organizations.API.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<Industry, IndustryDTO>().ReverseMap();
            CreateMap<Organization, OrganizationResponse>().ReverseMap();
        }
    }
}
