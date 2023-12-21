import NavBar from "./components/shared/NavBar/NavBar";
import AppRoutes from "./utils/routes";
import { useEffect, useState } from "react";
import { PcConfiguration, Toast } from "./utils/models";
import axios from "axios";
import { Footer } from "./components/shared/Footer";
function App() {
  let [pcConfiguration, setPcConfiguration] = useState(PcConfiguration);

  return (
    <div className="flex flex-col h-screen">
      <header className="top-0">
        <NavBar />
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
