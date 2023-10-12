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

            CreateMap<CpuCooling, CpuCoolingDto>();
            CreateMap<CpuCoolingDto, CpuCooling>();

            CreateMap<Cpu, CpuDto>();
            CreateMap<CpuDto, Cpu>();

            CreateMap<Fan, FanDto>();
            CreateMap<FanDto, Fan>();

            CreateMap<GraphicCard, GraphicCardDto>();
            CreateMap<GraphicCardDto, GraphicCard>();

            CreateMap<Memory, MemoryDto>();
            CreateMap<MemoryDto, Memory>();

            CreateMap<Motherboard, MotherboardDto>();
            CreateMap<MotherboardDto, Motherboard>();

            CreateMap<PowerSupply, PowerSupplyDto>();
            CreateMap<PowerSupplyDto, PowerSupply>();

            CreateMap<Ram, RamDto>();
            CreateMap<RamDto, Ram>();

        }
    }
}
