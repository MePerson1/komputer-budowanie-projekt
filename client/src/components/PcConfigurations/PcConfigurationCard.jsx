import { useNavigate } from "react-router-dom";
import React, { useEffect, useState } from "react";
import { DetailButton } from "../shared/DetailButton";

const PcConfigurationCard = ({ pcConfiguration }) => {
  const navigate = useNavigate();
  const [showFullDescription, setShowFullDescription] = useState(false);
  const descriptionLimit = 40;

  const toggleDescription = () => {
    setShowFullDescription(!showFullDescription);
  };
  return (
    <div className="p-5 m-2 rounded-lg justify-center bg-base-200 shadow-base-300 hover:scale-105 transform transition duration-300">
      <div className="shadow-lg p-4">
        <div>Twórca: {pcConfiguration.user.userName}</div>
        <figure className="flex flex-col justify-center items-center">
          <img
            src="images/parts/computer.png"
            className="w-60 dark:invert items"
            alt="Album"
          />
        </figure>
        <div className="mt-4 flex flex-col">
          <h2 className="text-lg font-bold">{pcConfiguration.name}</h2>
          <div
            tabIndex={0}
            className="collapse collapse-plus border border-base-300 bg-base-200"
          >
            <div className="collapse-title text-lg font-medium">Opis</div>
            <div className="collapse-content">
              <p>
                {pcConfiguration.description
                  ? pcConfiguration.description
                  : "brak"}
              </p>
            </div>
          </div>
          <p>Cena: {pcConfiguration.totalPrice.toFixed(2)} zł</p>
          <div className="flex justify-end">
            <DetailButton id={pcConfiguration.id} />
          </div>
        </div>
      </div>
    </div>
  );
};

export default PcConfigurationCard;
