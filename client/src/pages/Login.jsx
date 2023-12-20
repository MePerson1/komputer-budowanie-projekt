import { useState } from "react";
import Topic from "../components/shared/Topic";

const Login = () => {
  const [login, setLogin] = useState("");
  const [password, setPassword] = useState("");
  const [remember, setRemember] = useState(false);
  return (
    <>
      <div class="flex flex-col justify-center h-screen ">
        <Topic title="Logowanie" />
        <div class="w-full p-6 m-auto  rounded-md shadow-md lg:max-w-lg border border-secondary bg-base-200">
          <h1 class="text-3xl font-semibold text-center ">Witaj z powrotem!</h1>
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
                <span class="text-base label-text">Hasło</span>
              </label>
              <input
                type="password"
                placeholder="Wprowadź hasło"
                class="w-full input input-bordered input-primary"
              />
            </div>
            <a
              href="#"
              class="text-xs text-gray-600 hover:underline hover:text-blue-600"
            >
              Zapomniałeś hasła?
            </a>
            <div>
              <button class="btn btn-primary">Zaloguj się</button>
            </div>
          </form>
        </div>
      </div>
    </>
  );
};

export default Login;
