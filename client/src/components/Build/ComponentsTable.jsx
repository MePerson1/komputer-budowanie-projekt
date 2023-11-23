import ComponentView from "../shared/ComponentView";
import EmptyComponentView from "./EmptyComponentView";

const ComponentsTable = ({ pcConfiguration, pcParts }) => {
  return (
    <table className="table table-sm text-xs ">
      <tbody>
        <tr id="cpu">
          {pcConfiguration.cpu !== null && pcConfiguration.cpu.id !== 0 ? (
            <ComponentView {...pcConfiguration.cpu} />
          ) : (
            <EmptyComponentView {...pcParts[0]} />
          )}
        </tr>
        <tr id="cpuCooling">
          {pcConfiguration.cpuCooling !== null &&
          pcConfiguration.cpuCooling.id !== 0 &&
          pcConfiguration.waterCooling !== undefined ? (
            <ComponentView {...pcConfiguration.cpuCooling} />
          ) : (
            <EmptyComponentView {...pcParts[1]} />
          )}
        </tr>
        <tr id="motherboard">
          {pcConfiguration.motherboard !== null &&
          pcConfiguration.motherboard.id !== 0 &&
          pcConfiguration.motherboard !== undefined ? (
            <ComponentView {...pcConfiguration} />
          ) : (
            <EmptyComponentView {...pcParts[2]} />
          )}
        </tr>
        <tr id="graphicCard">
          {pcConfiguration.graphicCard !== null &&
          pcConfiguration.graphicCard.id !== 0 &&
          pcConfiguration.graphicCard !== undefined ? (
            <ComponentView />
          ) : (
            <EmptyComponentView {...pcParts[3]} />
          )}
        </tr>
        <tr id="ram">
          {pcConfiguration.rams !== undefined &&
          pcConfiguration.rams.length !== 0 &&
          pcConfiguration.rams[0].id !== 0 ? (
            pcConfiguration.rams.map((ram, index) => (
              <ComponentView key={index} {...ram} />
            ))
          ) : (
            <EmptyComponentView {...pcParts[4]} />
          )}
        </tr>

        <tr id="storage">
          {pcConfiguration.storage !== undefined &&
          pcConfiguration.storage.length !== 0 ? (
            <ComponentView />
          ) : (
            <EmptyComponentView {...pcParts[5]} />
          )}
        </tr>
        <tr id="powerSupply">
          {pcConfiguration.powerSupply !== null &&
          pcConfiguration.powerSupply.id !== 0 &&
          pcConfiguration.powerSupply !== undefined ? (
            <ComponentView />
          ) : (
            <EmptyComponentView {...pcParts[6]} />
          )}
        </tr>
        <tr id="case">
          {pcConfiguration.case !== null &&
          pcConfiguration.case.id !== 0 &&
          pcConfiguration.case !== undefined ? (
            <ComponentView />
          ) : (
            <EmptyComponentView {...pcParts[7]} />
          )}
        </tr>
      </tbody>
    </table>
  );
};

export default ComponentsTable;
