import { PcConfiguration } from "../models";

export const mapServcerModelToClient = (pcConfiguration) => {
  var editedPcConfiguration = PcConfiguration;
  editedPcConfiguration.storages = [];
  editedPcConfiguration.rams = [];
  for (const key in pcConfiguration) {
    if (PcConfiguration.hasOwnProperty(key)) {
      editedPcConfiguration[key] = pcConfiguration[key];
    }

    if (key === "pcConfigurationStorages") {
      pcConfiguration[key].forEach((value) => {
        for (let i = 0; i < value.quantity; i++) {
          if (!editedPcConfiguration["storages"]) {
            editedPcConfiguration.storages = [];
          }
          editedPcConfiguration["storages"][i] = value.storage;
        }
      });
    }

    if (key === "pcConfigurationRams") {
      pcConfiguration[key].forEach((value) => {
        for (let i = 0; i < value.quantity; i++) {
          if (!editedPcConfiguration["rams"]) {
            editedPcConfiguration.rams = [];
          }
          editedPcConfiguration["rams"][i] = value.ram;
        }
      });
    }
  }
  return editedPcConfiguration;
};
