import NavBar from "./components/shared/NavBar";
import AppRoutes from "./utils/routes";
import { useEffect, useState } from "react";
import { PcConfiguration, Toast } from "./utils/models";
import axios from "axios";
function App() {
  let [pcConfiguration, setPcConfiguration] = useState(PcConfiguration);

  return (
    <div className="App">
      <header className="App-header">
        <NavBar />
      </header>
      <main>
        <AppRoutes
          pcConfiguration={pcConfiguration}
          setPcConfiguration={setPcConfiguration}
        />
      </main>
    </div>
  );
}

export default App;
