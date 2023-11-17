import ComponentView from "../components/shared/ComponentView";
import { useState, useEffect } from "react";
import axios from "axios";
import { PcConfiguration, Motherboard } from "../models/index";
const Build = () => {
  let pcConfiguration = useState(PcConfiguration);
  console.log(pcConfiguration);
  console.log(pcConfiguration.motherboard);
  return (
    <>
      <div>
        <div className="flex">
          <table className="table">
            <tbody>
              <tr id="cpu">
                <ComponentView />
              </tr>
              <tr id="motherboard">
                {pcConfiguration.motherboard !== undefined ? (
                  <ComponentView />
                ) : (
                  <ComponentView />
                )}
              </tr>
              <tr>
                <ComponentView />
              </tr>
              <tr>
                <ComponentView />
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
