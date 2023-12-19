import { useState } from "react";
import { NavLink } from "react-router-dom";

export const NavMenu = () => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  const handleMenuToggle = () => {
    setIsMenuOpen(!isMenuOpen);
  };

  const handleMenuItemClick = () => {
    setIsMenuOpen(false);
  };

  return (
    <div className="dropdown ">
      <summary
        tabIndex="0"
        role="button"
        className="btn btn-ghost flex lg:hidden"
        onClick={handleMenuToggle}
      >
        Menu
        <svg
          xmlns="http://www.w3.org/2000/svg"
          width="24"
          height="24"
          viewBox="0 0 24 24"
        >
          <path fill="currentColor" d="m7 10l5 5l5-5z" />
        </svg>
      </summary>

      {isMenuOpen && (
        <ul
          tabIndex="0"
          className="menu menu-sm dropdown-content mt-3 z-[1] p-2 shadow bg-base-100 rounded-box "
        >
          <li>
            <NavLink to="parts" className="" onClick={handleMenuItemClick}>
              Części
            </NavLink>
          </li>
          <li>
            <NavLink
              to="/build"
              className="font-bold"
              onClick={handleMenuItemClick}
            >
              BUDUJ
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="14"
                height="14"
                viewBox="0 0 512 512"
              >
                <path
                  fill="currentColor"
                  d="m294.28 256.9l-54.42-54.41a12 12 0 0 0-17 0L12.45 401a12 12 0 0 0-.27 17.2l66.05 66.28a12 12 0 0 0 17.22-.23l198.81-210.36a12 12 0 0 0 .02-16.99m205.05-57.57l-43.89-43.58a21.46 21.46 0 0 0-15.28-6.26a21.89 21.89 0 0 0-12.79 4.14c0-.43.06-.85.09-1.22c.45-6.5 1.15-16.32-5.2-25.22a258 258 0 0 0-24.8-28.74a.6.6 0 0 0-.08-.08c-13.32-13.12-42.31-37.83-86.72-55.94A139.55 139.55 0 0 0 257.56 32C226 32 202 46.24 192.81 54.68A53.4 53.4 0 0 0 176 86.17L192 96s8.06-2 13.86-3.39a62.73 62.73 0 0 1 18.45-1.15c13.19 1.09 28.79 7.64 35.69 13.09c11.7 9.41 17.33 22.09 18.26 41.09c.2 4.23-9.52 21.35-24.16 39.84a8 8 0 0 0 .61 10.62l45.37 45.37a8 8 0 0 0 11 .25c12.07-11 30.49-28 34.67-30.59c7.69-4.73 13.19-5.64 14.7-5.8a19.18 19.18 0 0 1 11.29 2.38a1.24 1.24 0 0 1-.31.95l-1.82 1.73l-.3.28a21.52 21.52 0 0 0 .05 30.54l43.95 43.68a8 8 0 0 0 11.28 0l74.68-74.2a8 8 0 0 0 .06-11.36"
                />
              </svg>
            </NavLink>
          </li>
          <li>
            <NavLink
              to="configurations"
              className=""
              onClick={handleMenuItemClick}
            >
              Konfiguracje
            </NavLink>
          </li>
        </ul>
      )}
    </div>
  );
};
