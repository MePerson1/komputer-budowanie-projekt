import { useNavigate, useLocation } from "react-router-dom";
import PartPrices from "../shared/PartPrices";
import { DetailButton } from "../shared/DetailButton";

const ComponentRow = ({
  part,
  key,
  index,
  partType,
  pcConfiguration,
  setPcConfiguration,
}) => {
  const navigate = useNavigate();
  const handleDetails = () => {
    navigate(`${window.location.pathname}/${part.id}`);
  };
  const handleAddPart = (e) => {
    e.stopPropagation();

    switch (partType) {
      case "motherboard":
        setPcConfiguration({ ...pcConfiguration, motherboard: part });
        navigate("/build");
        break;
      case "cpu":
        setPcConfiguration({ ...pcConfiguration, cpu: part });
        navigate("/build");
        break;
      case "cpu-cooling":
        setPcConfiguration({ ...pcConfiguration, cpuCooling: part });
        navigate("/build");
        break;
      case "water-cooling":
        setPcConfiguration({ ...pcConfiguration, waterCooling: part });
        navigate("/build");
        break;
      case "case":
        console.log(part);
        console.log({ ...pcConfiguration, case: part });
        setPcConfiguration({ ...pcConfiguration, case: part });
        navigate("/build");
        break;
      case "graphic-card":
        setPcConfiguration({ ...pcConfiguration, graphicCard: part });
        navigate("/build");
        break;
      case "power-supply":
        setPcConfiguration({ ...pcConfiguration, powerSupply: part });
        navigate("/build");
        break;
      case "ram":
        const updatedRams = [...pcConfiguration.rams, part];
        setPcConfiguration({ ...pcConfiguration, rams: updatedRams });
        navigate("/build");
        break;
      case "storage":
        const updatedStorages = [...pcConfiguration.storages, part];
        setPcConfiguration({ ...pcConfiguration, storages: updatedStorages });
        navigate("/build");
        break;
      default:
        break;
    }
  };

  return (
    <>
      <tr className="hover:bg-black">
        <td onClick={handleDetails}>Zdjecie</td>
        <td>{part.name}</td>
        <td>{part.producer}</td>
        <td>
          <PartPrices prices={part.prices} />
        </td>
        <td className="flex-col">
          <button
            onClick={handleAddPart}
            className="btn btn-outline btn-primary mr-2"
          >
            Dodaj
          </button>
          <DetailButton id={part.id} />
        </td>
      </tr>
    </>
  );
};

export default ComponentRow;
