import { useEffect, useState } from "react";
import { useLocation, useParams } from "react-router-dom";
import axios from "axios";
import ReturnButton from "./ReturnButton";

const PartDetail = () => {
  const location = useLocation();
  const [part, setPart] = useState(null);
  const { id } = useParams();
  const partType = location.pathname
    .replace(`/${id}`, "")
    .replace("/", "")
    .replace("parts", "")
    .replace("/", "");

  useEffect(() => {
    if (id) {
      getPart(partType, id);
    }
  }, [id, partType]);

  async function getPart(partType, id) {
    try {
      const response = await axios.get(
        `http://localhost:5198/api/${partType}/${id}`
      );
      setPart(response.data);
    } catch (error) {
      console.log(error);
    }
  }
  const renderSpecifications = () => {
    const specifications = Object.entries(part).map(([key, value]) => {
      if (typeof value === "boolean") {
        return <p key={key}>{`${key}: ${value ? "Yes" : "No"}`}</p>;
      } else if (typeof value === "number" && key !== "id") {
        return <p key={key}>{`${key}: ${value}`}</p>;
      } else if (typeof value === "string" && key !== "description") {
        return <p key={key}>{`${key}: ${value}`}</p>;
      }
      return null;
    });

    return specifications;
  };

  return (
    <div>
      <ReturnButton />

      {part !== null ? (
        <div>
          <p>Nazwa: {part.name}</p>
          <p>Producent: {part.producer}</p>
          <p>Cena: {part.price} zł</p>
          <p>Kod producenta: {part.producerCode}</p>
          <div>
            <h3>Specyfikacje:</h3>
            {renderSpecifications()}
          </div>
        </div>
      ) : (
        <div className="flex justify-center content-center  animate-bounce p-5">
          <span className="loading loading-spinner loading-lg"></span>
          <p className="content-cent p-5 text-3xl">Ładowanie</p>
        </div>
      )}
    </div>
  );
};

export default PartDetail;
