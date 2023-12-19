import { NavLink } from "react-router-dom";

export const NavMenu = () => {
  return (
    <div class="dropdown ">
      <div tabindex="0" role="button" class="btn btn-ghost lg:hidden flex">
        Menu
        <svg
          xmlns="http://www.w3.org/2000/svg"
          width="24"
          height="24"
          viewBox="0 0 24 24"
        >
          <path fill="currentColor" d="m7 10l5 5l5-5z" />
        </svg>
      </div>

      <ul
        tabindex="0"
        class="menu menu-sm dropdown-content mt-3 z-[1] p-2 shadow bg-base-100 rounded-box w-max"
      >
        <li>
          <NavLink to="parts" className="">
            Części
          </NavLink>
        </li>
        <li>
          <NavLink to="/build" className="">
            BUDUJ
          </NavLink>
        </li>
        <li>
          <NavLink to="configurations" className="">
            Konfiguracje
          </NavLink>
        </li>
      </ul>
    </div>
  );
};
