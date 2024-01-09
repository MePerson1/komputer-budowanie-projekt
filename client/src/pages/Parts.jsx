import pcParts from "../utils/constants/pcParts";
import ChooseCategory from "../components/Parts/ChooseCategory";
import Topic from "../components/shared/Topic";
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
