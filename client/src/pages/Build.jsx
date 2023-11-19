import ComponentsTable from "../components/Build/ComponentsTable";
import Info from "../components/Build/Info";
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
          <div>
            <Info />
            <div className=" m-5 rounded-md inline-block">
              <div className="bg-black bg-opacity-25 rounded-md border-black border-2 shadow-lg shadow-black p-3 pl-10 pr-10 w-full sm:w-96 flex flex-col mb-5">
                <div className="text-base mb-4">
                  <h3 className="font-bold text-lg mb-2">Problemy:</h3>
                  <ul className="list-disc pl-5">
                    <li className="text-sm text-red-600">
                      Płyta główna nie wspiera procesora tej lini!
                    </li>

                    {/* Add more list items with warning details */}
                  </ul>
                </div>
                <div className="text-base">
                  <h3 className="font-bold text-lg mb-2">Uwagi:</h3>
                  <ul className="list-disc pl-5">
                    <li className="text-sm text-blue-400">
                      Procesor nie zawiera zintegrowanego układu graficznego.
                    </li>
                  </ul>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default Build;
