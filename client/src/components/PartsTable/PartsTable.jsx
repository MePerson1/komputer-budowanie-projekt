import ComponentRow from "./ComponentRow";

const PartsTable = ({
  parts,
  setPcConfiguration,
  partType,
  pcConfiguration,
  editedPcConfiguration,
}) => {
  return (
    <div>
      <table className="table table-xs">
        <thead>
          <tr>
            <th></th>
            <th>Nazwa</th>
            <th>Producent</th>
            <th>Cena</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {parts !== undefined &&
            parts !== null &&
            parts.map((part, index) => (
              <ComponentRow
                part={part}
                key={part.id}
                partType={partType}
                index={index}
                setPcConfiguration={setPcConfiguration}
                pcConfiguration={pcConfiguration}
                editedPcConfiguration={editedPcConfiguration}
              />
            ))}
        </tbody>
      </table>
      {(parts === undefined || parts === null) && (
        <div className="flex justify-center content-center  animate-bounce p-5">
          <span class="loading loading-spinner loading-lg"></span>
          <p className="content-cent p-5 text-3xl">Ładowanie</p>
        </div>
      )}
    </div>
  );
};

export default PartsTable;
