import NavBar from "./components/shared/NavBar/NavBar";
import AppRoutes from "./utils/routes";
import { useEffect, useState } from "react";
import { PcConfiguration, Toast } from "./utils/models";
import axios from "axios";
import { Footer } from "./components/shared/Footer";
function App() {
  let [pcConfiguration, setPcConfiguration] = useState(PcConfiguration);

  return (
    <div className="App">
      <header className="App-header sticky top-0">
        <NavBar />
      </header>
      <main>
        <AppRoutes
          pcConfiguration={pcConfiguration}
          setPcConfiguration={setPcConfiguration}
        />
        <Footer />
      </main>
    </div>
  );
}

export default App;
