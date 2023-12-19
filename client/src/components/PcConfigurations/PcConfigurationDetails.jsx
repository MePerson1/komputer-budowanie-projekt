import ComponentView from "../Build/ComponentView";
import pcParts from "../../utils/constants/pcParts";
import { PcConfiguration } from "../../utils/models";
import { useEffect, useState } from "react";
import { useLocation, useParams } from "react-router-dom";
import axios from "axios";

const PcConfigurationDetails = () => {
  const location = useLocation();
  const { id } = useParams();
  const [pcConfigurationById, setPcConfigurationById] = useState();
  useEffect(() => {
    if (id) {
      getConfiguration(id);
    }
  }, [id]);

  async function getConfiguration(id) {
    try {
      const response = await axios.get(
        `http://localhost:5198/api/configuration/${id}`
      );
      setPcConfigurationById(response.data);
      console.log(response.data);
    } catch (error) {
      console.log(error);
    }
  }

  const componentKeys = [
    "cpu",
    "cpuCooling",
    "motherboard",
    "graphicCard",
    "pcConfigurationRams",
    "pcConfigurationStorages",
    "powerSupply",
    "case",
  ];

  return (
    <div>
      {pcConfigurationById !== undefined && pcConfigurationById !== null ? (
        <>
          <h1>{pcConfigurationById.name}</h1>
          <div>
            <table className="table table-sm text-xs ">
              <tbody></tbody>
            </table>
            <h2>{pcConfigurationById.description}</h2>
          </div>
        </>
      ) : (
        <h1>xd</h1>
      )}
    </div>
  );
};

export default PcConfigurationDetails;
