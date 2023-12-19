import { Link, NavLink } from "react-router-dom";
import { Logo } from "../Logo";
import { NavUserMenu } from "./NavUserMenu";
import { NavMenu } from "./NavMenu";

const NavBar = () => {
  return (
    <>
      <nav class="navbar bg-base-100">
        <div class="navbar-start">
          <Logo />
        </div>
        <div class="navbar-center">
          <NavMenu />
        </div>
        <div class="navbar-center hidden lg:flex">
          <ul class="menu menu-horizontal px-1">
            <li>
              <NavLink to="parts" className="pr-5">
                Części
              </NavLink>
            </li>
            <li>
              <NavLink
                to="/build"
                className="p-1 text-3xl font-bold hover:animate-pulse hover:bg-slate-200 hover:text-slate-900"
              >
                BUDUJ
              </NavLink>
            </li>
            <li>
              <NavLink to="configurations" className="pl-5">
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
