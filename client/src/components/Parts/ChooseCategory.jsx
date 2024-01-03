import { Link } from "react-router-dom";
const ChooseCategory = ({ pcPart }) => {
  return (
    <Link className="hover:animate-pulse" to={pcPart.key}>
      <div className="border-4 border-black rounded-lg bg-black bg-opacity-25 p-5 m-5">
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
    </Link>
  );
};

export default ChooseCategory;
