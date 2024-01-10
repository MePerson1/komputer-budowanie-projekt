import { useNavigate } from "react-router-dom";

export const DetailButton = ({ id, partKey }) => {
  const navigate = useNavigate();
  const handleDetails = () => {
    console.log(window.location.pathname);
    switch (window.location.pathname) {
      case "/build":
        navigate(`/parts/${partKey}/${id}`);
        break;
      case "/twoje-konfiguracje":
        navigate(`/configurations/${id}`);
        break;
      default:
        navigate(`${window.location.pathname}/${id}`);
        break;
    }
  };
  return (
    <button
      className="btn btn-sm btn-info btn-outline sm:btn-md"
      onClick={handleDetails}
    >
      Szczegóły
    </button>
  );
};
