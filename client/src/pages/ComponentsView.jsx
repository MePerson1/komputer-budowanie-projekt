import PartsTable from "../components/shared/PartsTable";
import { useState, useEffect } from "react";
import axios from "axios";
import ReturnButton from "../components/shared/ReturnButton";
import mapPcPartsToIds from "../utils/functions/mapPcPartsToIds";
import Topic from "../components/shared/Topic";
import Breadcrumbs from "../components/shared/Breadcrumbs";
import { Pagination } from "../components/PartsTable/Pagination";
import { Select } from "../components/shared/Select";
const ComponentsView = ({ partType, pcConfiguration, setPcConfiguration }) => {
  const sortOptions = [
    { value: "name", name: "Alfabetycznie" },
    { value: "price", name: "Cena rosnąco" },
    { value: "priceDesc", name: "Cena malejąco" },
  ];

  const paginationParams = {
    CurrentPage: 1,
    PageSize: 10,
    TotalCount: 0,
    TotalPages: 0,
  };

  const [parts, setParts] = useState([]);
  const [filter, setFilter] = useState(false);
  const [paginationInfo, setPaginationInfo] = useState(paginationParams);
  const [searchTerm, setSearchTerm] = useState("");
  const [sortBy, setSortBy] = useState("");

  const handlePageChange = (pageNumber) => {
    setPaginationInfo({ ...paginationInfo, CurrentPage: pageNumber });
    if (filter) {
      getFilteredParts(partType.key, pageNumber, searchTerm, sortBy);
    } else {
      getParts(partType.key, paginationInfo.CurrentPage, searchTerm, sortBy);
    }
  };

  const handleSearch = async () => {
    if (filter) {
      getFilteredParts(
        partType.key,
        paginationInfo.CurrentPage,
        searchTerm,
        sortBy
      );
    } else {
      getParts(partType.key, paginationInfo.CurrentPage, searchTerm, sortBy);
    }
  };
  useEffect(() => {
    const localConfiugration = JSON.parse(
      localStorage.getItem("localConfiugration")
    );
    setPcConfiguration(localConfiugration);
  }, []);

  useEffect(() => {
    if (filter) {
      getFilteredParts(
        partType.key,
        paginationInfo.CurrentPage,
        searchTerm,
        sortBy
      );
    } else {
      getParts(partType.key);
    }
  }, [partType, filter]);

  async function getParts(partKey, pageNumber, searchTerm, sortBy) {
    const partsParams = {
      SortBy: sortBy ? sortBy : "",
      SearchTerm: searchTerm ? searchTerm : "",
      PageNumber: pageNumber ? pageNumber : 1,
      PageSize: 10,
    };

    console.log(partsParams);
    await axios
      .get(`http://localhost:5198/api/${partKey}/pagination`, {
        params: partsParams,
      })
      .then((res) => {
        console.log(res);
        console.log(JSON.parse(res.headers.pagination));
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

  async function getFilteredParts(partKey, pageNumber, searchTerm, sortBy) {
    const partsParams = {
      SortBy: sortBy ? sortBy : "",
      SearchTerm: searchTerm ? searchTerm : "",
      PageNumber: pageNumber ? pageNumber : 1,
      PageSize: 10,
    };

    var pcConfigurationIds = mapPcPartsToIds(pcConfiguration);
    console.log(pcConfigurationIds);
    await axios
      .post(
        `http://localhost:5198/api/${partKey}/compatible`,
        pcConfigurationIds,
        {
          params: partsParams,
        }
      )
      .then((res) => {
        console.log(res);
        setPaginationInfo(JSON.parse(res.headers.pagination));
        setParts(res.data);
      })
      .catch((err) => console.log(err));
  }

  return (
    <div>
      <Topic title={partType.namePL} />

      <ReturnButton />
      <div className="flex ml-1 mr-2">
        <div className=" form-control ">
          <label className="pl-2 label bg-gray-900 rounded-lg border border-secondary">
            <span className="label-text">Tylko pasujące częsci</span>
            <input
              type="checkbox"
              defaultChecked={filter}
              onChange={() => setFilter((state) => !state)}
              className="checkbox m-5"
            />
          </label>
        </div>
      </div>
      <div className="flex flex-row justify-between mb-4 w-full items-start">
        <Select items={sortOptions} />
        <div className="w-full sm:w-auto flex">
          <input
            type="text"
            placeholder="Szukaj"
            className="input input-bordered w-48 md:w-64 max-w-xs"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
          <button className="btn btn-info" onClick={handleSearch}>
            Szukaj
          </button>
        </div>
      </div>
      {parts && parts.length !== 0 && (
        <>
          <PartsTable
            parts={parts}
            partType={partType.key}
            setPcConfiguration={setPcConfiguration}
            pcConfiguration={pcConfiguration}
          />
          <Pagination
            paginationInfo={paginationInfo}
            handlePageChange={handlePageChange}
          />
        </>
      )}
      {parts.length == 0 && <div>Pusto </div>}
      {!parts && <div>Ładowanie </div>}
    </div>
  );
};

export default ComponentsView;
