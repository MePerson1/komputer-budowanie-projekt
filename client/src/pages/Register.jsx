import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Topic from "../components/shared/Topic";
import axios from "axios";
import { useNavigate } from "react-router-dom";

const Register = () => {
  const navigate = useNavigate();
  const [nickname, setNickname] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const [success, setSuccess] = useState("");
  useEffect(() => {
    setErrorMessage("");
  }, [nickname, email, password]);
  useEffect(() => {
    const token = JSON.parse(localStorage.getItem("token"));
    if (token !== null) navigate("/");
  }, []);
  const handleRegistration = async (e) => {
    e.preventDefault();

    axios
      .post("http://localhost:5198/api/user", { nickname, email, password })
      .then((response) => {
        console.log("Registration Successful", response.data);
        setSuccess("Konto utworzone pomyślnie!");
        setTimeout(() => navigate("/logowanie"), 3000);
      })
      .catch((error) => {
        console.error("Registration Failed", error.response.data);
        setErrorMessage(error.response.data);
      });
  };
  // TODO:
  // - wyswietlanie error o hasle itd pod danymi rzeczami
  // -walidacja po stronie klienta
  return (
    <>
      <div class="flex flex-col justify-center h-screen overflow-hidden ">
        <Topic title="Rejestracja" />
        {success && (
          <div role="alert" class="alert alert-success">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              class="stroke-current shrink-0 h-6 w-6"
              fill="none"
              viewBox="0 0 24 24"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z"
              />
            </svg>
            <span>{success}</span>
          </div>
        )}
        <div class="w-full p-6 m-auto  rounded-md shadow-md lg:max-w-lg border border-secondary bg-base-200">
          <h1 class="text-3xl font-semibold text-center ">
            Rozpocznij przygodę z budowaniem!
          </h1>
          <form class="space-y-4" onSubmit={handleRegistration}>
            <div>
              <label class="label">
                <span class="text-base label-text">Nazwa</span>
              </label>
              <input
                type="text"
                placeholder="Podaj nazwę"
                class="w-full input input-bordered input-primary"
                autoComplete="off"
                onChange={(e) => setNickname(e.target.value)}
                value={nickname}
                required
              />
            </div>
            <div>
              <label class="label">
                <span class="text-base label-text">Email</span>
              </label>
              <input
                type="text"
                placeholder="Adres Email"
                class="w-full input input-bordered input-primary"
                autoComplete="off"
                onChange={(e) => setEmail(e.target.value)}
                value={email}
                required
              />
            </div>
            <div>
              <label class="label">
                <span class="text-base label-text">Hasło</span>
              </label>
              <input
                type="password"
                placeholder="Wprowadź hasło"
                class="w-full input input-bordered input-primary"
                autoComplete="off"
                onChange={(e) => setPassword(e.target.value)}
                value={password}
                required
              />
            </div>
            <Link
              to="/logowanie"
              class="text-xs  hover:underline hover:text-blue-600"
            >
              Masz juz konto?
            </Link>
            {errorMessage && (
              <div role="alert" className="alert alert-error">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  className="stroke-current shrink-0 h-6 w-6"
                  fill="none"
                  viewBox="0 0 24 24"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth="2"
                    d="M10 14l2-2m0 0l2-2m-2 2l-2-2m2 2l2 2m7-2a9 9 0 11-18 0 9 9 0 0118 0z"
                  />
                </svg>
                <span>
                  {typeof errorMessage === "string"
                    ? errorMessage
                    : "Rejestracja nie powiodła się!"}
                </span>
              </div>
            )}
            <div>
              <button class="btn btn-primary " type="submit">
                Stwórz
              </button>
            </div>
          </form>
        </div>
      </div>
    </>
  );
};

export default Register;
