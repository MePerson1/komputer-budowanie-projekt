import NavBar from "./components/shared/NavBar";
import { AppRoutes } from "./utils/routes";
import { useEffect, useState } from "react";
import { PcConfiguration, Toast } from "./utils/models";
import axios from "axios";
function App() {
  let [pcConfiguration, setPcConfiguration] = useState(PcConfiguration);
  let [configurationInfo, setConfigurationInfo] = useState(Toast);

  useEffect(() => {
    if (pcConfiguration !== PcConfiguration) {
      localStorage.setItem(
        "localConfiugration",
        JSON.stringify(pcConfiguration)
      );
      getInfo(pcConfiguration);
    }
  }, [pcConfiguration]);

  useEffect(() => {
    console.log(configurationInfo);
    const localConfiugration = JSON.parse(
      localStorage.getItem("localConfiugration")
    );
    if (localConfiugration !== null) setPcConfiguration(localConfiugration);
    console.log("localstorage read");
  }, []);
  async function getInfo(pcConfiguration) {
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

    console.log(pcConfigurationIds);

    await axios
      .post("http://localhost:5198/api/compatibility", pcConfigurationIds)
      .then((res) => {
        console.log(res.data);
        setConfigurationInfo(res.data);
      })
      .catch((err) => console.log(err));
  }

  async function getData() {
    await axios
      .get(
        "http://localhost:5198/api/Configuration/97b3af2b-f26d-4052-a408-8ec969716f65"
      )
      .then((res) => {
        console.log(res.data);
        setPcConfiguration(res.data);
      })
      .catch((err) => console.log(err));
  }

  return (
    <div className="App">
      <header className="App-header">
        <NavBar />
      </header>
      <main className="pt-5">
        <AppRoutes
          pcConfiguration={pcConfiguration}
          setPcConfiguration={setPcConfiguration}
          configurationInfo={configurationInfo}
        />
      </main>
    </div>
  );
}

export default App;
