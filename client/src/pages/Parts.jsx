import pcParts from "../utils/constants/pcParts";
import ChooseCategory from "../components/Parts/ChooseCategory";
import Topic from "../components/shared/Topic";
const Parts = () => {
  return (
    <>
      <Topic title="Wybór typu części" />

      <div className="mt-5 grid grid-cols-1 xs:grid-cols-2 sm:grid-cols-3 lg:grid-cols-4 sm:gap-10">
        {pcParts.map((pcPart) => (
          <>
            <ChooseCategory pcPart={pcPart} />
          </>
        ))}
      </div>
    </>
  );
};

export default Parts;
