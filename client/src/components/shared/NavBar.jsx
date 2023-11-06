const NavBar = () => {
  return (
    <>
      <nav className="navbar bg-opacity-25 bg-slate-500">
        <div className="navbar-start">
          <a className="btn btn-ghost btn-circle">LOGO</a>
        </div>

        <div className="navbar-end">
          <div class="form-control">
            <input
              type="text"
              placeholder="Search"
              class="input input-bordered w-24 md:w-auto"
            />
          </div>
          <a>Zaloguj</a>
        </div>
      </nav>
      <nav className="navbar bg-opacity-25 bg-slate-500">
        <div className="navbar-center">
          <a className="pr-5">Części</a>
          <a className="p-1 text-3xl font-bold hover:animate-pulse hover:bg-slate-200 hover:text-slate-900">
            BUDUJ
          </a>
          <a className="pl-5">Konfiguracje</a>
        </div>
      </nav>
    </>
  );
};

export default NavBar;
