import NavBar from "./components/shared/NavBar";
import { Route, Routes } from "react-router-dom";
import { Home, Build, NotFound, Parts, ComponentsView } from "./pages";
import PartsTable from "./components/shared/PartsTable";
import pcParts from "./constants/pcParts";
import PartDetail from "./components/shared/PartDetail";

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <NavBar />
      </header>
      <main className="pt-5">
        <Routes>
          <Route path="/" exect element={<Home />} />
          <Route path="build" exect element={<Build />} />
          <Route path="*" exect element={<NotFound />} />
          <Route path="parts" exect element={<Parts />} />

          <Route
            path="parts/cpu"
            exect
            element={<ComponentsView partType={"cpu"} />}
          />
          <Route path="/parts/cpu/:id" exect element={<PartDetail />} />
          <Route
            path="parts/motherboard"
            exect
            element={<ComponentsView partType={"motherboard"} />}
          />
          <Route
            path="parts/case"
            exect
            element={<ComponentsView partType={"case"} />}
          />
          <Route
            path="parts/graphicCard"
            exect
            element={<ComponentsView partType={"graphicCard"} />}
          />
          <Route
            path="parts/cpuCooling"
            exect
            element={<ComponentsView partType={"cpuCooling"} />}
          />
          <Route
            path="parts/ram"
            exect
            element={<ComponentsView partType={"ram"} />}
          />
          <Route
            path="parts/powerSupply"
            exect
            element={<ComponentsView partType={"powerSupply"} />}
          />
          <Route
            path="parts/storage"
            exect
            element={<ComponentsView partType={"storage"} />}
          />
        </Routes>
      </main>
    </div>
  );
}

export default App;
