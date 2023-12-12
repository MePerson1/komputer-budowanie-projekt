import { Route, Routes } from "react-router-dom";
import pcParts from "../utils/constants/pcParts";
import ChooseCategory from "../components/Parts/ChooseCategory";
const Parts = () => {
  return (
    <>
      <div className="flex flex-wrap justify-center">
        {pcParts.map((pcPart) => (
          <div className="p-4">
            <ChooseCategory pcPart={pcPart} />
          </div>
        ))}
      </div>
    </>
  );
};

export default Parts;
