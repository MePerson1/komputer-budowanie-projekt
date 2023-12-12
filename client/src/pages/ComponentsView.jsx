import PartsTable from "../components/shared/PartsTable";
import { useState, useEffect } from "react";
import axios from "axios";
import ReturnButton from "../components/shared/ReturnButton";
import mapPcPartsToIds from "../utils/functions/mapPcPartsToIds";
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
    var pcConfigurationIds = mapPcPartsToIds(pcConfiguration);

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
      <div className="flex ml-1 mr-2">
        <div className=" form-control ">
          <label className="pl-2 label bg-gray-900 rounded-lg border border-secondary">
            <span className="label-text">Tylko pasujące częsci</span>
            <input
              type="checkbox"
              defaultChecked={filter}
              onChange={() => setFilter((state) => !state)}
              className="checkbox m-5"
            />
          </label>
        </div>
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
