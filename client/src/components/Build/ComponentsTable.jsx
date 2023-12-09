import ComponentView from "../Build/ComponentView";
import EmptyComponentView from "./EmptyComponentView";

const ComponentsTable = ({ setPcConfiguration, pcConfiguration, pcParts }) => {
  const componentKeys = [
    "cpu",
    "cpuCooling",
    "motherboard",
    "graphicCard",
    "rams",
    "storages",
    "powerSupply",
    "case",
  ];
  const handleSetToNull = (key, partId) => {
    if (key === "rams" || key === "storages") {
      const updatedParts = pcConfiguration[key]
        .map((part) => {
          if (part.part.id === partId) {
            const updatedQuantity = part.quantity - 1;
            return {
              ...part,
              quantity: updatedQuantity >= 0 ? updatedQuantity : 0,
            };
          }
          return part;
        })
        .filter((part) => part.quantity > 0);
      setPcConfiguration({ ...pcConfiguration, [key]: updatedParts });
    } else {
      setPcConfiguration({ ...pcConfiguration, [key]: null });
    }
  };

  return (
    <table className="table table-sm text-xs ">
      <tbody>
        {componentKeys.map((key, index) => (
          <tr key={index} id={key}>
            {pcConfiguration[key] !== undefined &&
            pcConfiguration[key] !== null &&
            !(
              Array.isArray(pcConfiguration[key]) &&
              pcConfiguration[key].length === 0
            ) ? (
              key === "rams" || key === "storages" ? (
                pcConfiguration[key].map((part, idx) => (
                  <ComponentView
                    key={idx}
                    pcPart={part.part}
                    quantity={part.quantity}
                    handleSetToNull={handleSetToNull}
                    partKey={key}
                    partType={pcParts[index]}
                  />
                ))
              ) : (
                <ComponentView
                  pcPart={pcConfiguration[key]}
                  handleSetToNull={handleSetToNull}
                  partKey={key}
                  partType={pcParts[index]}
                />
              )
            ) : (
              <EmptyComponentView pcPart={pcParts[index]} />
            )}
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default ComponentsTable;
