import ComponentsTable from "../components/Build/ComponentsTable";
import Info from "../components/Build/Info";
import { useState, useEffect } from "react";
import axios from "axios";
import { PcConfiguration, Motherboard } from "../utils/models/index";
import ConfigurationInfo from "../components/Build/ConfigurationInfo";
import pcParts from "../utils/constants/pcParts";
const Build = ({ pcConfiguration, setPcConfiguration, configurationInfo }) => {
  const [totalPrice, setTotalPrice] = useState(0.0);
  const [inputName, setInputName] = useState("");
  const [isSaved, setIsSaved] = useState(false);

  //useEffect for loading name
  useEffect(() => {
    const localInputName = JSON.parse(localStorage.getItem("inputName"));
    if (localInputName !== null) setInputName(localInputName);
  }, []);

  //useEffect for updating a price
  useEffect(() => {
    let totalPrice = 0;

    Object.keys(pcConfiguration).forEach((key) => {
      if (pcConfiguration[key] && pcConfiguration[key].price !== undefined) {
        totalPrice += pcConfiguration[key].price;
      } else if (key === "rams" || key === "storages") {
        pcConfiguration[key].map((part) => (totalPrice += part.part.price));
      }
    });

    setTotalPrice(totalPrice);
  }, [pcConfiguration]);

  const handleNameChange = (event) => {
    const newName = event.target.value;
    setInputName(newName);
    localStorage.setItem("inputName", JSON.stringify(newName));
  };

  async function savePcConfiguration(pcConfiguration, inputName) {
    if (pcConfiguration !== null && inputName !== "") {
      var pcConfigurationIds = {
        name: inputName,
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
        .post("http://localhost:5198/api/configuration", pcConfigurationIds)
        .then((res) => {
          console.log(res.data);
          localStorage.clear();
          window.location.reload(false);

          setIsSaved(true); //tu źle poprawić
        })
        .catch((err) => console.log(err));
    }
  }

  return (
    <>
      <div>
        <div>
          {isSaved && (
            <div role="alert" className="alert alert-info">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                className="stroke-current shrink-0 w-6 h-6"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth="2"
                  d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"
                ></path>
              </svg>
              <span>Twoja konfiguracja została zapisana!</span>
            </div>
          )}
        </div>
        <div className="flex content-center">
          <p className="m-5 text-center text-2xl">Ustaw nazwę:</p>
          <input
            type="text"
            className="input input-bordered input-primary input-lg w-3/5"
            value={inputName}
            onChange={handleNameChange}
          />
        </div>
        <div className="flex justify-between">
          <ComponentsTable
            pcParts={pcParts}
            pcConfiguration={pcConfiguration}
            setPcConfiguration={setPcConfiguration}
          />
          <div id="infos">
            <Info
              configurationInfo={configurationInfo}
              totalPrice={totalPrice}
              savePcConfiguration={savePcConfiguration}
              inputName={inputName}
              pcConfiguration={pcConfiguration}
            />
            <ConfigurationInfo configurationInfo={configurationInfo} />
          </div>
        </div>
      </div>
    </>
  );
};

export default Build;
