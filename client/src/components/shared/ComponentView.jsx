const ComponentView = (props) => {
  return (
    <div class="card card-bordered card-side bg-base-200 shadow-xl m-7">
      <figure>
        <img src="logo192.png" alt="Movie" />
      </figure>
      <div class="card-body">
        <h2 class="card-title">name</h2>
        <div class="flex ">
          <div className="m-5">
            <p>Specyfikacje</p>
            <p>Specyfikacje</p>
          </div>
          <div className="m-5">
            <p>Specyfikacje</p>
            <p>Specyfikacje</p>
          </div>
        </div>
      </div>
      <div>
        <button className="btn btn-secondary">Dodaj</button>
      </div>
    </div>
  );
};

export default ComponentView;
