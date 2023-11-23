import { Link } from "react-router-dom";
import { Route, Routes } from "react-router-dom";
const Parts = () => {
  return (
    <>
      <div>
        <div className="card">
          <Link class="card-title" to="/cpus">
            Procesory
          </Link>
        </div>
        <div className="card">
          <Link class="card-title" to="/motherboards">
            Płyty główne
          </Link>
        </div>
        <div className="card card-side">
          <Link class="card-title" to="/cpucoolings">
            Chłodzenie procesora
          </Link>
        </div>
        <div className="card">
          <Link class="card-title" to="/graphicCards">
            Karty graficzne
          </Link>
        </div>
        <div className="card">
          <Link class="card-title" to="/rams">
            Pamięć RAM
          </Link>
        </div>
        <div className="card">
          <Link class="card-title" to="/storages">
            Dyski SSD i HDD
          </Link>
        </div>
        <div className="card">
          <Link class="card-title" to="/powerSupplies">
            Zasilacze
          </Link>
        </div>
        <div className="card">
          <Link class="card-title" to="/cases">
            Obudowy
          </Link>
        </div>
      </div>
      <Routes></Routes>
    </>
  );
};

export default Parts;
