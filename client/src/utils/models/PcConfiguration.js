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
  case: null,
  cpu: null,
  cpuCooling: null,
  description: "",
  fans: [],
  graphicCard: null,
  id: "",
  motherboard: null,
  name: "",
  powerSupply: null,
  rams: [],
  storages: [],
  user: null,
  waterCooling: null,
};

export default PcConfiguration;
