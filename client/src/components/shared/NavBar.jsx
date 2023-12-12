import { Link } from "react-router-dom";

const NavBar = () => {
  return (
    <>
      <nav className="navbar bg-opacity-25 bg-slate-500">
        <div className="navbar-start">
          <Link to="/" className="btn btn-ghost btn-circle">
            LOGO
          </Link>
        </div>

        <div className="navbar-end">
          <div class="form-control">
            <input
              type="text"
              placeholder="Search"
              class="input input-bordered w-24 md:w-auto"
            />
          </div>
          <Link to="/login">Zaloguj</Link>
        </div>
      </nav>
      <nav className="navbar bg-opacity-25 bg-slate-500">
        <div className="navbar-center">
          <Link to="parts" className="pr-5">
            Części
          </Link>
          <Link
            to="/build"
            className="p-1 text-3xl font-bold hover:animate-pulse hover:bg-slate-200 hover:text-slate-900"
          >
            BUDUJ
          </Link>
          <Link to="configurations" className="pl-5">
            Konfiguracje
          </Link>
        </div>
      </nav>
    </>
  );
};

export default NavBar;
