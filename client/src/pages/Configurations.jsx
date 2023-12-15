import Topic from "../components/shared/Topic";

const Configurations = () => {
  return (
    <div className="flex flex-col">
      <Topic title="Konfiguracje" />
      <div>
        <div className="flex">
          <div className="w-1/5 bg-base-300 border border-base-100">
            <div>
              <p>Filtry</p>
              <div>Producent: </div>
              <div>Rozmiar: </div>
            </div>
          </div>
          <div>
            <div className="flex">
              <button className="btn">Sortuj</button>
              <div>
                <input
                  type="text"
                  placeholder="Szukaj"
                  class="input input-bordered w-full max-w-xs"
                />
              </div>
            </div>

            <table className="table">
              <thead>
                <tr>
                  <th></th>
                  <th>Nazwa</th>
                  <th>Producent</th>
                  <th>Cena</th>
                  <th></th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <th>XD</th>
                  <th>XD</th>
                  <th>XD</th>
                  <th>XD</th>
                </tr>
              </tbody>
            </table>
            <div className="flex justify-center content-center  animate-bounce p-5">
              <span class="loading loading-spinner loading-lg"></span>
              <p className="content-cent p-5 text-3xl">Ładowanie</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Configurations;
