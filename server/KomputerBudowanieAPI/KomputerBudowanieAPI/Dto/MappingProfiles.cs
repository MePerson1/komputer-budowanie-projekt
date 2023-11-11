﻿using AutoMapper;
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

        }
    }
}
