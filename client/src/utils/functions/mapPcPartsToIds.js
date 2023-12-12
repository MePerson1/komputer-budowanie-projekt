const mapPcPartsToIds = (pcConfiguration) => {
  var pcConfigurationIds = {
    name: pcConfiguration.name,
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
    userId: 0,
    storageIds: pcConfiguration.storages
      ? pcConfiguration.storages.map((storage) => storage.id)
      : [],
    ramsIds: pcConfiguration.rams
      ? pcConfiguration.rams.map((ram) => ram.id)
      : [],
    fanIds: [],
  };
  return pcConfigurationIds;
};

export default mapPcPartsToIds;
