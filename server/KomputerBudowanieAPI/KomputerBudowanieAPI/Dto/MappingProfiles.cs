using AutoMapper;
using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Dto
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Case, CaseDto>();
            CreateMap<CaseDto, Case>();
        }
    }
}
