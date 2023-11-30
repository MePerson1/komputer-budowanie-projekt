const ComponentView = (props) => {
  return (
    <div className="card card-bordered card-side bg-base-200 shadow-xl m-5 flex justify-between items-center">
      <figure>
        <img
          className="m-3 w-20 border"
          src="images/parts/CpuPodgladowe.png"
          alt="cpu"
        />
      </figure>
      <div className="card-body">
        <h2 className="card-title">{props.name}</h2>
        <div className="flex">
          <div className="m-2">
            <p>Producent: {props.producer}</p>
            {props.price !== undefined && (
              <p className="font-semibold">
                Cena: {props.price.toString().replace(".", ",")} zł
              </p>
            )}
            {props.price === undefined && <p>Cena: N/A</p>}
          </div>
        </div>
      </div>
      <div className="mr-7 flex flex-col">
        <button className="m-2 btn btn-outline">Zmień</button>
        <button className="m-2 btn btn-sm bg-red-600 hover:bg-opacity-80 hover:bg-red-950">
          Usuń
        </button>
      </div>
    </div>
  );
};

export default ComponentView;
