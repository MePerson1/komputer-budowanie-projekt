import axios from "axios";
import { useEffect, useState } from "react";
import { getUserInfo } from "../utils/apiRequests";

import Topic from "../components/shared/Topic";
import PcConfigurationCard from "../components/PcConfigurations/PcConfigurationCard";
import { useNavigate } from "react-router-dom";

export const UserConfigurations = () => {
  const navigate = useNavigate();
  const [userConfigurations, setUserConfigurations] = useState([]);
  const [isEmpty, setIsEmpty] = useState(false);
  useEffect(() => {
    var token = JSON.parse(localStorage.getItem("token"));
    var loggedUser = JSON.parse(localStorage.getItem("loggedUser"));
    if (
      !JSON.parse(localStorage.getItem("token")) ||
      !JSON.parse(localStorage.getItem("loggedUser"))
    ) {
      navigate("/logowanie");
    }
    if (token && loggedUser) {
      const config = { headers: { Authorization: `Bearer ${token}` } };

      axios
        .get(
          `http://localhost:5198/api/configuration/users/${loggedUser.id}`,
          config
        )
        .then((response) => {
          setUserConfigurations(response.data);
        })
        .catch((err) => {
          if (err.response && err.response.status === 401) {
            console.log("Unauthorized access. Deleting token...");
            localStorage.removeItem("token");
            localStorage.removeItem("loggedUser");
          } else if (err.response && err.response.status === 404) {
            setIsEmpty(true);
          } else {
            console.log(err);
          }
        });
    }
  }, []);

  return (
    <>
      <Topic title="Konfiguracje" />
      <div className="flex flex-col justify-center items-center p-4">
        <div className="flex flex-row justify-between mb-4 w-full items-start">
          <button className="btn mb-2 sm:mb-0">Sortuj</button>
          <div className="w-full sm:w-auto flex">
            <input
              type="text"
              placeholder="Szukaj"
              className="input input-bordered w-48 md:w-64 max-w-xs"
            />
            <button className="btn btn-info">Szukaj</button>
          </div>
        </div>
        {userConfigurations.length !== 0 && !isEmpty && (
          <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4 w-full">
            {userConfigurations.map((pcConfiguration, index) => (
              <div key={index} className="p-2">
                <PcConfigurationCard pcConfiguration={pcConfiguration} />
              </div>
            ))}
          </div>
        )}
        {isEmpty && (
          <div className="flex justify-center text-2xl">Pusto {" :("}</div>
        )}
      </div>
    </>
  );
};
