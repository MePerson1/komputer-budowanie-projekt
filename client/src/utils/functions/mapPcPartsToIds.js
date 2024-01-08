import { getUserInfo } from "../apiRequests";
const mapPcPartsToIds = (pcConfiguration) => {
  if (pcConfiguration != null) {
    var pcConfigurationIds;
    pcConfigurationIds = {
      name: pcConfiguration.name ? pcConfiguration.name : "",
      description: pcConfiguration.description,
      motherboadId: pcConfiguration.motherboard
        ? pcConfiguration.motherboard.id
        : 0,
      graphicCardId: pcConfiguration.graphicCard
        ? pcConfiguration.graphicCard.id
        : 0,
      cpuId: pcConfiguration.cpu ? pcConfiguration.cpu.id : 0,
      cpuCoolingId: pcConfiguration.cpuCooling
        ? pcConfiguration.cpuCooling.id
        : 0,
      waterCoolingId: pcConfiguration.waterCooling
        ? pcConfiguration.waterCooling.id
        : 0,
      caseId: pcConfiguration.case ? pcConfiguration.case.id : 0,
      powerSuplyId: pcConfiguration.powerSupply
        ? pcConfiguration.powerSupply.id
        : 0,
      userId: "",
      storageIds: pcConfiguration.storages
        ? pcConfiguration.storages.map((storage) => storage.id)
        : [],
      ramsIds: pcConfiguration.rams
        ? pcConfiguration.rams.map((ram) => ram.id)
        : [],
    };
  } else {
    pcConfigurationIds = {
      name: "",
      description: "",
      motherboadId: 0,
      graphicCardId: 0,
      cpuId: 0,
      cpuCoolingId: 0,
      waterCoolingId: 0,
      caseId: 0,
      powerSuplyId: 0,
      userId: 0,
      storageIds: 0,
      ramsIds: 0,
    };
  }

  return pcConfigurationIds;
};

export default mapPcPartsToIds;
