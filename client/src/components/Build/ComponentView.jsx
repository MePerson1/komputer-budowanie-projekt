import { useNavigate } from "react-router";
import PartPrices from "../shared/PartPrices";
import { DetailButton } from "../shared/DetailButton";

const ComponentView = ({ pcPart, handleSetToNull, partKey, partType }) => {
  const navigate = useNavigate();
  function handleChangePart() {
    navigate(`/parts/${partType.key}`);
  }

  return (
    <div className="card card-bordered card-side bg-base-200 shadow-xl m-5 flex justify-between items-center">
      <figure className="m-10">Zdjecie</figure>
      <div className="card-body">
        <h2 className="card-title">{pcPart.name}</h2>
        <div className="flex">
          <div className="m-2">
            <p>Producent: {pcPart.producer}</p>
            {pcPart.prices !== undefined && (
              <p className="font-semibold">
                Cena: <PartPrices prices={pcPart.prices} />
              </p>
            )}
            {pcPart.prices === undefined && <p>Cena: N/A</p>}
          </div>
        </div>
      </div>
      <div className="mr-7 flex flex-col">
        <button onClick={handleChangePart} className="m-2 btn btn-outline">
          Zmień
        </button>
        <button
          onClick={() => handleSetToNull(partKey, pcPart.id)}
          className="m-2 btn btn-sm bg-red-600 hover:bg-opacity-80 hover:bg-red-950"
        >
          Usuń
        </button>
        <DetailButton id={pcPart.id} partKey={partType.key} />
      </div>
    </div>
  );
};

export default ComponentView;
