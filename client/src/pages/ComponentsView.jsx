import PartsTable from "../components/shared/PartsTable";
import { useState, useEffect } from "react";
import axios from "axios";
import ReturnButton from "../components/shared/ReturnButton";
const ComponentsView = ({ partType, pcConfiguration, setPcConfiguration }) => {
  const [parts, setParts] = useState(null);
  const [filter, setFilter] = useState(true);

  useEffect(() => {
    if (filter) {
      getFilteredParts(partType);
    } else {
      getParts(partType);
    }
  }, [partType, filter]);

  async function getParts(partType) {
    await axios
      .get(`http://localhost:5198/api/${partType}`)
      .then((res) => {
        setParts(res.data);
      })
      .catch((err) => console.log(err));
  }

  async function getFilteredParts(partType) {
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

    await axios
      .post(
        `http://localhost:5198/api/${partType}/compatible`,
        pcConfigurationIds
      )
      .then((res) => {
        setParts(res.data);
      })
      .catch((err) => console.log(err));
  }

  return (
    <div>
      <ReturnButton />
      <div className="form-control ">
        <label className="label cursor-pointer">
          <span className="label-text">Tylko pasujące częsci</span>
          <input
            type="checkbox"
            defaultChecked={filter}
            onChange={() => setFilter((state) => !state)}
            className="checkbox"
          />
        </label>
      </div>

      <PartsTable
        parts={parts}
        partType={partType}
        setPcConfiguration={setPcConfiguration}
        pcConfiguration={pcConfiguration}
      />
    </div>
  );
};

export default ComponentsView;
