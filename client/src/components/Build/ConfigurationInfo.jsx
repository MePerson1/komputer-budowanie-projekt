const ConfigurationInfo = ({ configurationInfo }) => {
  return (
    <div className="m-5 rounded-md inline-block">
      <div className="bg-black bg-opacity-25 rounded-md border-black border-2 shadow-lg shadow-black p-3 pl-10 pr-10 w-full sm:w-96 flex flex-col mb-5">
        <div className="text-base mb-4">
          <h3 className="font-bold text-lg mb-2">Problemy:</h3>
          <ul className="list-disc pl-5">
            {configurationInfo.problems.length === 0 ? (
              <li className="text-sm text-red-600">Brak</li>
            ) : (
              configurationInfo.problems.map((problem, index) => (
                <li className="text-sm text-red-600" key={index}>
                  {problem}
                </li>
              ))
            )}
          </ul>
        </div>
        <div className="text-base">
          <h3 className="font-bold text-lg mb-2">Uwagi:</h3>
          <ul className="list-disc pl-5">
            {configurationInfo.warnings.length === 0 ? (
              <li className="text-sm text-blue-400">Brak</li>
            ) : (
              configurationInfo.warnings.map((warning, index) => (
                <li className="text-sm text-blue-400" key={index}>
                  {warning}
                </li>
              ))
            )}
          </ul>
        </div>
      </div>
    </div>
  );
};

export default ConfigurationInfo;
