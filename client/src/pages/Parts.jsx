import { Route, Routes } from "react-router-dom";
import pcParts from "../utils/constants/pcParts";
import ChooseCategory from "../components/Parts/ChooseCategory";
import Topic from "../components/shared/Topic";
import Breadcrumbs from "../components/shared/Breadcrumbs";
import ChooseCategoryHover from "../components/Parts/ChooseCategoryHover";
const Parts = () => {
  return (
    <>
      <Topic title="Wybór typu części" />

      <div className="flex flex-wrap justify-center ">
        {pcParts.map((pcPart) => (
          <ChooseCategory pcPart={pcPart} />
        ))}
      </div>
    </>
  );
};

export default Parts;
