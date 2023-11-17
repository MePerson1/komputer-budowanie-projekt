import {
  Case,
  Cpu,
  CpuCooling,
  GraphicCard,
  Motherboard,
  PowerSupply,
  Ram,
  Storage,
  WaterCooling,
} from "./index";

const PcConfiguration = {
  case: Case,
  cpu: Cpu,
  cpuCooling: CpuCooling,
  graphicCard: GraphicCard,
  motherboard: Motherboard,
  powerSupply: PowerSupply,
  rams: [Ram],
  storages: [Storage],
  waterCooling: WaterCooling,
};

export default PcConfiguration;
