import { useState } from "react";
import { Link } from "react-router-dom";
import Topic from "../components/shared/Topic";

const Register = () => {
  const [login, setLogin] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [remember, setRemember] = useState(false);
  return (
    <>
      <div class="relative flex flex-col justify-center h-screen overflow-hidden ">
        <Topic title="Rejestracja" />
        <div class="w-full p-6 m-auto  rounded-md shadow-md lg:max-w-lg border border-secondary bg-base-200">
          <h1 class="text-3xl font-semibold text-center ">
            Rozpocznij przygodę z budowaniem!
          </h1>
          <form class="space-y-4">
            <div>
              <label class="label">
                <span class="text-base label-text">Login</span>
              </label>
              <input
                type="text"
                placeholder="Podaj login"
                class="w-full input input-bordered input-primary"
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
            <Link
              to="/logowanie"
              class="text-xs  hover:underline hover:text-blue-600"
            >
              Masz juz konto?
            </Link>
            <div>
              <button class="btn btn-primary">Stwórz</button>
            </div>
          </form>
        </div>
      </div>
    </>
  );
};

export default Register;
