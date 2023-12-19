import { useNavigate } from "react-router";

const EmptyComponentView = ({ pcPart }) => {
  const navigate = useNavigate();
  function handleChangePart() {
    navigate(`/parts/${pcPart.key}`);
  }
  return (
    pcPart && (
      <div className="card card-bordered card-side bg-base-200 shadow-xl m-5 flex justify-between items-center">
        <figure>
          <img
            className="m-3 w-20 border invert"
            src={pcPart.icon}
            alt="Movie"
          />
        </figure>
        <div className="card-body items-start">
          <div className="tooltip" data-tip={pcPart.tip}>
            <h2 className="card-title ">{pcPart.namePL}</h2>
          </div>

          <div className="resize hidden lg:flex">
            <div className="m-5">{pcPart.info}</div>
          </div>
          <div></div>
        </div>
        <div className="mr-7">
          <button onClick={handleChangePart} className="btn btn-secondary">
            Dodaj
          </button>
        </div>
      </div>
    )
  );
};

export default EmptyComponentView;
