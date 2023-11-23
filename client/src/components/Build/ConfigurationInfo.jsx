const ConfigurationInfo = () => {
  return (
    <div className=" m-5 rounded-md inline-block">
      <div className="bg-black bg-opacity-25 rounded-md border-black border-2 shadow-lg shadow-black p-3 pl-10 pr-10 w-full sm:w-96 flex flex-col mb-5">
        <div className="text-base mb-4">
          <h3 className="font-bold text-lg mb-2">Problemy:</h3>
          <ul className="list-disc pl-5">
            <li className="text-sm text-red-600">
              Płyta główna nie wspiera procesora tej lini!
            </li>
          </ul>
        </div>
        <div className="text-base">
          <h3 className="font-bold text-lg mb-2">Uwagi:</h3>
          <ul className="list-disc pl-5">
            <li className="text-sm text-blue-400">
              Procesor nie zawiera zintegrowanego układu graficznego.
            </li>
          </ul>
        </div>
      </div>
    </div>
  );
};

export default ConfigurationInfo;
