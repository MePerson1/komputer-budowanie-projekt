import ComponentView from "../Build/ComponentView";
import EmptyComponentView from "./EmptyComponentView";

const ComponentsTable = ({ setPcConfiguration, pcConfiguration, pcParts }) => {
  const handleSetToNull = (key, partId) => {
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

  return (
    <>
      <div className="grid grid-cols-1 gap-5">
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

        {/* TODO: usprawnić to */}
        {pcConfiguration.cpuCooling !== null && (
          <ComponentView
            key={pcParts[1].key}
            pcPart={pcConfiguration.cpuCooling}
            handleSetToNull={handleSetToNull}
            partKey={pcParts[1].key}
            partType={pcParts[1]}
          />
        )}

        {pcConfiguration.waterCooling !== null && (
          <ComponentView
            key={pcParts[1].key}
            pcPart={pcConfiguration.waterCooling}
            handleSetToNull={handleSetToNull}
            partKey={"waterCooling"}
            partType={pcParts[2]}
          />
        )}

        {pcConfiguration.cpuColing === null ||
          (pcConfiguration.waterCooling === null && (
            <div className="bg-base-200 rounded lg:">
              <EmptyComponentView pcPart={pcParts[1]} />
              <div className="divider">lub</div>
              <EmptyComponentView pcPart={pcParts[2]} />
            </div>
          ))}

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

        {pcConfiguration.case !== undefined && pcConfiguration.case !== null ? (
          <ComponentView
            key={pcParts[8].key}
            pcPart={pcConfiguration.pcCase}
            handleSetToNull={handleSetToNull}
            partKey={"case"}
            partType={pcParts[8]}
          />
        ) : (
          <EmptyComponentView pcPart={pcParts[8]} />
        )}
      </div>
    </>
  );
};

export default ComponentsTable;
