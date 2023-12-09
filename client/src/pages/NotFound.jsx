import { Link } from "react-router-dom";
const NotFound = () => {
  return (
    <div class="flex flex-col justify-center place-content-center place-items-center">
      <div>
        <h1 className="text-7xl">Nie znaleziono strony 😞</h1>
      </div>
      <div class="p-5">
        <Link className="btn btn-primary" to="/">
          Wróć do strony głównej
        </Link>
      </div>
    </div>
  );
};

export default NotFound;
