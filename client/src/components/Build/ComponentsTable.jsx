import ComponentView from "../Build/ComponentView";
import { PartsTooltip } from "../shared/PartsTooltip";
import EmptyComponentView from "./EmptyComponentView";

const ComponentsTable = ({ setPcConfiguration, pcConfiguration, pcParts }) => {
  const handleSetToNull = (key, partId) => {
    console.log(key + " " + partId);
    if (key === "rams" || key === "storages") {
      const indexToRemove = pcConfiguration[key].findIndex(
        (part) => part.id === partId
      );
      const updatedParts = [...pcConfiguration[key]];
      if (indexToRemove !== -1) {
        updatedParts.splice(indexToRemove, 1);
        setPcConfiguration({ ...pcConfiguration, [key]: updatedParts });
      }
      if (updatedParts.length === 0) {
        updatedParts[key] = [];
      }
      setPcConfiguration({ ...pcConfiguration, [key]: updatedParts });
    } else {
      setPcConfiguration({ ...pcConfiguration, [key]: null });
    }
  };
  //TODO: zmienic troche dodatkowe informacje itd
  return (
    <>
      <div className="grid grid-cols-1 gap-5">
        <div>
          <PartsTooltip name={pcParts[0].namePL} tip={pcParts[0].tip} />
          {pcConfiguration.cpu !== undefined && pcConfiguration.cpu !== null ? (
            <ComponentView
              key={pcParts[0].key}
              pcPart={pcConfiguration.cpu}
              handleSetToNull={handleSetToNull}
              partKey={pcParts[0].key}
              partType={pcParts[0]}
            />
          ) : (
            <EmptyComponentView pcPart={pcParts[0]} />
          )}
        </div>

        <div>
          <PartsTooltip name={"Chłodzenie procesora"} tip={pcParts[1].tip} />
          {pcConfiguration.cpuCooling !== null && (
            <ComponentView
              key={pcParts[1].key}
              pcPart={pcConfiguration.cpuCooling}
              handleSetToNull={handleSetToNull}
              partKey={"cpuCooling"}
              partType={pcParts[1]}
            />
          )}

          {pcConfiguration.waterCooling !== null && (
            <ComponentView
              key={pcParts[2].key}
              pcPart={pcConfiguration.waterCooling}
              handleSetToNull={handleSetToNull}
              partKey={"waterCooling"}
              partType={pcParts[2]}
            />
          )}

          {pcConfiguration.cpuCooling === null &&
            pcConfiguration.waterCooling === null && (
              <div className="bg-base-200 rounded lg:">
                <p className="text-xl pl-2 underline text-center">Powietrzne</p>
                <EmptyComponentView pcPart={pcParts[1]} />
                <div className="divider">lub</div>
                <p className="text-xl pl-2 underline text-center">Wodne</p>
                <EmptyComponentView pcPart={pcParts[2]} />
              </div>
            )}
        </div>

        <div>
          <PartsTooltip name={pcParts[3].namePL} tip={pcParts[3].tip} />
          {pcConfiguration.motherboard !== undefined &&
          pcConfiguration.motherboard !== null ? (
            <ComponentView
              key={pcParts[3].key}
              pcPart={pcConfiguration.motherboard}
              handleSetToNull={handleSetToNull}
              partKey={pcParts[3].key}
              partType={pcParts[3]}
            />
          ) : (
            <EmptyComponentView pcPart={pcParts[3]} />
          )}
        </div>

        <div>
          <PartsTooltip name={pcParts[4].namePL} tip={pcParts[4].tip} />

          {pcConfiguration.graphicCard !== undefined &&
          pcConfiguration.graphicCard !== null ? (
            <ComponentView
              key={pcParts[4].key}
              pcPart={pcConfiguration.graphicCard}
              handleSetToNull={handleSetToNull}
              partKey={"graphicCard"}
              partType={pcParts[4]}
            />
          ) : (
            <EmptyComponentView pcPart={pcParts[4]} />
          )}
        </div>

        <div>
          <PartsTooltip name={pcParts[5].namePL} tip={pcParts[5].tip} />

          {pcConfiguration.rams !== undefined &&
          pcConfiguration.rams !== null &&
          !(
            Array.isArray(pcConfiguration.rams) &&
            pcConfiguration.rams.length === 0
          ) ? (
            pcConfiguration.rams.map((part, idx) => (
              <ComponentView
                key={idx}
                pcPart={part}
                handleSetToNull={handleSetToNull}
                partKey={"rams"}
                partType={pcParts[5]}
              />
            ))
          ) : (
            <EmptyComponentView pcPart={pcParts[5]} />
          )}
        </div>
        <div>
          <PartsTooltip name={pcParts[6].namePL} tip={pcParts[6].tip} />
          {pcConfiguration.storages !== undefined &&
          pcConfiguration.storages !== null &&
          !(
            Array.isArray(pcConfiguration.storages) &&
            pcConfiguration.storages.length === 0
          ) ? (
            pcConfiguration.storages.map((part, idx) => (
              <ComponentView
                key={idx}
                pcPart={part}
                handleSetToNull={handleSetToNull}
                partKey={"storages"}
                partType={pcParts[6]}
              />
            ))
          ) : (
            <EmptyComponentView pcPart={pcParts[6]} />
          )}
        </div>

        <div>
          <PartsTooltip name={pcParts[7].namePL} tip={pcParts[7].tip} />

          {pcConfiguration.powerSupply !== undefined &&
          pcConfiguration.powerSupply !== null ? (
            <ComponentView
              key={pcParts[7].key}
              pcPart={pcConfiguration.powerSupply}
              handleSetToNull={handleSetToNull}
              partKey={"powerSupply"}
              partType={pcParts[7]}
            />
          ) : (
            <EmptyComponentView pcPart={pcParts[7]} />
          )}
        </div>

        <div>
          <PartsTooltip name={pcParts[8].namePL} tip={pcParts[8].tip} />
          {pcConfiguration.case !== undefined &&
          pcConfiguration.case !== null ? (
            <ComponentView
              key={pcParts[8].key}
              pcPart={pcConfiguration.case}
              handleSetToNull={handleSetToNull}
              partKey={"case"}
              partType={pcParts[8]}
            />
          ) : (
            <EmptyComponentView pcPart={pcParts[8]} />
          )}
        </div>
      </div>
    </>
  );
};

export default ComponentsTable;
