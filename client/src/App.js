import NavBar from "./components/shared/NavBar/NavBar";
import AppRoutes from "./utils/routes";
import { useEffect, useState } from "react";
import { PcConfiguration, Toast } from "./utils/models";
import axios from "axios";
import { Footer } from "./components/shared/Footer";
function App() {
  let [pcConfiguration, setPcConfiguration] = useState(PcConfiguration);
  const [user, setUser] = useState(null);
  useEffect(() => {
    const token = JSON.parse(localStorage.getItem("token"));

    console.log(token);
    if (token !== null) {
      const config = { headers: { Authorization: `Bearer ${token}` } };
      axios
        .get("http://localhost:5198/api/user/userInfo", config)
        .then((response) => {
          console.log(response.data);
          setUser(response.data);
        })
        .catch((err) => {
          console.log(err);
          localStorage.removeItem("token");
        });
    }
  }, []);

  return (
    <div className="flex flex-col h-screen">
      <header className="top-0">
        <NavBar loggedUser={user} />
      </header>
      <main className="flex-grow">
        <AppRoutes
          pcConfiguration={pcConfiguration}
          setPcConfiguration={setPcConfiguration}
        />
      </main>
      <Footer />
    </div>
  );
}

export default App;
