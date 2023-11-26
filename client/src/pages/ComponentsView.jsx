import PartsTable from "../components/shared/PartsTable";
import { useState, useEffect } from "react";
import axios from "axios";
const ComponentsView = ({ partType }) => {
  const [parts, setParts] = useState(null);

  useEffect(() => {
    getParts(partType);
  }, [partType]);

  async function getParts(partType) {
    switch (partType) {
      case "cpu":
        console.log("hurra");
        await axios
          .get("http://localhost:5198/api/Cpu")
          .then((res) => {
            console.log(res.data);
            setParts(res.data);
          })
          .catch((err) => console.log(err));
        break;
      case "motherboard":
        await axios
          .get("http://localhost:5198/api/Motherboard")
          .then((res) => {
            console.log(res.data);
            setParts(res.data);
          })
          .catch((err) => console.log(err));
        break;
      case "case":
        await axios
          .get("http://localhost:5198/api/Case")
          .then((res) => {
            console.log(res.data);
            setParts(res.data);
          })
          .catch((err) => console.log(err));
        break;
      case "graphicCard":
        await axios
          .get("http://localhost:5198/api/GraphicCard")
          .then((res) => {
            console.log(res.data);
            setParts(res.data);
          })
          .catch((err) => console.log(err));
        break;
      case "cpuCooling":
        await axios
          .get("http://localhost:5198/api/CpuCooling")
          .then((res) => {
            console.log(res.data);
            setParts(res.data);
          })
          .catch((err) => console.log(err));
        break;
      case "powerSupply":
        await axios
          .get("http://localhost:5198/api/PowerSupply")
          .then((res) => {
            console.log(res.data);
            setParts(res.data);
          })
          .catch((err) => console.log(err));
        break;
      case "ram":
        await axios
          .get("http://localhost:5198/api/Ram")
          .then((res) => {
            console.log(res.data);
            setParts(res.data);
          })
          .catch((err) => console.log(err));
        break;
      case "storage":
        await axios
          .get("http://localhost:5198/api/Storage")
          .then((res) => {
            console.log(res.data);
            setParts(res.data);
          })
          .catch((err) => console.log(err));
        break;
      default:
        break;
    }
  }

  return (
    <div>
      <div></div>
      <PartsTable parts={parts} />
    </div>
  );
};

export default ComponentsView;
