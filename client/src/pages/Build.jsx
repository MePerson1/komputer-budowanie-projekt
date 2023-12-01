import ComponentsTable from "../components/Build/ComponentsTable";
import Info from "../components/Build/Info";
import { useState, useEffect } from "react";
import axios from "axios";
import { PcConfiguration, Motherboard } from "../utils/models/index";
import ConfigurationInfo from "../components/Build/ConfigurationInfo";
import pcParts from "../utils/constants/pcParts";
const Build = ({ pcConfiguration, setPcConfiguration }) => {
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
            <Info />
            <ConfigurationInfo />
          </div>
        </div>
      </div>
    </>
  );
};

export default Build;
