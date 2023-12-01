import PartsTable from "../components/shared/PartsTable";
import { useState, useEffect } from "react";
import axios from "axios";
import ReturnButton from "../components/shared/ReturnButton";
const ComponentsView = ({ partType, pcConfiguration, setPcConfiguration }) => {
  const [parts, setParts] = useState(null);

  useEffect(() => {
    getParts(partType);
  }, [partType]);

  async function getParts(partType) {
    await axios
      .get(`http://localhost:5198/api/${partType}`)
      .then((res) => {
        setParts(res.data);
      })
      .catch((err) => console.log(err));
  }

  return (
    <div>
      <ReturnButton />
      <PartsTable
        parts={parts}
        partType={partType}
        setPcConfiguration={setPcConfiguration}
        pcConfiguration={pcConfiguration}
      />
    </div>
  );
};

export default ComponentsView;
