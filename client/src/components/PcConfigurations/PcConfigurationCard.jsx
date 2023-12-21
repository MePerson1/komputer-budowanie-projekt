import { useNavigate } from "react-router-dom";
const PcConfigurationCard = ({ pcConfiguration }) => {
  const navigate = useNavigate();
  const handleDetails = () => {
    navigate(`${window.location.pathname}/${pcConfiguration.id}`);
  };

  return (
    <div className="p-5 m-2 border rounded-lg justify-center">
      <div className="shadow-lg p-4">
        <figure className="">
          <img
            src="images/parts/computer.png"
            className="w-64 dark:invert items"
            alt="Album"
          />
        </figure>
        <div className="mt-4">
          <h2 className="text-lg font-bold">{pcConfiguration.name}</h2>
          <p className="overflow-hidden">{pcConfiguration.description}</p>
          <p>Cena: {pcConfiguration.totalPrice.toFixed(2)} zł</p>
          <div className="flex justify-end">
            <button className="btn btn-primary" onClick={handleDetails}>
              Szczegóły
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};
export default PcConfigurationCard;
