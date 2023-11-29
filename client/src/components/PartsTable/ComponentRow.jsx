import { useNavigate, useLocation } from "react-router-dom";

const ComponentRow = ({ part, index }) => {
  const navigate = useNavigate();
  const location = useLocation();
  const handleDetails = () => {
    navigate(`${window.location.pathname}/${part.id}`);
  };
  return (
    <>
      <tr onClick={handleDetails} className="hover:bg-black ">
        <td>Zdjecie</td>
        <td>{part.name}</td>
        <td>{part.producer}</td>
        <td>{part.price} zł</td>
        <td>
          <button className="btn btn-primary">+</button>
        </td>
      </tr>
    </>
  );
};

export default ComponentRow;
