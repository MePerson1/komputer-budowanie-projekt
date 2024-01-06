export const Select = ({ items, handleOnChange }) => {
  return (
    <div className="w-full max-w-xs flex items-center">
      <div className="mr-2">
        <p className="font-bold text-info">Sortuj</p>
      </div>
      <select
        className="select select-info w-full max-w-xs"
        onChange={(e) => handleOnChange(e.target.value)}
      >
        {items &&
          items.map((item, index) => (
            <option value={item.value} key={index}>
              {item.name}
            </option>
          ))}
      </select>
    </div>
  );
};
