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
import { Navigate, useLocation, useNavigate } from "react-router-dom";
import { getTokenConfig, getUserInfo } from "../utils/apiRequests";
const Build = ({ pcConfiguration, setPcConfiguration, loggedUser }) => {
  const location = useLocation();
  const editedPcConfiguration = location.state;
  const navigate = useNavigate();
  const [totalPrice, setTotalPrice] = useState(0.0);
  const [isSaved, setIsSaved] = useState(false);
  let [configurationInfo, setConfigurationInfo] = useState(Toast);
  const [description, setDescription] = useState("");
  const [isPrivate, setIsPrivate] = useState(false);
  const [errorMessage, setError] = useState();

  useEffect(() => {
    console.log(pcConfiguration);
    if (pcConfiguration !== PcConfiguration) {
      localStorage.setItem(
        "localConfiugration",
        JSON.stringify(pcConfiguration)
      );
    }
  }, [pcConfiguration]);

  useEffect(() => {
    console.log(editedPcConfiguration);
    const localConfiugration = JSON.parse(
      localStorage.getItem("localConfiugration")
    );
    if (localConfiugration !== null) setPcConfiguration(localConfiugration);
  }, []);

  useEffect(() => {
    setError();
  }, [pcConfiguration.name]);

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
    countTotalPrice();

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

  function countTotalPrice() {
    let totalPrice = 0;

    Object.keys(pcConfiguration).forEach((key) => {
      if (
        pcConfiguration[key] &&
        pcConfiguration[key].prices &&
        pcConfiguration[key].prices[0] &&
        pcConfiguration[key].prices[0].price !== undefined
      ) {
        totalPrice += pcConfiguration[key].prices[0].price;
      } else if (key === "rams" || key === "storages") {
        pcConfiguration[key].forEach(
          (part) => part.prices[0] && (totalPrice += part.prices[0].price)
        );
      }
    });

    setTotalPrice(totalPrice);
  }

  async function savePcConfiguration(pcConfiguration) {
    if (pcConfiguration.name === "") {
      setError("Nazwa jest wymagana!");
    }
    if (pcConfiguration !== null && pcConfiguration.name !== "") {
      var pcConfigurationIds = mapPcPartsToIds(pcConfiguration);
      const token = JSON.parse(localStorage.getItem("token"));
      const loggedUser = await getUserInfo(token);
      console.log(loggedUser);
      if (token && loggedUser) {
        pcConfigurationIds.userId = loggedUser.id;
      }
      pcConfigurationIds.isPrivate = isPrivate;
      console.log(pcConfigurationIds);
      if (pcConfigurationIds.userId && token) {
        const config = getTokenConfig(token);
        await axios
          .post(
            "http://localhost:5198/api/configuration",

            pcConfigurationIds,
            config
          )
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
              errorMessage={errorMessage}
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
              setIsPrivate={setIsPrivate}
              isPrivate={isPrivate}
            />
            <ConfigurationInfo configurationInfo={configurationInfo} />
          </div>
        </div>
      </div>
    </>
  );
};

export default Build;
