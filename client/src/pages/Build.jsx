import ComponentsTable from "../components/Build/ComponentsTable";
import Info from "../components/Build/Info";
import { useState, useEffect } from "react";
import axios from "axios";
import { PcConfiguration, Motherboard } from "../models/index";
import ConfigurationInfo from "../components/Build/ConfigurationInfo";
import pcParts from "../constants/pcParts";
const Build = () => {
  let [pcConfiguration, setPcConfiguration] = useState(PcConfiguration);

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
