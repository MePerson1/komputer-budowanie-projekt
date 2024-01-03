import { useNavigate } from "react-router";

const PartRow = ({ pcPart }) => {
  const navigate = useNavigate();
  function handleChangePart(partType) {
    navigate(`/parts/${partType}`);
  }
  return (
    <>
      {pcPart && (
        <div className="bg-base-200 shadow-xl flex justify-between items-center rounded-xl p-2">
          <figure>
            <img
              className="m-3 w-10 sm:w-20 h-auto invert"
              style={{ minWidth: "2rem", maxWidth: "5rem" }}
              src={pcPart.icon}
              alt="Movie"
            />
          </figure>

          <div className="items-start ">
            <div className="tooltip" data-tip={pcPart.tip}>
              <h2 className=" text-sm lg:text-2xl font-bold">
                {pcPart.namePL}
              </h2>
            </div>

            <div className="resize hidden lg:flex">
              <div className="m-5">{pcPart.info}</div>
            </div>
            <div></div>
          </div>
          <div className="mr-7">
            <button
              onClick={() => handleChangePart(pcPart.key)}
              className="btn btn-secondary"
            >
              Dodaj
            </button>
          </div>
        </div>
      )}
    </>
  );
};

export default PartRow;
