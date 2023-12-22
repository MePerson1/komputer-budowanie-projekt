import { useNavigate } from "react-router-dom";
import React, { useState } from "react";

const PcConfigurationCard = ({ pcConfiguration }) => {
  const navigate = useNavigate();
  const [showFullDescription, setShowFullDescription] = useState(false);
  const descriptionLimit = 40;

  const toggleDescription = () => {
    setShowFullDescription(!showFullDescription);
  };
  const handleDetails = () => {
    navigate(`${window.location.pathname}/${pcConfiguration.id}`);
  };

  return (
    <div className="p-5 m-2 rounded-lg justify-center bg-base-200 shadow-base-300 hover:scale-105 transform transition duration-300">
      <div className="shadow-lg p-4">
        <figure className="">
          <img
            src="images/parts/computer.png"
            className="w-60 dark:invert items"
            alt="Album"
          />
        </figure>
        <div className="mt-4">
          <h2 className="text-lg font-bold">{pcConfiguration.name}</h2>
          <p
            className={`overflow-hidden ${
              !showFullDescription && "line-clamp-3"
            }`}
          >
            {pcConfiguration.description.length > descriptionLimit
              ? showFullDescription
                ? pcConfiguration.description
                : pcConfiguration.description.slice(0, descriptionLimit) + "..."
              : pcConfiguration.description}
            {pcConfiguration.description.length > descriptionLimit &&
              (showFullDescription ? (
                <button
                  className="text-blue-500 hover:underline ml-2"
                  onClick={toggleDescription}
                >
                  Mniej
                </button>
              ) : (
                <button
                  className="text-blue-500 hover:underline ml-2"
                  onClick={toggleDescription}
                >
                  Więcej
                </button>
              ))}
          </p>
          <p>Cena: {pcConfiguration.totalPrice.toFixed(2)} zł</p>
          <div className="flex justify-end">
            <button className="btn btn-primary" onClick={handleDetails}>
              Szczegóły
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default PcConfigurationCard;
