import ComponentView from "../shared/ComponentView";
import EmptyComponentView from "./EmptyComponentView";

const ComponentsTable = ({ setPcConfiguration, pcConfiguration, pcParts }) => {
  const handleSetToNull = (key) => {
    console.log(key);
    setPcConfiguration({ ...pcConfiguration, [key]: null });
  };
  return (
    <table className="table table-sm text-xs ">
      <tbody>
        <tr id="cpu">
          {pcConfiguration.cpu !== undefined && pcConfiguration.cpu !== null ? (
            <ComponentView
              pcPart={pcConfiguration.cpu}
              handleSetToNull={handleSetToNull}
              partKey={"cpu"}
            />
          ) : (
            <EmptyComponentView pcPart={pcParts[0]} />
          )}
        </tr>
        <tr id="cpuCooling">
          {pcConfiguration.cpuCooling !== null &&
          pcConfiguration.waterCooling !== undefined ? (
            <ComponentView
              pcPart={pcConfiguration.cpuCooling}
              handleSetToNull={handleSetToNull}
              partKey={"cpuCooling"}
            />
          ) : (
            <EmptyComponentView pcPart={pcParts[1]} />
          )}
        </tr>
        <tr id="motherboard">
          {pcConfiguration.motherboard !== null &&
          pcConfiguration.motherboard !== undefined ? (
            <ComponentView pcPart={pcConfiguration.motherboard} />
          ) : (
            <EmptyComponentView pcPart={pcParts[2]} />
          )}
        </tr>
        <tr id="graphicCard">
          {pcConfiguration.graphicCard !== null &&
          pcConfiguration.graphicCard !== undefined ? (
            <ComponentView pcPart={pcConfiguration.graphicCard} />
          ) : (
            <EmptyComponentView pcPart={pcParts[3]} />
          )}
        </tr>
        <tr id="rams">
          {pcConfiguration.rams !== undefined &&
          pcConfiguration.rams.length !== 0 ? (
            pcConfiguration.rams.map((ram, index) => (
              <ComponentView key={index} pcPart={ram} />
            ))
          ) : (
            <EmptyComponentView pcPart={pcParts[4]} />
          )}
        </tr>

        <tr id="storages">
          {pcConfiguration.storages !== undefined &&
          pcConfiguration.storages.length !== 0 ? (
            pcConfiguration.storages.map((storage, index) => (
              <ComponentView key={index} pcPart={storage} />
            ))
          ) : (
            <EmptyComponentView pcPart={pcParts[5]} />
          )}
        </tr>
        <tr id="powerSupply">
          {pcConfiguration.powerSupply !== null &&
          pcConfiguration.powerSupply !== undefined ? (
            <ComponentView pcPart={pcConfiguration.powerSupply} />
          ) : (
            <EmptyComponentView pcPart={pcParts[6]} />
          )}
        </tr>
        <tr id="case">
          {pcConfiguration.case !== null &&
          pcConfiguration.case.id !== 0 &&
          pcConfiguration.case !== undefined ? (
            <ComponentView pcPart={pcConfiguration.case} />
          ) : (
            <EmptyComponentView pcPart={pcParts[7]} />
          )}
        </tr>
      </tbody>
    </table>
  );
};

export default ComponentsTable;
