using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Helpers
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

            CreateMap<WaterCooling, WaterCoolingDto>();
            CreateMap<WaterCoolingDto, WaterCooling>();

            CreateMap<GraphicCard, GraphicCardDto>();
            CreateMap<GraphicCardDto, GraphicCard>();

            CreateMap<Storage, StorageDto>();
            CreateMap<StorageDto, Storage>();

            CreateMap<Motherboard, MotherboardDto>();
            CreateMap<MotherboardDto, Motherboard>();

            CreateMap<PowerSupply, PowerSupplyDto>();
            CreateMap<PowerSupplyDto, PowerSupply>();

            CreateMap<Ram, RamDto>();
            CreateMap<RamDto, Ram>();

            CreateMap<Ram, ProductDto>();
            CreateMap<Case, ProductDto>();
            CreateMap<CpuCooling, ProductDto>();
            CreateMap<Cpu, ProductDto>();
            CreateMap<WaterCooling, ProductDto>();
            CreateMap<GraphicCard, ProductDto>();
            CreateMap<Storage, ProductDto>();
            CreateMap<Motherboard, ProductDto>();
            CreateMap<PowerSupply, ProductDto>();

            CreateMap<ApplicationUser, UserDto>();
            CreateMap<PcConfiguration, PcConfigurationViewModel>();

        }
    }
}
