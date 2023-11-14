import Case from "./Case";
import Cpu from "./Cpu";
import CpuCooling from "./CpuCooling";
import GraphicCard from "./GraphicCard";
import Motherboard from "./Motherboard";
import PowerSupply from "./PowerSupply";
import Ram from "./Ram";
import Storage from "./Storage";
import WaterCooling from "./WaterCooling";

const PcConfiguration = {
  case: Case,
  cpu: Cpu,
  cpuCooling: CpuCooling,
  graphicCard: GraphicCard,
  motherboard: Motherboard,
  powerSupply: PowerSupply,
  ram: Ram,
  storage: Storage,
  waterCooling: WaterCooling,
};

export default PcConfiguration;
