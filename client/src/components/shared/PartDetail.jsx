import { useEffect, useState } from "react";
import { useLocation, useParams } from "react-router-dom";
import axios from "axios";

const PartDetail = () => {
  const location = useLocation();
  const [part, setPart] = useState(null);
  const { id } = useParams();
  const partType = location.pathname
    .replace(`/${id}`, "")
    .replace("/", "")
    .replace("parts", "")
    .replace("/", "");
  console.log(id);
  useEffect(() => {
    if (id) {
      getPart(partType, id);
    }
  }, [id]);

  async function getPart(partType, id) {
    try {
      const response = await axios.get(`http://localhost:5198/api/Cpu/${id}`);
      setPart(response.data);
    } catch (error) {
      console.log(error);
    }
  }

  return part !== null ? <p>{part.name}</p> : <p>Nieoczekiwany błąd</p>;
};

export default PartDetail;
