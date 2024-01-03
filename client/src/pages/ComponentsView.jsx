import PartsTable from "../components/shared/PartsTable";
import { useState, useEffect } from "react";
import axios from "axios";
import ReturnButton from "../components/shared/ReturnButton";
import mapPcPartsToIds from "../utils/functions/mapPcPartsToIds";
import Topic from "../components/shared/Topic";
import Breadcrumbs from "../components/shared/Breadcrumbs";
const ComponentsView = ({ partType, pcConfiguration, setPcConfiguration }) => {
  const [parts, setParts] = useState(null);
  const [filter, setFilter] = useState(true);

  useEffect(() => {
    if (filter) {
      getFilteredParts(partType.key);
    } else {
      getParts(partType.key);
    }
  }, [partType, filter]);

  async function getParts(partKey) {
    await axios
      .get(`http://localhost:5198/api/${partKey}`)
      .then((res) => {
        setParts(res.data);
      })
      .catch((err) => console.log(err));
  }

  async function getFilteredParts(partKey) {
    var pcConfigurationIds = mapPcPartsToIds(pcConfiguration);

    await axios
      .post(
        `http://localhost:5198/api/${partKey}/compatible`,
        pcConfigurationIds
      )
      .then((res) => {
        setParts(res.data);
      })
      .catch((err) => console.log(err));
  }

  return (
    <div>
      <Topic title={partType.namePL} />

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
        partType={partType.key}
        setPcConfiguration={setPcConfiguration}
        pcConfiguration={pcConfiguration}
      />
    </div>
  );
};

export default ComponentsView;
