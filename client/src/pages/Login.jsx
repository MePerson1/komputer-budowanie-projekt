import { useEffect, useState } from "react";
import Topic from "../components/shared/Topic";
import axios from "axios";
import { Link, useNavigate } from "react-router-dom";

const Login = () => {
  const [remember, setRemember] = useState(false);

  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const [success, setSuccess] = useState("");
  const [token, setToken] = useState("");

  useEffect(() => {
    setErrorMessage("");
  }, [email, password]);

  const handleLogin = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post(
        "http://localhost:5198/api/user/token",
        {
          email,
          password,
        }
      );
      //
      setSuccess("Zalogowano pomyślnie!");

      setTimeout(() => navigate("/"), 3000);
    } catch (error) {
      console.error("Registration Failed", error.response.data);
      setErrorMessage(error.response.data);
    }
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
              />
            </div>
            <div className="flex">
              <Link
                to="/rejestracja"
                class="text-xs  hover:underline hover:text-blue-600"
              >
                Nie masz konta?
              </Link>
              <a
                href="#"
                class="text-xs text-gray-600 hover:underline hover:text-blue-600"
              >
                Zapomniałeś hasła?
              </a>
            </div>

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
