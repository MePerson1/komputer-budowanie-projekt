import { NavLink } from "react-router-dom";

export const NavUserMenu = () => {
  return (
    <details className="dropdown dropdown-bottom dropdown-end dropdown-hover">
      <summary className="m1 btn">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          width="24"
          height="24"
          viewBox="0 0 24 24"
        >
          <path
            fill="currentColor"
            d="M12 4a4 4 0 0 1 4 4a4 4 0 0 1-4 4a4 4 0 0 1-4-4a4 4 0 0 1 4-4m0 10c4.42 0 8 1.79 8 4v2H4v-2c0-2.21 3.58-4 8-4"
          />
        </svg>
      </summary>
      <ul className="p-2 shadow menu dropdown-content z-[1] bg-base-100 rounded-box w-52">
        <li>
          <NavLink to="/logowanie">Logowanie</NavLink>
        </li>
        <li>
          <NavLink to="/rejestracja">Rejestracja</NavLink>
        </li>
      </ul>
    </details>
  );
};
