import { Link } from "react-router-dom";
const ChooseCategoryHover = ({ pcPart }) => {
  return (
    <div className="group" to={pcPart.key}>
      <div className="border-4 border-black rounded-lg bg-black bg-opacity-25 p-5 m-2">
        <figure>
          <img
            src={pcPart.icon}
            alt={"Ikona" + pcPart.namePL}
            className="invert w-52"
          />
        </figure>
        <div className="text-center">
          <p className="text-lg">{pcPart.namePL}</p>
        </div>
      </div>
      <div className="invisible group-hover:visible flex">
        <div>Powietrzne</div>
        <div>Wodne</div>
      </div>
    </div>
  );
};

export default ChooseCategoryHover;
