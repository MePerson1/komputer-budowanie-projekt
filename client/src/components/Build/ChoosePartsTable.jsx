import EmptyComponentView from "./EmptyComponentView";
import pcParts from "../../utils/constants/pcParts";
export const ChoosePartsTable = () => {
  return (
    <div className="grid grid-cols-1 gap-5 ">
      <EmptyComponentView pcPart={pcParts[0]} />

      <div className="bg-base-200 border border-white rounded lg:">
        <EmptyComponentView pcPart={pcParts[1]} />
        <div className="divider ">lub</div>
        <EmptyComponentView pcPart={pcParts[2]} />
      </div>

      <EmptyComponentView pcPart={pcParts[3]} />
      <EmptyComponentView pcPart={pcParts[4]} />
      <EmptyComponentView pcPart={pcParts[5]} />
      <EmptyComponentView pcPart={pcParts[6]} />
      <EmptyComponentView pcPart={pcParts[7]} />
    </div>
  );
};
