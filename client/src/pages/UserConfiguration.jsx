import axios from "axios";
import { useEffect, useState } from "react";
import { getUserInfo } from "../utils/apiRequests";

import Topic from "../components/shared/Topic";
import PcConfigurationCard from "../components/PcConfigurations/PcConfigurationCard";
import { useNavigate } from "react-router-dom";
import { paginationParams } from "../utils/constants/paginationParams";
import { Pagination } from "../components/PartsTable/Pagination";
import { Select } from "../components/shared/Select";
import { SearchBar } from "../components/shared/SearchBar";

export const UserConfigurations = () => {
  const navigate = useNavigate();
  const [userConfigurations, setUserConfigurations] = useState([]);
  const [isEmpty, setIsEmpty] = useState(false);

  const sortOptions = [
    { value: "name", name: "Alfabetycznie od A-Z" },
    { value: "nameDesc", name: "Alfabetycznie od Z-A" },
    { value: "price", name: "Cena rosnąco" },
    { value: "priceDesc", name: "Cena malejąco" },
  ];

  const [paginationInfo, setPaginationInfo] = useState(paginationParams);
  const [searchTerm, setSearchTerm] = useState("");
  const [sortBy, setSortBy] = useState("");
  const [loading, setLoading] = useState(true);

  const handlePageChange = async (pageNumber) => {
    var token = JSON.parse(localStorage.getItem("token"));
    var loggedUser = JSON.parse(localStorage.getItem("loggedUser"));
    setPaginationInfo({ ...paginationInfo, CurrentPage: pageNumber });
    await getUserConfigurations(
      token,
      loggedUser,
      pageNumber,
      searchTerm,
      sortBy
    );
  };
  const handleSearch = async () => {
    var token = JSON.parse(localStorage.getItem("token"));
    var loggedUser = JSON.parse(localStorage.getItem("loggedUser"));
    await getUserConfigurations(
      token,
      loggedUser,
      paginationInfo.CurrentPage,
      searchTerm,
      sortBy
    );
  };
  const handleSort = async (sortValue) => {
    var token = JSON.parse(localStorage.getItem("token"));
    var loggedUser = JSON.parse(localStorage.getItem("loggedUser"));
    setSortBy(sortValue);
    await getUserConfigurations(
      token,
      loggedUser,
      paginationInfo.CurrentPage,
      searchTerm,
      sortValue
    );
  };

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
      getUserConfigurations(
        token,
        loggedUser,
        paginationInfo.CurrentPage,
        searchTerm,
        sortBy
      );
    }
  }, []);

  async function getUserConfigurations(
    token,
    loggedUser,
    pageNumber,
    searchTerm,
    sortBy
  ) {
    setLoading(true);

    const partsParams = {
      SortBy: sortBy ? sortBy : "",
      SearchTerm: searchTerm ? searchTerm : "",
      PageNumber: pageNumber ? pageNumber : 1,
      PageSize: 8,
    };
    if (token && loggedUser) {
      await axios
        .get(
          `http://localhost:5198/api/configuration/users/${loggedUser.id}/pagination`,
          {
            headers: { Authorization: `Bearer ${token}` },
            params: partsParams,
          }
        )
        .then((res) => {
          setPaginationInfo(JSON.parse(res.headers.pagination));
          setUserConfigurations(res.data);
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
  }

  return (
    <>
      <Topic title="Konfiguracje" />
      <div className="flex flex-col justify-center items-center p-4">
        <div className="flex flex-row justify-between mb-4 w-full items-start">
          <Select items={sortOptions} handleOnChange={handleSort} />
          <SearchBar
            value={searchTerm}
            setValue={setSearchTerm}
            handleSearch={handleSearch}
          />
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
        <Pagination
          paginationInfo={paginationInfo}
          handlePageChange={handlePageChange}
        />
      </div>
    </>
  );
};
