import { useNavigate } from "react-router";

const ComponentView = ({
  pcPart,
  handleSetToNull,
  partKey,
  partType,
  quantity,
}) => {
  const navigate = useNavigate();
  function handleChangePart() {
    navigate(`/parts/${partType.key}`);
  }
  return (
    <div className="card card-bordered card-side bg-base-200 shadow-xl m-5 flex justify-between items-center">
      <figure>
        <img
          className="m-3 w-20 border"
          src="images/pcParts/CpuPodgladowe.png"
          alt="cpu"
        />
      </figure>
      <div className="card-body flex flex-row justify-between">
        <div>
          <h2 className="card-title">{pcPart.name}</h2>
          <div className="flex">
            <div className="m-2">
              <p>Producent: {pcPart.producer}</p>
              {pcPart.price !== undefined && (
                <p className="font-semibold">
                  Cena: {pcPart.price.toString().replace(".", ",")} zł
                </p>
              )}
              {pcPart.price === undefined && <p>Cena: N/A</p>}
            </div>
          </div>
        </div>
        <div className="items-center">
          {(partKey === "rams" || partKey === "storages") && (
            <div>x{quantity}</div>
          )}
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
      </div>
    </div>
  );
};

export default ComponentView;
