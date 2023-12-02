import { useEffect } from "react";
const Info = ({ configurationInfo }) => {
  return (
    <div className="m-5 rounded-md inline-block border-black border-2 shadow-lg shadow-black ">
      <table className="table border-separate p-3 w-full sm:w-96">
        <tbody>
          <tr>
            <td className="text-lg">
              <b>Budżet:</b>
            </td>
            <td className="text-lg">2000 zł</td>
          </tr>
          <tr>
            <td className="text-lg">
              <b>Łączna cena:</b>
            </td>
            <td className="text-lg">661,49 zł</td>
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
