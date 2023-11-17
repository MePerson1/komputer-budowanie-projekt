const ComponentView = (props) => {
  return (
    <div className="card card-bordered card-side bg-base-200 shadow-xl m-7 flex justify-between items-center">
      <figure>{/* Include your figure content here */}</figure>
      <div className="card-body">
        <h2 className="card-title">{props.name}</h2>
        <div className="flex">
          <div className="m-5">
            <p>Producent: {props.producer}</p>
            {props.price !== undefined && (
              <p>Cena: {props.price.toString().replace(".", ",")} zł</p>
            )}
            {props.price === undefined && <p>Cena: N/A</p>}
          </div>
        </div>
      </div>
      <div className="mr-7">
        <button className="btn btn-outline">Zmień</button>
      </div>
    </div>
  );
};

export default ComponentView;
