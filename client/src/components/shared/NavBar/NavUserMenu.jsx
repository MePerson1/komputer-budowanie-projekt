import { useState } from "react";
import { NavLink } from "react-router-dom";

export const NavUserMenu = () => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  const handleMenuToggle = () => {
    setIsMenuOpen(!isMenuOpen);
  };

  const handleMenuItemClick = () => {
    setIsMenuOpen(false);
  };

  return (
    <div className="dropdown">
      <summary
        tabIndex="0"
        role="button"
        className="btn btn-ghost flex"
        onClick={handleMenuToggle}
      >
        Konto
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

      {isMenuOpen && (
        <ul
          tabIndex="0"
          className="menu menu-sm dropdown-content mt-3 z-[1] p-2 shadow bg-base-100 rounded-box w-max"
        >
          <li>
            <NavLink to="/logowanie" onClick={handleMenuItemClick}>
              Logowanie
            </NavLink>
          </li>
          <li>
            <NavLink to="/rejestracja" onClick={handleMenuItemClick}>
              Rejestracja
            </NavLink>
          </li>
        </ul>
      )}
    </div>
  );
};
