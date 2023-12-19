import { useEffect, useState } from "react";
import Topic from "../components/shared/Topic";
import { PcConfiguration } from "../utils/models";
import axios from "axios";
import PcConfigurationCard from "../components/PcConfigurations/PcConfigurationCard";

const Configurations = () => {
  const [pcConfigrations, setPcConfigurations] = useState([]);
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
    <div className="flex flex-col">
      <Topic title="Konfiguracje" />
      <div>
        <div className="flex">
          <div className="w-1/5 bg-base-300 border border-base-100">
            <div>
              <p>Filtry</p>
              <div>Producent: </div>
              <div>Rozmiar: </div>
            </div>
          </div>
          <div>
            <div className="flex">
              <button className="btn">Sortuj</button>
              <div>
                <input
                  type="text"
                  placeholder="Szukaj"
                  class="input input-bordered w-full max-w-xs"
                />
              </div>
            </div>

            <div className="overflow-auto border border-4 border-base-200 flex flex-wrap ">
              {pcConfigrations.length !== 0 &&
                pcConfigrations.map((pcConfigration, index) => (
                  <PcConfigurationCard pcConfigration={pcConfigration} />
                ))}
            </div>
            <div className="flex justify-center content-center  animate-bounce p-5">
              <span class="loading loading-spinner loading-lg"></span>
              <p className="content-cent p-5 text-3xl">Ładowanie</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Configurations;
