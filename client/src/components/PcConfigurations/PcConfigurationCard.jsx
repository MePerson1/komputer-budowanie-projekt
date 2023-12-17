const PcConfigurationCard = ({ pcConfigration }) => {
  return (
    <div className="p-5">
      <div className="card lg:card-side bg-base-100 shadow-xl">
        <figure>
          <img src="images/parts/computer.png" className="w-64" alt="Album" />
        </figure>
        <div className="card-body">
          <h2 className="card-title">{pcConfigration.name}</h2>
          <p>{pcConfigration.description}</p>
          <p>Cena: {pcConfigration.totalPrice.toFixed(2)} zł</p>
          <div className="card-actions justify-end">
            <button className="btn btn-primary">Szczegóły</button>
          </div>
        </div>
      </div>
    </div>
  );
};
export default PcConfigurationCard;
