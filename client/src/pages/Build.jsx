import ComponentsTable from "../components/Build/ComponentsTable";
import Info from "../components/Build/Info";
import { useState, useEffect } from "react";
import axios from "axios";
import { PcConfiguration, Motherboard } from "../utils/models/index";
import ConfigurationInfo from "../components/Build/ConfigurationInfo";
import pcParts from "../utils/constants/pcParts";
const Build = ({ pcConfiguration, setPcConfiguration, configurationInfo }) => {
  const [totalPrice, setTotalPrice] = useState(0.0);

  useEffect(() => {
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
  return (
    <>
      <div>
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
            />
            <ConfigurationInfo configurationInfo={configurationInfo} />
          </div>
        </div>
      </div>
    </>
  );
};

export default Build;
