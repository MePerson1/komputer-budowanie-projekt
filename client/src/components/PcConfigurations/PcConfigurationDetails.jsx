import pcParts from "../../utils/constants/pcParts";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import axios from "axios";
import PcConfigurationPart from "./PcConfigurationPart";
import { componentKeys } from "../../utils/constants/componentKeys";

const PcConfigurationDetails = () => {
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

  // fix ram and storages
  return (
    <div>
      {pcConfigurationById !== undefined && pcConfigurationById !== null ? (
        <>
          <h1 className="text-5xl m-5">{pcConfigurationById.name}</h1>
          <div>
            <table className="table table-sm text-xs ">
              <tbody>
                {componentKeys.map((key, index) => (
                  <tr key={index} id={key}>
                    {pcConfigurationById[key] !== undefined &&
                      pcConfigurationById[key] !== null &&
                      !(
                        Array.isArray(pcConfigurationById[key]) &&
                        pcConfigurationById[key].length === 0
                      ) &&
                      (key === "pcConfigurationRam" || key === "storages" ? (
                        pcConfigurationById[key].map((part, idx) => (
                          <PcConfigurationPart
                            key={idx}
                            pcPart={part}
                            partKey={key}
                            partType={pcParts[index]}
                          />
                        ))
                      ) : (
                        <PcConfigurationPart
                          pcPart={pcConfigurationById[key]}
                          partKey={key}
                          partType={pcParts[index]}
                        />
                      ))}
                  </tr>
                ))}
              </tbody>
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
