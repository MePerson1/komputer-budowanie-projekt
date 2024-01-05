import { useEffect, useState } from "react";
import Topic from "../components/shared/Topic";
import axios from "axios";
import { Link, useNavigate } from "react-router-dom";
import { ErrorAlert } from "../components/shared/ErrorAlert";

const Login = () => {
  const [remember, setRemember] = useState(false);

  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const [success, setSuccess] = useState("");

  useEffect(() => {
    const token = JSON.parse(localStorage.getItem("token"));
    if (token !== null) navigate("/");
  }, []);

  useEffect(() => {
    setErrorMessage("");
  }, [email, password]);

  const handleLogin = async (e) => {
    e.preventDefault();

    axios
      .post("http://localhost:5198/api/user/token", { email, password })
      .then((response) => {
        console.log("Registration Successful", response.data);
        setSuccess("Zalogowano pomyślnie!");
        localStorage.setItem("token", JSON.stringify(response.data.token));

        window.location.reload(false);
      })
      .catch((error) => {
        console.error("Registration Failed", error.response.data);
        setErrorMessage(error.response.data);
      });
  };

  return (
    <>
      <div class="flex flex-col justify-center h-screen ">
        <Topic title="Logowanie" />
        <div class="w-full p-6 m-auto  rounded-md shadow-md lg:max-w-lg border border-secondary bg-base-200">
          <h1 class="text-3xl font-semibold text-center ">Witaj z powrotem!</h1>
          <form class="space-y-4" onSubmit={handleLogin}>
            <div>
              <label class="label">
                <span class="text-base label-text">Email</span>
              </label>
              <input
                type="text"
                placeholder="Podaj email"
                class="w-full input input-bordered input-primary"
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
            <div className="flex justify-between ">
              <Link
                to="/rejestracja"
                class="text-xs  hover:underline hover:text-blue-600"
              >
                Nie masz konta?
              </Link>
            </div>
            {errorMessage && (
              <ErrorAlert
                errorMessage={errorMessage}
                extraMessage="Logowanie nie powiodło się!"
              />
            )}
            <div>
              <button class="btn btn-primary" type="submit">
                Zaloguj się
              </button>
            </div>
          </form>
        </div>
      </div>
    </>
  );
};

export default Login;
