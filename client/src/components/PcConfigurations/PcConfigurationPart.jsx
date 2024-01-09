import { useNavigate } from "react-router-dom";
import PartPrices from "../shared/PartPrices";

const PcConfigurationPart = ({ pcPart, quantity, partType }) => {
  const navigate = useNavigate();
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
                    <p className="font-semibold">
                      Cena: <PartPrices prices={pcPart.prices} />
                    </p>
                  )}
                {pcPart.prices[0] === undefined && <p>Cena: N/A</p>}
              </div>
            </div>
          </div>
          <div className="mr-7 flex flex-col ">
            <button
              className="btn btn-sm btn-info btn-outline sm:btn-md"
              onClick={() => navigate(`/parts/${partType}/${pcPart.id}`)}
            >
              Szczegóły
            </button>
          </div>
        </div>
      )}
    </>
  );
};

export default PcConfigurationPart;
