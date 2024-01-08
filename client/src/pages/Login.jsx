import { useEffect, useState } from "react";
import Topic from "../components/shared/Topic";
import axios from "axios";
import { Link, useNavigate } from "react-router-dom";
import { ErrorAlert } from "../components/shared/ErrorAlert";
import { TextInput } from "../components/shared/TextInput";

const Login = () => {
  const navigate = useNavigate();

  const [errorMessage, setErrorMessage] = useState();
  const [success, setSuccess] = useState("");

  const [loginValues, setLoginValues] = useState({
    email: "",
    password: "",
  });

  useEffect(() => {
    const token = JSON.parse(localStorage.getItem("token"));
    if (token !== null) navigate("/build");
  }, []);

  const handleLoginValues = (e) => {
    const { name, value } = e.target;
    setLoginValues({ ...loginValues, [name]: value });
  };

  const handleLogin = async (e) => {
    e.preventDefault();
    setErrorMessage("");
    axios
      .post("http://localhost:5198/api/user/token", loginValues)
      .then((response) => {
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
      <Topic title="Logowanie" />
      <div class="flex flex-col justify-center mt-5 sm:mt-10 overflow-hidden">
        <div class="w-full p-6 m-auto  rounded-md shadow-md lg:max-w-lg border border-secondary bg-base-200">
          <h1 class="text-3xl font-semibold text-center ">Witaj z powrotem!</h1>
          <form class="space-y-4" onSubmit={handleLogin}>
            <div>
              <TextInput
                type="text"
                title="E-mail"
                name="email"
                value={loginValues.email}
                onChange={handleLoginValues}
                placeholder="Podaj swój adres e-mail."
              />
            </div>
            <div>
              <TextInput
                type="password"
                title="Hasło"
                name="password"
                value={loginValues.password}
                onChange={handleLoginValues}
                placeholder="Wprowadź hasło"
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
