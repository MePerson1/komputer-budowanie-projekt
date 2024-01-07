export const PartsTooltip = ({ name, tip }) => {
  return (
    <div className="tooltip tooltip-right" data-tip={tip}>
      <h2 className=" text-sm lg:text-2xl font-bold hover:text-info">{name}</h2>
    </div>
  );
};
