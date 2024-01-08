import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Topic from "../components/shared/Topic";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import {
  handleIsValid,
  handleValidation,
  validationInfo,
} from "../utils/validators";
import { ErrorAlert } from "../components/shared/ErrorAlert";
import { TextInput } from "../components/shared/TextInput";

const Register = () => {
  const navigate = useNavigate();

  const [registerValues, setRegisterValues] = useState({
    userName: "",
    email: "",
    password: "",
  });

  const [validationMessages, setValidationMessages] = useState(validationInfo);
  const [errorMessage, setErrorMessage] = useState("");
  const [success, setSuccess] = useState("");

  useEffect(() => {
    setErrorMessage("");
  }, [registerValues]);

  useEffect(() => {
    const token = JSON.parse(localStorage.getItem("token"));
    if (token !== null) navigate("/build");
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setRegisterValues({ ...registerValues, [name]: value });
  };

  const handleRegistration = async (e) => {
    e.preventDefault();
    setValidationMessages(handleValidation(registerValues));
    const isFormValid = handleIsValid(validationMessages);
    console.log(registerValues);
    if (isFormValid) {
      axios
        .post("http://localhost:5198/api/user", registerValues)
        .then((response) => {
          console.log("Registration Successful", response.data);
          setSuccess("Konto utworzone pomyślnie!");
          localStorage.setItem("token", JSON.stringify(response.data.token));
          setTimeout(() => window.location.reload(false), 3000);
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
      <Topic title="Rejestracja" />
      <div class="flex flex-col justify-center mt-5 sm:mt-10 overflow-hidden ">
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
              <TextInput
                type="text"
                title="Nazwa"
                name="userName"
                value={registerValues.userName}
                onChange={handleChange}
                placeholder="Podaj nazwę użytkownika"
              />
              {!validationMessages[1].isValid && (
                <p className="text-error">{validationMessages[1].info}</p>
              )}
            </div>
            <div>
              <TextInput
                type="text"
                title="E-mail"
                name="email"
                value={registerValues.email}
                onChange={handleChange}
                placeholder="Podaj adres e-mail"
              />
              {!validationMessages[0].isValid && (
                <p className="text-error">{validationMessages[0].info}</p>
              )}
            </div>
            <div>
              <TextInput
                type="password"
                title="Hasło"
                name="password"
                value={registerValues.password}
                onChange={handleChange}
                placeholder="Wprowadź hasło"
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
              <ErrorAlert
                errorMessage={errorMessage}
                extraMessage="Rejestracja nie powiodła się!"
              />
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
