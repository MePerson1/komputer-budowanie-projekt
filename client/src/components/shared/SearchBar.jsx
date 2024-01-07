export const SearchBar = ({ value, setValue, handleSearch }) => {
  return (
    <div className="w-full sm:w-auto flex">
      <input
        type="text"
        placeholder="Szukaj"
        className="input input-bordered w-48 md:w-64 max-w-xs"
        value={value}
        onChange={(e) => setValue(e.target.value)}
      />
      <button className="btn btn-info" onClick={handleSearch}>
        Szukaj
      </button>
    </div>
  );
};
