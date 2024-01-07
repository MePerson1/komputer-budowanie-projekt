import { useEffect, useState } from "react";
import Topic from "../components/shared/Topic";
import axios from "axios";
import PcConfigurationCard from "../components/PcConfigurations/PcConfigurationCard";
import { paginationParams } from "../utils/constants/paginationParams";
import { Pagination } from "../components/PartsTable/Pagination";
import { SearchBar } from "../components/shared/SearchBar";
import { Select } from "../components/shared/Select";

const Configurations = () => {
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

  const [pcConfigurations, setPcConfigurations] = useState([]);
  useEffect(() => {
    getAllConfigurations();
  }, []);

  useEffect(() => {
    setLoading(false);
  }, [pcConfigurations]);

  const handlePageChange = async (pageNumber) => {
    setPaginationInfo({ ...paginationInfo, CurrentPage: pageNumber });
    await getAllConfigurations(pageNumber, searchTerm, sortBy);
  };
  const handleSearch = async () => {
    await getAllConfigurations(paginationInfo.CurrentPage, searchTerm, sortBy);
  };
  const handleSort = async (sortValue) => {
    setSortBy(sortValue);
    await getAllConfigurations(
      paginationInfo.CurrentPage,
      searchTerm,
      sortValue
    );
  };

  async function getAllConfigurations(pageNumber, searchTerm, sortBy) {
    setLoading(true);

    const partsParams = {
      SortBy: sortBy ? sortBy : "",
      SearchTerm: searchTerm ? searchTerm : "",
      PageNumber: pageNumber ? pageNumber : 1,
      PageSize: 8,
    };

    await axios
      .get("http://localhost:5198/api/configuration/public/pagination", {
        params: partsParams,
      })
      .then((res) => {
        setPaginationInfo(JSON.parse(res.headers.pagination));
        setPcConfigurations(res.data);
      })
      .catch((err) => {
        if (err.response && err.response.status === 404) {
          console.error("404 Error: Nie znaleziono");
          console.log(err.response);
          setPcConfigurations([]);
        } else {
          console.error("Error fetching parts:", err.message);
        }
      });
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
        {loading && (
          <div className="flex justify-center items-center">
            <span class="loading loading-spinner loading-xs"></span>
            <p>Ładowanie</p>
          </div>
        )}
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4 w-full">
          {pcConfigurations.length !== 0 &&
            pcConfigurations.map((pcConfiguration, index) => (
              <div key={index} className="p-2">
                <PcConfigurationCard pcConfiguration={pcConfiguration} />
              </div>
            ))}
        </div>
        <Pagination
          paginationInfo={paginationInfo}
          handlePageChange={handlePageChange}
        />
      </div>
    </>
  );
};

export default Configurations;
