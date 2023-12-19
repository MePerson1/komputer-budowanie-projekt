import ComponentsTable from "../components/Build/ComponentsTable";
import Info from "../components/Build/Info";
import { useState, useEffect } from "react";
import axios from "axios";
import { PcConfiguration, Motherboard } from "../utils/models/index";
import ConfigurationInfo from "../components/Build/ConfigurationInfo";
import pcParts from "../utils/constants/pcParts";
import mapPcPartsToIds from "../utils/functions/mapPcPartsToIds";
import { Toast } from "../utils/models";
import Topic from "../components/shared/Topic";
const Build = ({ pcConfiguration, setPcConfiguration }) => {
  const [totalPrice, setTotalPrice] = useState(0.0);
  const [isSaved, setIsSaved] = useState(false);
  let [configurationInfo, setConfigurationInfo] = useState(Toast);
  const [description, setDescription] = useState("");

  useEffect(() => {
    if (pcConfiguration !== PcConfiguration) {
      localStorage.setItem(
        "localConfiugration",
        JSON.stringify(pcConfiguration)
      );
    }

    let totalPrice = 0;

    Object.keys(pcConfiguration).forEach((key) => {
      if (pcConfiguration[key] && pcConfiguration[key].price !== undefined) {
        totalPrice += pcConfiguration[key].price;
      } else if (key === "rams" || key === "storages") {
        pcConfiguration[key].map((part) => (totalPrice += part.price));
      }
    });

    setTotalPrice(totalPrice);
  }, [pcConfiguration]);

  useEffect(() => {
    const localConfiugration = JSON.parse(
      localStorage.getItem("localConfiugration")
    );
    if (localConfiugration !== null) setPcConfiguration(localConfiugration);
  }, []);

  async function getInfo(pcConfiguration) {
    var pcConfigurationIds = mapPcPartsToIds(pcConfiguration);

    await axios
      .post("http://localhost:5198/api/compatibility", pcConfigurationIds)
      .then((res) => {
        console.log(res.data);
        setConfigurationInfo(res.data);
      })
      .catch((err) => console.log(err));
  }

  useEffect(() => {
    if (pcConfiguration !== PcConfiguration) {
      getInfo(pcConfiguration);
    }
  }, []);

  const handleNameChange = (event) => {
    const newName = event.target.value;
    setPcConfiguration({ ...pcConfiguration, name: newName });
  };

  async function savePcConfiguration(pcConfiguration) {
    if (pcConfiguration !== null && pcConfiguration.name !== "") {
      console.log(pcConfiguration);
      var pcConfigurationIds = mapPcPartsToIds(pcConfiguration);
      console.log(pcConfigurationIds);
      await axios
        .post("http://localhost:5198/api/configuration", pcConfigurationIds)
        .then((res) => {
          console.log(res.data);
          //ToDo do poprawy
          localStorage.clear();
          window.location.reload(false);
          setIsSaved(true);
        })
        .catch((err) => console.log(err));
    }
  }

  return (
    <>
      <div>
        <Topic title="Budowanie" />
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
            value={pcConfiguration.name}
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
              pcConfiguration={pcConfiguration}
              description={description}
              setDescription={setDescription}
              setPcConfiguration={setPcConfiguration}
            />
            <ConfigurationInfo configurationInfo={configurationInfo} />
          </div>
        </div>
      </div>
    </>
  );
};

export default Build;
