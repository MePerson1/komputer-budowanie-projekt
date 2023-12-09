import { useNavigate, useLocation } from "react-router-dom";

const ComponentRow = ({
  part,
  key,
  index,
  partType,
  pcConfiguration,
  setPcConfiguration,
}) => {
  const navigate = useNavigate();
  const location = useLocation();
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
      case "case":
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
        const existingRamIndex = pcConfiguration.rams.findIndex(
          (item) => item.part.id === part.id
        );
        if (existingRamIndex !== -1) {
          const updatedRams = [...pcConfiguration.rams];
          updatedRams[existingRamIndex].quantity += 1;
          setPcConfiguration({
            ...pcConfiguration,
            rams: updatedRams,
          });
          console.log(pcConfiguration);
        } else {
          const updatedRams = [
            ...pcConfiguration.rams,
            { part: part, quantity: 1 },
          ];
          setPcConfiguration({
            ...pcConfiguration,
            rams: updatedRams,
          });
          console.log(pcConfiguration);
        }

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
      <tr onClick={handleDetails} className="hover:bg-black">
        <td>Zdjecie</td>
        <td>{part.name}</td>
        <td>{part.producer}</td>
        <td>{part.price} zł</td>
        <td>
          <button onClick={handleAddPart} className="btn btn-primary">
            +
          </button>
        </td>
      </tr>
    </>
  );
};

export default ComponentRow;
