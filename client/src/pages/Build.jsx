import ComponentView from "../components/shared/ComponentView";
import { useState, useEffect } from "react";
import axios from "axios";
const Build = () => {
  useEffect(() => {
    axios.get();
  });
  return (
    <>
      <div>
        <div className="flex">
          <table className="table">
            <tbody>
              <tr>
                <ComponentView />
              </tr>
              <tr>
                <ComponentView />
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
