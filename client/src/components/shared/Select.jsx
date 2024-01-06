export const Select = ({ items }) => {
  return (
    <label className="form-control w-full max-w-xs">
      <div class="label">
        <span class="label-text font-bold text-info">Sortuj</span>
      </div>
      <select className="select select-info w-full max-w-xs">
        {items &&
          items.map((item, index) => (
            <option value={item.value} key={index}>
              {item.name}
            </option>
          ))}
      </select>
    </label>
  );
};
