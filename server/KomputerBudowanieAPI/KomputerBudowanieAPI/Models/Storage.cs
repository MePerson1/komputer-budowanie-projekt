﻿using KomputerBudowanieAPI.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KomputerBudowanieAPI.Models
{
    public class Storage : IPart
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ShopPrice>? Prices { get; set; }
        public string Producer { get; set; }
        public string ProducerCode { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public string FormFactor { get; set; }
        public string Capacity { get; set; }
        public string Interface { get; set; }
        public float? ThiccnessMM { get; set; }
        public string? CacheMemory { get; set; }

        //hdd only
        public float? NoiseLevelDB { get; set; } = null;
        public int? RotatingSpeedRPM { get; set; } = null;
        public float? WeightG { get; set; } = null;

        //ssd only
        public bool? Radiator { get; set; } = null;
        public string? MemoryChipType { get; set; } = null;
        public float? ReadSpeedMBs { get; set; } = null;
        public float? WriteSpeedMBs { get; set; } = null;
        public int? ReadRandomIOPS { get; set; } = null;
        public int? WriteRandomIOPS { get; set; } = null;
        public string? Longevity { get; set; } = null;
        public string? TBW { get; set; } = null;
        public string? Key { get; set; } = null;
        public string? Controler { get; set; } = null;
        public bool? HardwareEncryption { get; set; } = null;

        /*
        *  RELACJE
        */
        [JsonIgnore]
        public ICollection<PcConfigurationStorage>? PcConfigurationStorages { get; set; }
    }
}
