const PcConfigurationPart = ({ pcPart }) => {
  return (
    <div className="card card-bordered card-side bg-base-200 shadow-xl flex justify-between items-center">
      <figure className="m-10">Zdjecie</figure>
      <div className="card-body">
        <h2 className="card-title">{pcPart.name}</h2>
        <div className="flex">
          <div className="m-2">
            <p>Producent: {pcPart.producer}</p>
            {pcPart.price !== undefined && (
              <p className="font-semibold">
                Cena: {pcPart.price.toString().replace(".", ",")} zł
              </p>
            )}
            {pcPart.price === undefined && <p>Cena: N/A</p>}
          </div>
        </div>
      </div>
    </div>
  );
};

export default PcConfigurationPart;
