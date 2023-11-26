import ComponentRow from "../PartsTable/ComponentRow";
import { Link } from "react-router-dom";

const PartsTable = ({ parts }) => {
  return (
    <div>
      <table className="table">
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
              <ComponentRow part={part} key={index} index={index} />
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
