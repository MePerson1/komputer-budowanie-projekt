import { Link, NavLink } from "react-router-dom";
import { Logo } from "../Logo";
import { NavUserMenu } from "./NavUserMenu";
import { NavMenu } from "./NavMenu";

const NavBar = () => {
  return (
    <>
      <nav class="navbar bg-base-300">
        <div class="navbar-start">
          <Logo />
        </div>
        <div class="navbar-center">
          <NavMenu />
        </div>
        <div class="navbar-center hidden lg:flex ">
          <ul class="menu menu-horizontal px-1">
            <li>
              <NavLink to="parts" className="text-lg">
                Części
              </NavLink>
            </li>
            <li>
              <NavLink
                to="/build"
                className="text-xl font-bold border border-white mr-2 ml-2"
              >
                BUDUJ
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  width="24"
                  height="24"
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
              <NavLink to="configurations" className="text-lg">
                Konfiguracje
              </NavLink>
            </li>
          </ul>
        </div>
        <div class="navbar-end">
          <NavUserMenu />
        </div>
      </nav>
    </>
  );
};

export default NavBar;