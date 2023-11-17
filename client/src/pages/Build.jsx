import ComponentView from "../components/shared/ComponentView";
import EmptyComponentView from "../components/Build/EmptyComponentView";
import { useState, useEffect } from "react";
import axios from "axios";
import { PcConfiguration, Motherboard } from "../models/index";
const Build = () => {
  let [pcConfiguration, setPcConfiguration] = useState(PcConfiguration);

  const pcParts = [
    {
      nameENG: "Cpu",
      namePL: "Procesor",
      icon: "images/parts/cpu-tower.png",
      info: "Procesuje",
    },
    {
      nameENG: "Cpu Cooling",
      namePL: "Chłodzenie procesora",
      icon: "images/parts/cpu-cooling.png",
      info: "Chłodzi procesor",
    },
    {
      nameENG: "Motherboard",
      namePL: "Płyta główna",
      icon: "images/parts/motherboard.png",
      info: "Główno płytuje",
    },
    {
      nameENG: "Graphic Card",
      namePL: "Karta graficzna",
      icon: "images/parts/graphic-card.png",
      info: "Graficznie kartuje",
    },
    {
      nameENG: "Memory",
      namePL: "Pamięć RAM",
      icon: "images/parts/ram.png",
      info: "Pamięcio ramuje",
    },
    {
      nameENG: "Storage",
      namePL: "Dysk",
      icon: "images/parts/hard-disk.png",
      info: "Zapamiętuje",
    },
    {
      nameENG: "Case",
      namePL: "Obudowa",
      icon: "images/parts/computer-case.png",
      info: "Obudowuje",
    },
  ];
  useEffect(() => {
    getData();
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
        <div className="flex">
          <table className="table">
            <tbody>
              <tr id="cpu">
                {pcConfiguration.cpu !== null ? (
                  <ComponentView {...pcConfiguration.cpu} />
                ) : (
                  <EmptyComponentView {...pcParts[0]} />
                )}
              </tr>
              <tr id="cpuCooling">
                {pcConfiguration.cpuCooling !== null &&
                pcConfiguration.waterCooling !== undefined ? (
                  <ComponentView {...pcConfiguration.cpuCooling} />
                ) : (
                  <EmptyComponentView {...pcParts[1]} />
                )}
              </tr>
              <tr id="motherboard">
                {pcConfiguration.motherboard !== null ? (
                  <ComponentView {...pcConfiguration} />
                ) : (
                  <EmptyComponentView {...pcParts[2]} />
                )}
              </tr>
              <tr id="graphicCard">
                {pcConfiguration.graphicCard !== null ? (
                  <ComponentView />
                ) : (
                  <EmptyComponentView {...pcParts[3]} />
                )}
              </tr>
              <tr id="ram">
                {pcConfiguration.rams !== undefined &&
                pcConfiguration.rams.length !== 0 ? (
                  pcConfiguration.rams.map((ram, index) => (
                    <ComponentView key={index} {...ram} />
                  ))
                ) : (
                  <EmptyComponentView {...pcParts[4]} />
                )}
              </tr>

              <tr id="storage">
                {pcConfiguration.storage !== undefined &&
                pcConfiguration.storage.length !== 0 ? (
                  <ComponentView />
                ) : (
                  <EmptyComponentView {...pcParts[5]} />
                )}
              </tr>
              <tr id="case">
                {pcConfiguration.case !== null ? (
                  <ComponentView />
                ) : (
                  <EmptyComponentView {...pcParts[6]} />
                )}
              </tr>
            </tbody>
          </table>
          <div className="m-20 flex-col">
            <p>Dodatkowe info</p>
            <p>Cena łączna</p>
            <p>Wolatage</p>
            <button className="btn btn-primary btn-sm">zapisz</button>
          </div>
        </div>

        <div>Informacja o kompaktybilnosci</div>
      </div>
    </>
  );
};

export default Build;
