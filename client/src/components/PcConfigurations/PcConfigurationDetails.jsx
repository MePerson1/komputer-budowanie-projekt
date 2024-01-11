import pcParts from "../../utils/constants/pcParts";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import axios from "axios";
import PcConfigurationPart from "./PcConfigurationPart";
import { useNavigate } from "react-router-dom";
import Topic from "../shared/Topic";
import { PartsTooltip } from "../shared/PartsTooltip";
import ReturnButton from "../shared/ReturnButton";

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

  return (
    <>
      {pcConfigurationById !== undefined && pcConfigurationById !== null && (
        <Topic
          title={"Tytuł: " + pcConfigurationById.name}
          autor={pcConfigurationById.user.userName}
        />
      )}
      <ReturnButton />
      <div className="flex flex-col justify-center items-center">
        <div className="m-5 flex flex-col lg: w-1/2 ">
          {pcConfigurationById !== undefined && pcConfigurationById !== null ? (
            <>
              {pcConfigurationById.totalPrice && (
                <div className="flex flex-col items-center">
                  <h3 className="text-xl font-bold text-info">Łączna kwota</h3>
                  <p className="text-xl p-2">
                    {pcConfigurationById.totalPrice.toFixed(2)} zł
                  </p>
                </div>
              )}
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
                  {pcConfigurationById.cpu !== undefined &&
                    pcConfigurationById.cpu !== null && (
                      <div>
                        <PartsTooltip name={pcParts[0].namePL} />
                        <PcConfigurationPart
                          pcPart={pcConfigurationById.cpu}
                          partType={pcParts[0].key}
                        />
                      </div>
                    )}
                  {pcConfigurationById.cpuCooling !== undefined &&
                    pcConfigurationById.cpuCooling !== null && (
                      <div>
                        <PartsTooltip name={pcParts[1].namePL} />
                        <PcConfigurationPart
                          pcPart={pcConfigurationById.cpuCooling}
                          partType={pcParts[1].key}
                        />
                      </div>
                    )}
                  {pcConfigurationById.waterCooling !== undefined &&
                    pcConfigurationById.waterCooling !== null && (
                      <div>
                        <PartsTooltip name={pcParts[2].namePL} />
                        <PcConfigurationPart
                          pcPart={pcConfigurationById.waterCooling}
                          partType={pcParts[2].key}
                        />
                      </div>
                    )}
                  {pcConfigurationById.motherboard !== undefined &&
                    pcConfigurationById.motherboard !== null && (
                      <div>
                        <PartsTooltip name={pcParts[3].namePL} />
                        <PcConfigurationPart
                          pcPart={pcConfigurationById.motherboard}
                          partType={pcParts[3].key}
                        />
                      </div>
                    )}
                  {pcConfigurationById.graphicCard !== undefined &&
                    pcConfigurationById.graphicCard !== null && (
                      <div>
                        <PartsTooltip name={pcParts[4].namePL} />
                        <PcConfigurationPart
                          pcPart={pcConfigurationById.graphicCard}
                          partType={pcParts[4].key}
                        />
                      </div>
                    )}
                  {pcConfigurationById.pcConfigurationRams !== undefined &&
                    pcConfigurationById.pcConfigurationRams !== null &&
                    pcConfigurationById.pcConfigurationRams.length !== 0 && (
                      <div>
                        <PartsTooltip name={pcParts[5].namePL} />
                        {pcConfigurationById.pcConfigurationRams.map(
                          (part, idx) => (
                            <PcConfigurationPart
                              pcPart={part.ram}
                              quantity={part.quantity}
                              partType={pcParts[5].key}
                            />
                          )
                        )}
                      </div>
                    )}
                  {pcConfigurationById.pcConfigurationStorages !== undefined &&
                    pcConfigurationById.pcConfigurationStorages !== null &&
                    pcConfigurationById.pcConfigurationStorages.length !==
                      0 && (
                      <div>
                        <PartsTooltip name={pcParts[6].namePL} />
                        {pcConfigurationById.pcConfigurationStorages.map(
                          (part, idx) => (
                            <PcConfigurationPart
                              pcPart={part.storage}
                              quantity={part.quantity}
                              partType={pcParts[6].key}
                            />
                          )
                        )}
                      </div>
                    )}
                  {pcConfigurationById.powerSupply !== undefined &&
                    pcConfigurationById.powerSupply !== null && (
                      <div>
                        <PartsTooltip name={pcParts[7].namePL} />
                        <PcConfigurationPart
                          pcPart={pcConfigurationById.powerSupply}
                          partType={pcParts[7].key}
                        />
                      </div>
                    )}
                  {pcConfigurationById.case !== undefined &&
                    pcConfigurationById.case !== null && (
                      <div>
                        <PartsTooltip name={pcParts[8].namePL} />
                        <PcConfigurationPart
                          pcPart={pcConfigurationById.case}
                          partType={pcParts[8].key}
                        />
                      </div>
                    )}
                </div>
              </div>
            </>
          ) : (
            <h1>Ładowanie</h1>
          )}
        </div>
      </div>
    </>
  );
};

export default PcConfigurationDetails;
