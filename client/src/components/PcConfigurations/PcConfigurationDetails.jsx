import pcParts from "../../utils/constants/pcParts";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import axios from "axios";
import PcConfigurationPart from "./PcConfigurationPart";
import { componentKeys } from "../../utils/constants/componentKeys";
import Topic from "../shared/Topic";

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
    <>
      {pcConfigurationById !== undefined && pcConfigurationById !== null && (
        <Topic
          title={"Tytuł: " + pcConfigurationById.name}
          autor={pcConfigurationById.user.userName}
        />
      )}

      <div className="m-5">
        {pcConfigurationById !== undefined && pcConfigurationById !== null ? (
          <>
            {pcConfigurationById.description && (
              <div className="flex flex-col ">
                <p className="text-xl font-bold text-info">Opis</p>
                <p className="text-lg border border-double border-info p-5">
                  {pcConfigurationById.description}
                </p>
              </div>
            )}
            <div className="mt-5">
              <div className="grid grid-cols-1 gap-5">
                {componentKeys.map((key, index) => (
                  <div key={index} id={key}>
                    {pcConfigurationById[key] !== undefined &&
                      pcConfigurationById[key] !== null &&
                      !(
                        Array.isArray(pcConfigurationById[key]) &&
                        pcConfigurationById[key].length === 0
                      ) &&
                      (key === "pcConfigurationRam" || key === "storages" ? (
                        pcConfigurationById[key].map((part, idx) => (
                          <>
                            <h2 className=" text-sm lg:text-2xl font-bold hover:text-info">
                              {pcParts[index].namePL}
                            </h2>
                            <PcConfigurationPart
                              key={idx}
                              pcPart={part}
                              partKey={key}
                              partType={pcParts[index]}
                            />
                          </>
                        ))
                      ) : (
                        <>
                          <h2 className=" text-sm lg:text-2xl font-bold">
                            {pcParts[index].namePL}
                          </h2>
                          <PcConfigurationPart
                            pcPart={pcConfigurationById[key]}
                            partKey={key}
                            partType={pcParts[index]}
                          />
                        </>
                      ))}
                  </div>
                ))}
              </div>
            </div>
          </>
        ) : (
          <h1>xd</h1>
        )}
      </div>
    </>
  );
};

export default PcConfigurationDetails;
