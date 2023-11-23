import ComponentsTable from "../components/Build/ComponentsTable";
import Info from "../components/Build/Info";
import { useState, useEffect } from "react";
import axios from "axios";
import { PcConfiguration, Motherboard } from "../models/index";
import ConfigurationInfo from "../components/Build/ConfigurationInfo";

const Build = () => {
  let [pcConfiguration, setPcConfiguration] = useState(PcConfiguration);

  const pcParts = [
    {
      nameENG: "Cpu",
      namePL: "Procesor",
      icon: "images/parts/cpu-tower.png",
      info: "Procesuje",
      tip: "Nie zapomnij śmiać się z siebie",
    },
    {
      nameENG: "Cpu Cooling",
      namePL: "Chłodzenie procesora",
      icon: "images/parts/cpu-cooling.png",
      info: "Chłodzi procesor",
      tip: "Zapomnij śmiać się z siebie",
    },
    {
      nameENG: "Motherboard",
      namePL: "Płyta główna",
      icon: "images/parts/motherboard.png",
      info: "Główno płytuje",
      tip: "Nie zapomnij śmiać się z siebie",
    },
    {
      nameENG: "Graphic Card",
      namePL: "Karta graficzna",
      icon: "images/parts/graphic-card.png",
      info: "Graficznie kartuje",
      tip: "Nie zapomnij śmiać się z siebie",
    },
    {
      nameENG: "Memory",
      namePL: "Pamięć RAM",
      icon: "images/parts/ram.png",
      info: "Pamięcio ramuje",
      tip: "Nie zapomnij śmiać się z siebie",
    },
    {
      nameENG: "Storage",
      namePL: "Dysk",
      icon: "images/parts/hard-disk.png",
      info: "Zapamiętuje",
      tip: "Nie zapomnij śmiać się z siebie",
    },
    {
      nameENG: "Case",
      namePL: "Obudowa",
      icon: "images/parts/computer-case.png",
      info: "Obudowuje",
      tip: "Nie zapomnij śmiać się z siebie",
    },
  ];
  useEffect(() => {
    getData();
    console.log(pcConfiguration);
  }, []);

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
    <>
      <div>
        <div className="flex justify-between">
          <ComponentsTable
            pcParts={pcParts}
            pcConfiguration={pcConfiguration}
          />
          <div id="infos">
            <Info />
            <ConfigurationInfo />
          </div>
        </div>
      </div>
    </>
  );
};

export default Build;
