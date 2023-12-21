import { useEffect, useState } from "react";
import Topic from "../components/shared/Topic";
import { PcConfiguration } from "../utils/models";
import axios from "axios";
import PcConfigurationCard from "../components/PcConfigurations/PcConfigurationCard";

const Configurations = () => {
  const [pcConfigurations, setPcConfigurations] = useState([]);
  useEffect(() => {
    getAllConfigurations();
  }, []);

  function getAllConfigurations() {
    axios
      .get("http://localhost:5198/api/configuration")
      .then((res) => {
        setPcConfigurations(res.data);
      })
      .catch((err) => console.log(err));
  }

  return (
    <>
      <Topic title="Konfiguracje" />
      <div className="flex flex-col justify-center items-center p-4">
        <div className="flex flex-row justify-between mb-4 w-full items-start">
          <button className="btn mb-2 sm:mb-0">Sortuj</button>
          <div className="w-full sm:w-auto flex">
            <input
              type="text"
              placeholder="Szukaj"
              className="input input-bordered w-48 md:w-64 max-w-xs"
            />
            <button className="btn btn-info">Szukaj</button>
          </div>
        </div>

        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4 w-full">
          {pcConfigurations.length !== 0 &&
            pcConfigurations.map((pcConfiguration, index) => (
              <div key={index} className="p-2">
                <PcConfigurationCard pcConfiguration={pcConfiguration} />
              </div>
            ))}
        </div>
      </div>
    </>
  );
};

export default Configurations;
