import { useEffect, useState } from "react";
const Info = ({
  configurationInfo,
  totalPrice,
  savePcConfiguration,
  pcConfiguration,
  setPcConfiguration,
}) => {
  const [budget, setBudget] = useState(0);

  const handleBudgetChange = (event) => {
    const newValue = event.target.value;
    const newBudget = parseFloat(newValue.replace(".", ","));
    if (!isNaN(newBudget)) {
      setBudget(newBudget);
      localStorage.setItem("localBudget", JSON.stringify(budget));
    } else {
      setBudget(0);
    }
  };

  const handleDescriptionChange = (event) => {
    const newDescription = event.target.value;
    setPcConfiguration({ ...pcConfiguration, description: newDescription });
    localStorage.setItem("localDescription", JSON.stringify(newDescription));
  };

  useEffect(() => {
    const localBudget = JSON.parse(localStorage.getItem("localBudget"));
    if (localBudget !== null) setBudget(localBudget);
  }, []);

  const inputWidth = `${(budget.toString().length + 1) * 9}px`;
  return (
    <div className="m-5 rounded-md inline-block border-black border-2 shadow-lg shadow-black ">
      <table className="table border-separate p-3 w-full sm:w-96">
        <tbody>
          <tr>
            <td className="text-lg">
              <b>Budżet:</b>
            </td>
            <td className="flex text-lg ">
              <input
                type="text"
                className="bg-transparent"
                placeholder="0"
                style={{ width: inputWidth }}
                value={budget === 0 ? "" : budget}
                onChange={handleBudgetChange}
              />
              <span>
                <i>zł</i>
              </span>
            </td>
          </tr>
          <tr>
            <td className="text-lg">
              <b>Łączna cena:</b>
            </td>
            <td
              className={
                totalPrice > budget
                  ? "text-lg text-red-300"
                  : "text-lg text-green-300"
              }
            >
              {totalPrice.toFixed(2).toString().replace(".", ",")} zł
            </td>
          </tr>
          <tr>
            <td colSpan="2" className="text-center">
              <button
                className="btn btn-primary btn-sm"
                onClick={() =>
                  document.getElementById("my_modal_5").showModal()
                }
              >
                Zapisz
              </button>
              <dialog
                id="my_modal_5"
                className="modal modal-bottom sm:modal-middle"
              >
                <div className="modal-box">
                  <h3 className="font-bold text-lg">
                    Czy na pewno chcesz zapisac?
                  </h3>
                  <label className="form-control">
                    <div className="label">
                      <span className="label-text">
                        Opowiedz coś o zestawie
                      </span>
                    </div>
                    <textarea
                      onChange={handleDescriptionChange}
                      value={pcConfiguration.description}
                      className="textarea textarea-bordered h-24"
                      placeholder="Opis (opcjonalnie)"
                    ></textarea>
                  </label>
                  <div className="modal-action">
                    <div>
                      <button
                        onClick={() => savePcConfiguration(pcConfiguration)}
                        className="btn btn-primary "
                      >
                        Zapisz
                      </button>
                    </div>
                    <form method="dialog">
                      <button className="btn">Anuluj</button>
                    </form>
                  </div>
                </div>
              </dialog>
            </td>
          </tr>
          {configurationInfo && configurationInfo.problems.length !== 0 && (
            <tr>
              <td colSpan="2" className="text-red-600 text-center py-4">
                Występują problemy ze zgodnością!
              </td>
            </tr>
          )}
        </tbody>
      </table>
    </div>
  );
};

export default Info;
