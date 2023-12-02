import { useEffect, useState } from "react";
const Info = ({ configurationInfo, totalPrice }) => {
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

  //TODO
  /*
    Jeżeli wyświetlam części to te które przekraczają mają dodatkowy wykrzyknik  
  */
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
            <td className="text-lg">
              <b>Szacowana moc:</b>
            </td>
            <td className="text-lg">100 W</td>
          </tr>

          <tr>
            <td colSpan="2" className="text-center">
              <button className="btn btn-primary btn-sm">zapisz</button>
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
