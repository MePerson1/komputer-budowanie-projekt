import { Link } from "react-router-dom";

const Home = () => {
  return (
    <>
      <h1 className="text-4xl">Witaj wbudowalni!</h1>
      <div>
        <p>Rozpocznij swoją przygodę z budowanie komputerów.</p>
        <p>Stwórz swój własny zestaw komputerowy przy pomocy naszej strony!</p>
        <p>
          Ułatwiamy proces tworzenia dla początkujących jak i dla starych
          wyjadaczy!
        </p>
        <p>
          Rozpocznij zabawę już teraz:{" "}
          {!localStorage.getItem("token") && (
            <>
              <Link className="link" to="/rejestracja">
                Załóż konto
              </Link>{" "}
              lub{" "}
            </>
          )}
          <Link className="link" to="/build">
            Buduj
          </Link>
        </p>
      </div>
    </>
  );
};

export default Home;
