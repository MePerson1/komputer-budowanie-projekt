import { useNavigate } from "react-router";
import PartPrices from "../shared/PartPrices";
import { DetailButton } from "../shared/DetailButton";

const ComponentView = ({ pcPart, handleSetToNull, partKey, partType }) => {
  const navigate = useNavigate();
  function handleChangePart() {
    navigate(`/parts/${partType.key}`);
  }

  return (
    <>
      {pcPart && (
        <div className="bg-base-200 shadow-xl p-5 flex justify-between items-center rounded-xl">
          <div className="">
            <h2 className="text-base sm:text-lg lg:text-xl font-bold">
              {pcPart.name}
            </h2>
            <div className="flex">
              <div className="m-2">
                <p>Producent: {pcPart.producer}</p>
                {pcPart.prices !== undefined &&
                  pcPart.prices.length !== 0 &&
                  pcPart.prices[0] !== undefined && (
                    <div className="font-semibold">
                      Cena: <PartPrices prices={pcPart.prices} />
                    </div>
                  )}
                {pcPart.prices[0] === undefined && <p>Cena: N/A</p>}
              </div>
            </div>
          </div>
          <div className="mr-7 flex flex-col ">
            {partType.key !== "ram" && partType.key !== "storage" && (
              <button
                onClick={handleChangePart}
                className="m-2 btn btn-outline"
              >
                Zmień
              </button>
            )}
            <div className="scale-90">
              <DetailButton id={pcPart.id} partKey={partType.key} />
            </div>

            <button
              onClick={() => handleSetToNull(partKey, pcPart.id)}
              className="m-2 btn btn-sm btn-outline btn-error "
            >
              Usuń
            </button>
          </div>
        </div>
      )}
    </>
  );
};

export default ComponentView;
