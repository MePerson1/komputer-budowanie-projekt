import ComponentsTable from "../components/Build/ComponentsTable";
import Info from "../components/Build/Info";
import { useState, useEffect } from "react";
import axios from "axios";
import { PcConfiguration } from "../utils/models/index";
import ConfigurationInfo from "../components/Build/ConfigurationInfo";
import pcParts from "../utils/constants/pcParts";
import mapPcPartsToIds from "../utils/functions/mapPcPartsToIds";
import { Toast } from "../utils/models";
import Topic from "../components/shared/Topic";
import { Alert } from "../components/shared/Alert";
import { NameInput } from "../components/Build/NameInput";
import { Navigate, useNavigate } from "react-router-dom";
const Build = ({ pcConfiguration, setPcConfiguration }) => {
  const navigate = useNavigate();
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
    let totalPrice = 0;

    Object.keys(pcConfiguration).forEach((key) => {
      if (pcConfiguration[key] && pcConfiguration[key].prices !== undefined) {
        totalPrice += pcConfiguration[key].prices[0].price;
      } else if (key === "rams" || key === "storages") {
        pcConfiguration[key].map((part) => (totalPrice += part.price));
      }
    });

    setTotalPrice(totalPrice);
    if (pcConfiguration !== PcConfiguration) {
      getInfo(pcConfiguration);
    }
  }, [
    pcConfiguration.case,
    pcConfiguration.cpu,
    pcConfiguration.cpuCooling,
    pcConfiguration.fans,
    pcConfiguration.graphicCard,
    pcConfiguration.id,
    pcConfiguration.motherboard,
    pcConfiguration.powerSupply,
    pcConfiguration.rams,
    pcConfiguration.storages,
    pcConfiguration.user,
    pcConfiguration.waterCooling,
  ]);

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
          var data = res.data;
          localStorage.removeItem("localConfiugration");
          setPcConfiguration(PcConfiguration);
          console.log("test");
          setIsSaved(true);
          navigate(`/configurations/${data.id}`);
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
            <Alert alertInfo={"Twoja konfiguracja została zapisana!"} />
          )}
        </div>

        <div className="flex flex-col md:flex-row justify-center">
          <div>
            <NameInput
              name={pcConfiguration.name}
              handleNameChange={handleNameChange}
            />
            <ComponentsTable
              pcParts={pcParts}
              pcConfiguration={pcConfiguration}
              setPcConfiguration={setPcConfiguration}
            />
          </div>
          <div id="infos" className="flex flex-col mt-4 md:mt-0">
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
