import PartsTable from "../components/PartsTable/PartsTable";
import { useState, useEffect } from "react";
import axios from "axios";
import ReturnButton from "../components/shared/ReturnButton";
import mapPcPartsToIds from "../utils/functions/mapPcPartsToIds";
import Topic from "../components/shared/Topic";

import { Pagination } from "../components/PartsTable/Pagination";
import { Select } from "../components/shared/Select";
import { paginationParams } from "../utils/constants/paginationParams";
import { SearchBar } from "../components/shared/SearchBar";
const PcPartsView = ({ partType, pcConfiguration, setPcConfiguration }) => {
  const sortOptions = [
    { value: "name", name: "Alfabetycznie" },
    { value: "price", name: "Cena rosnąco" },
    { value: "priceDesc", name: "Cena malejąco" },
  ];

  const [parts, setParts] = useState([]);
  const [filter, setFilter] = useState(true);
  const [paginationInfo, setPaginationInfo] = useState(paginationParams);
  const [searchTerm, setSearchTerm] = useState("");
  const [sortBy, setSortBy] = useState("");
  const [loading, setLoading] = useState(true);

  const [editedPcConfiguration, setEditedPcConfiguration] = useState();
  useEffect(() => {
    const localConfiugration = JSON.parse(
      localStorage.getItem("localConfiugration")
    );
    if (localConfiugration != null) setPcConfiguration(localConfiugration);

    const localEditedPcConfiguration = JSON.parse(
      localStorage.getItem("localEditedConfiugration")
    );

    if (localEditedPcConfiguration !== null)
      setEditedPcConfiguration(localEditedPcConfiguration);
  }, []);

  useEffect(() => {
    if (filter) {
      if (editedPcConfiguration) {
        getFilteredParts(
          partType.key,
          paginationInfo.CurrentPage,
          searchTerm,
          sortBy,
          editedPcConfiguration
        );
      } else {
        console.log(pcConfiguration + "test");
        getFilteredParts(
          partType.key,
          paginationInfo.CurrentPage,
          searchTerm,
          sortBy,
          pcConfiguration
        );
      }
    } else {
      getParts(partType.key);
    }
  }, [partType, filter]);

  useEffect(() => {
    setLoading(false);
  }, [parts]);

  const handlePageChange = async (pageNumber) => {
    setPaginationInfo({ ...paginationInfo, CurrentPage: pageNumber });
    if (filter) {
      if (editedPcConfiguration) {
        await getFilteredParts(
          partType.key,
          pageNumber,
          searchTerm,
          sortBy,
          editedPcConfiguration
        );
      } else
        await getFilteredParts(
          partType.key,
          pageNumber,
          searchTerm,
          sortBy,
          pcConfiguration
        );
    } else {
      await getParts(partType.key, pageNumber, searchTerm, sortBy);
    }
  };

  const handleSearch = async () => {
    if (filter) {
      if (editedPcConfiguration) {
        await getFilteredParts(
          partType.key,
          paginationInfo.CurrentPage,
          searchTerm,
          sortBy,
          editedPcConfiguration
        );
      } else
        await getFilteredParts(
          partType.key,
          paginationInfo.CurrentPage,
          searchTerm,
          sortBy,
          pcConfiguration
        );
    } else {
      await getParts(
        partType.key,
        paginationInfo.CurrentPage,
        searchTerm,
        sortBy
      );
    }
  };
  const handleSort = async (sortValue) => {
    setSortBy(sortValue);
    if (filter) {
      if (editedPcConfiguration) {
        await getFilteredParts(
          partType.key,
          paginationInfo.CurrentPage,
          searchTerm,
          sortValue,
          editedPcConfiguration
        );
      } else
        await getFilteredParts(
          partType.key,
          paginationInfo.CurrentPage,
          searchTerm,
          sortValue,
          pcConfiguration
        );
    } else {
      await getParts(
        partType.key,
        paginationInfo.CurrentPage,
        searchTerm,
        sortValue
      );
    }
  };

  async function getParts(partKey, pageNumber, searchTerm, sortBy) {
    setLoading(true);

    const partsParams = {
      SortBy: sortBy ? sortBy : "",
      SearchTerm: searchTerm ? searchTerm : "",
      PageNumber: pageNumber ? pageNumber : 1,
      PageSize: 10,
    };

    await axios
      .get(`http://localhost:5198/api/${partKey}/pagination`, {
        params: partsParams,
      })
      .then((res) => {
        setPaginationInfo(JSON.parse(res.headers.pagination));
        setParts(res.data);
      })
      .catch((err) => {
        if (err.response && err.response.status === 404) {
          console.error("404 Error: Not Found");
          console.log(err.response);
          setParts([]);
        } else {
          console.error("Error fetching parts:", err.message);
        }
      });
  }

  async function getFilteredParts(
    partKey,
    pageNumber,
    searchTerm,
    sortBy,
    pcConfiguration
  ) {
    setLoading(true);

    const partsParams = {
      SortBy: sortBy ? sortBy : "",
      SearchTerm: searchTerm ? searchTerm : "",
      PageNumber: pageNumber ? pageNumber : 1,
      PageSize: 10,
    };
    var pcConfigurationIds = mapPcPartsToIds(pcConfiguration);
    await axios
      .post(
        `http://localhost:5198/api/${partKey}/compatible`,
        pcConfigurationIds,
        {
          params: partsParams,
        }
      )
      .then((res) => {
        setPaginationInfo(JSON.parse(res.headers.pagination));
        setParts(res.data);
      })
      .catch((err) => {
        if (err.response && err.response.status === 404) {
          console.error("404 Error: Nie znaleziono");
          console.log(err.response);
          setParts([]);
        } else {
          console.log("Error fetching parts:", err);
        }
      });
  }

  return (
    <div>
      <Topic title={partType.namePL} />

      <ReturnButton />

      <div className="flex sm:flex-row flex-col justify-between mb-4 w-full items-start sm:items-end">
        <div className="flex ml-1 mr-2">
          <div className=" form-control ">
            <label className="pl-2 label bg-gray-900 rounded-lg border border-secondary">
              <span className="label-text">Tylko pasujące częsci</span>
              <input
                type="checkbox"
                defaultChecked={filter}
                onChange={() => setFilter((state) => !state)}
                className="checkbox m-2"
              />
            </label>
          </div>
        </div>
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
      {parts && parts.length !== 0 && (
        <div className="flex flex-col ">
          <PartsTable
            parts={parts}
            partType={partType.key}
            setPcConfiguration={setPcConfiguration}
            pcConfiguration={pcConfiguration}
            editedPcConfiguration={editedPcConfiguration}
          />
          <Pagination
            paginationInfo={paginationInfo}
            handlePageChange={handlePageChange}
          />
        </div>
      )}
      {parts.length === 0 && !loading && <div>Pusto </div>}
    </div>
  );
};

export default PcPartsView;
