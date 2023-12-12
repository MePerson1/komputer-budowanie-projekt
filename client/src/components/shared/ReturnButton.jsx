import { useNavigate } from "react-router-dom";

const ReturnButton = () => {
  const navigate = useNavigate();
  return (
    <button className="btn btn-xs btn-accent m-1" onClick={() => navigate(-1)}>
      Powrót
    </button>
  );
};

export default ReturnButton;
