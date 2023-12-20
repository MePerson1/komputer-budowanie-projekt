export const NameInput = ({ name, handleNameChange }) => {
  return (
    <div className="flex content-center">
      <p className="m-5 text-center text-2xl">Ustaw nazwę:</p>
      <input
        type="text"
        className="input input-bordered input-primary input-lg w-3/5"
        value={name}
        onChange={handleNameChange}
      />
    </div>
  );
};
