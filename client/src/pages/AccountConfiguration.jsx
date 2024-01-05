import { useEffect, useState } from "react";
import Topic from "../components/shared/Topic";
import { TextInput } from "../components/shared/TextInput";
import { Link } from "react-router-dom";
import { getTokenConfig } from "../utils/apiRequests";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import {
  handleIsValid,
  handleValidation,
  validationInfo,
} from "../utils/validators";
import { ErrorAlert } from "../components/shared/ErrorAlert";

export const AccountConfiguration = () => {
  const navigate = useNavigate();
  const [loggedUser, setLoggedUser] = useState(null);

  const [validationMessages, setValidationMessages] = useState(validationInfo);

  const [changeEmailValues, setChangeEmailValues] = useState({
    email: "",
    currentPassword: "",
  });
  const [emailChangeSucceed, setEmailChangeSuccseded] = useState();
  const [emailChangeError, setEmailChangeError] = useState("");

  const [changePasswordValues, setChangePasswordValues] = useState({
    password: "",
    currentPassword: "",
  });

  const [passwordChangeSucceed, setPasswordChangeSuccseded] = useState();
  const [passwordChangeError, setPasswordChangeError] = useState("");
  const [token, setToken] = useState("");

  const handleChangeEmailValues = (e) => {
    const { name, value } = e.target;

    setChangeEmailValues({ ...changeEmailValues, [name]: value });
  };
  const handleChangePaswordValues = (e) => {
    const { name, value } = e.target;
    setChangePasswordValues({ ...changePasswordValues, [name]: value });
  };

  useEffect(() => {
    setToken(JSON.parse(localStorage.getItem("token")));
    setLoggedUser(JSON.parse(localStorage.getItem("loggedUser")));
    if (
      !JSON.parse(localStorage.getItem("token")) ||
      !JSON.parse(localStorage.getItem("loggedUser"))
    ) {
      navigate("/logowanie");
    }
  }, []);
  const changeEmail = async (e) => {
    e.preventDefault();
    const validated = handleValidation(changeEmailValues);
    setValidationMessages(validated);
    const isFormValid = handleIsValid(validated);
    console.log(isFormValid);
    if (
      changeEmailValues.email &&
      changeEmailValues.currentPassword &&
      token &&
      isFormValid
    ) {
      const config = getTokenConfig(token);
      axios
        .put(
          "http://localhost:5198/api/user/changeemail",
          {
            newEmail: changeEmailValues.email,
            currentPassword: changeEmailValues.currentPassword,
          },
          config
        )
        .then((response) => {
          console.log(response);
          setEmailChangeSuccseded(response.data);
          localStorage.removeItem("token");
          localStorage.removeItem("loggedUser");
          setTimeout(function () {
            window.location.reload();
          }, 5000);
        })
        .catch((err) => {
          if (err.response && err.response.status === 401) {
            console.log("Unauthorized access!");
            localStorage.removeItem("token");
            localStorage.removeItem("loggedUser");
            window.location.reload();
          } else {
            console.log("Error occurred:", err);
            setEmailChangeError(err);
          }
        });
    }
  };

  const changePassword = async (e) => {
    e.preventDefault();
    const validated = handleValidation(changePasswordValues);
    setValidationMessages(validated);
    const isFormValid = handleIsValid(validated);
    if (
      changePasswordValues.password &&
      changePasswordValues.currentPassword &&
      token &&
      isFormValid
    ) {
      console.log("test");
      const config = getTokenConfig(token);
      axios
        .put(
          "http://localhost:5198/api/user/changepassword",
          {
            newPassword: changePasswordValues.password,
            currentPassword: changePasswordValues.currentPassword,
          },
          config
        )
        .then((response) => {
          console.log(response);
          setPasswordChangeSuccseded(response.data);
          localStorage.removeItem("token");
          localStorage.removeItem("loggedUser");
          setTimeout(function () {
            window.location.reload();
          }, 5000);
        })
        .catch((err) => {
          if (err.response && err.response.status === 401) {
            console.log("Unauthorized access!");
            localStorage.removeItem("token");
            localStorage.removeItem("loggedUser");
            window.location.reload();
          } else {
            console.log("Error occurred:", err);
            setPasswordChangeError(err);
          }
        });
    }
  };

  return (
    <>
      {loggedUser ? (
        <div>
          <Topic title={"Witaj " + loggedUser.nickname + "!"} />
          <div className="border w-full p-6 m-auto rounded-md shadow-md lg:max-w-lg border-secondary bg-base-200">
            <div className="bg-base-300 ">
              <table className="text-xl">
                <tr>
                  <td className="font-bold">Nazwa:</td>
                  <td className="p-2">{loggedUser.nickname}</td>
                </tr>
                <tr>
                  <td className="font-bold">Email:</td>
                  <td className="p-2">{loggedUser.email}</td>
                </tr>
              </table>
              <div className="flex justify-end">
                <Link className="btn btn-info" to="/twoje-konfiguracje">
                  Twoje konfiguracje
                </Link>
              </div>
            </div>
            <div className="divider" />
            <form onSubmit={changeEmail}>
              <h2 className="text-2xl">Zmiana email</h2>
              {emailChangeSucceed && (
                <div role="alert" className="alert alert-success">
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
                      d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z"
                    />
                  </svg>
                  <span>
                    {emailChangeSucceed} Nastąpi wylogowanie z konta.
                    <span class="loading loading-dots loading-sm align-bottom"></span>
                  </span>
                </div>
              )}
              {emailChangeError && (
                <ErrorAlert
                  errorMessage={emailChangeError}
                  extraMessage="Zmiana adresu email nie powiodła się!"
                />
              )}
              <div>
                <TextInput
                  type="text"
                  title="Nowy email"
                  name="email"
                  value={changeEmailValues.email}
                  onChange={handleChangeEmailValues}
                  placeholder="Podaj nowy email"
                />
                {!validationMessages[0].isValid && (
                  <p className="text-error">{validationMessages[0].info}</p>
                )}
                <TextInput
                  type="password"
                  title="Hasło"
                  name="currentPassword"
                  value={changeEmailValues.currentPassword}
                  onChange={handleChangeEmailValues}
                  placeholder="Podaj swoje hasło"
                />
                <div className="pt-5">
                  <button className="btn btn-primary " type="submit">
                    Zapisz
                  </button>
                </div>
              </div>
            </form>
            <div className="divider" />
            <form onSubmit={changePassword}>
              <h2 className="text-2xl">Zmiana hasła</h2>
              {passwordChangeSucceed && (
                <div role="alert" className="alert alert-success">
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
                      d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z"
                    />
                  </svg>
                  <span>
                    {passwordChangeSucceed} Nastąpi wylogowanie z konta.
                    <span class="loading loading-dots loading-sm align-bottom"></span>
                  </span>
                </div>
              )}
              <div>
                <TextInput
                  name="password"
                  title="Nowe hasło"
                  type="password"
                  placeholder="Podaj nowe hasło"
                  value={changePasswordValues.password}
                  onChange={handleChangePaswordValues}
                />
                {!validationMessages[2].isValid && (
                  <p className="text-error">{validationMessages[2].info}</p>
                )}
                <TextInput
                  name="currentPassword"
                  title="Aktualne hasło"
                  type="password"
                  placeholder="Podaj aktualne hasło"
                  value={changePasswordValues.currentPassword}
                  onChange={handleChangePaswordValues}
                />
                <div className="pt-5">
                  <button className="btn btn-primary" type="submit">
                    Zapisz
                  </button>
                </div>
                {passwordChangeError && (
                  <ErrorAlert
                    errorMessage={passwordChangeError}
                    extraMessage="Zmiana hasła nie powiodła się!"
                  />
                )}
              </div>
            </form>
          </div>
        </div>
      ) : (
        <div>Ładowanie</div>
      )}
    </>
  );
};
