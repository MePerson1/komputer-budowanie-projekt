const EmptyComponentView = (props) => {
  return (
    <div className="card card-bordered card-side bg-base-200 shadow-xl m-7 flex justify-between items-center">
      <figure>
        <img className="m-5 w-20 border invert" src={props.icon} alt="Movie" />
      </figure>
      <div className="card-body">
        <h2 className="card-title">{props.namePL}</h2>
        <div className="flex">
          <div className="m-5">{props.info}</div>
        </div>
      </div>
      <div className="mr-5">
        <button className="btn btn-secondary">Dodaj</button>
      </div>
    </div>
  );
};

export default EmptyComponentView;
