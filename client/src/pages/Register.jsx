import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Topic from "../components/shared/Topic";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import {
  handleIsValid,
  handleValidation,
  validateEmail,
  validateNickname,
  validatePassword,
  validationInfo,
} from "../utils/validators";

const Register = () => {
  const navigate = useNavigate();

  const initialValues = {
    nickname: "",
    email: "",
    password: "",
  };

  const [registerValues, setRegisterValues] = useState(initialValues);
  const [validationMessages, setValidationMessages] = useState(validationInfo);
  const [errorMessage, setErrorMessage] = useState("");
  const [success, setSuccess] = useState("");

  useEffect(() => {
    setErrorMessage("");
  }, [registerValues]);

  useEffect(() => {
    const token = JSON.parse(localStorage.getItem("token"));
    if (token !== null) navigate("/");
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setRegisterValues({ ...registerValues, [name]: value });
  };

  const handleRegistration = async (e) => {
    e.preventDefault();
    setValidationMessages(handleValidation(registerValues));
    const isFormValid = handleIsValid(validationMessages);
    console.log(isFormValid);
    if (isFormValid) {
      axios
        .post("http://localhost:5198/api/user", registerValues)
        .then((response) => {
          console.log("Registration Successful", response.data);
          setSuccess("Konto utworzone pomyślnie!");
          setTimeout(() => navigate("/logowanie"), 3000);
        })
        .catch((error) => {
          if (error.response) {
            console.error("Registration Failed", error.response);
            setErrorMessage(error.response.data);
            console.log(error.response.data);
          } else if (error.request) {
            console.log(error.request);
            setErrorMessage("Coś poszło nie tak :(");
          } else {
            console.log("Error", error.message);
            setErrorMessage("Coś poszło nie tak :(");
          }
          console.log(error.config);
        });
    }
  };
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
                name="nickname"
                onChange={handleChange}
                value={registerValues.nickname}
                required
              />
              {!validationMessages[1].isValid && (
                <p className="text-error">{validationMessages[1].info}</p>
              )}
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
                name="email"
                onChange={handleChange}
                value={registerValues.email}
                required
              />
              {!validationMessages[0].isValid && (
                <p className="text-error">{validationMessages[0].info}</p>
              )}
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
                name="password"
                onChange={handleChange}
                value={registerValues.password}
                required
              />
              {!validationMessages[2].isValid && (
                <p className="text-error">{validationMessages[2].info}</p>
              )}
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
