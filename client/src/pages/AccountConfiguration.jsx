import { useEffect, useState } from "react";
import Topic from "../components/shared/Topic";
import { TextInput } from "../components/shared/TextInput";
import { Link } from "react-router-dom";

export const AccountConfiguration = () => {
  const [loggedUser, setLoggedUser] = useState(null);
  const [newEmail, setNewEmail] = useState("");
  const [newPassword, setNewPassword] = useState("");

  useEffect(() => {
    var token = JSON.parse(localStorage.getItem("token"));
    setLoggedUser(JSON.parse(localStorage.getItem("loggedUser")));
  }, []);
  async function changeEmail() {}

  async function changePassword() {}

  return (
    <>
      {loggedUser && (
        <div>
          <Topic title={"Witaj " + loggedUser.nickname + "!"} />
          <div className="bg-base-200 border m-2 p-5">
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
            </div>
            <div className="divider" />
            <div>
              <h2 className="text-2xl">Zmiana email</h2>
              <div>
                <TextInput name="email" />
                <TextInput name="hasło" />
                <div className="pt-5">
                  <button className="btn btn-primary ">Zapisz</button>
                </div>
              </div>
            </div>
            <div className="divider" />
            <div>
              <h2 className="text-2xl">Zmiana hasła</h2>
              <div>
                <TextInput name="Nowe hasło" />
                <TextInput name="Potwierdź hasło" />
                <TextInput name="Stare hasło" />
                <div className="pt-5">
                  <button className="btn btn-primary ">Zapisz</button>
                </div>
              </div>
            </div>
          </div>
          <Link to="/twoje-konfiguracje">Twoje konfiguracje</Link>
        </div>
      )}
    </>
  );
};
