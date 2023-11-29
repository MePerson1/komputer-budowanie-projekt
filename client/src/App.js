import NavBar from "./components/shared/NavBar";
import { Route, Routes } from "react-router-dom";
import { Home, Build, NotFound, Parts, ComponentsView } from "./pages";
import PartsTable from "./components/shared/PartsTable";
import pcParts from "./utils/constants/pcParts";
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
          <Route path="/parts/motherboard/:id" exect element={<PartDetail />} />
          <Route
            path="parts/case"
            exect
            element={<ComponentsView partType={"case"} />}
          />
          <Route
            path="parts/case/:id"
            exect
            element={<PartDetail partType={"case"} />}
          />
          <Route
            path="parts/graphic-card"
            exect
            element={<ComponentsView partType={"graphic-card"} />}
          />
          <Route
            path="/parts/graphic-card/:id"
            exect
            element={<PartDetail partType={"graphic-card"} />}
          />
          <Route
            path="parts/cpu-cooling"
            exect
            element={<ComponentsView partType={"cpu-cooling"} />}
          />
          <Route
            path="parts/ram"
            exect
            element={<ComponentsView partType={"ram"} />}
          />
          <Route
            path="parts/power-supply"
            exect
            element={<ComponentsView partType={"power-supply"} />}
          />
          <Route
            path="parts/storage"
            exect
            element={<ComponentsView partType={"storage"} />}
          />

          <Route
            path="/parts/cpu-cooling/:id"
            exect
            element={<PartDetail partType={"cpu-cooling"} />}
          />
          <Route
            path="/parts/ram/:id"
            exect
            element={<PartDetail partType={"ram"} />}
          />
          <Route
            path="/parts/power-supply/:id"
            exect
            element={<PartDetail partType={"power-supply"} />}
          />
          <Route
            path="/parts/storage/:id"
            exect
            element={<PartDetail partType={"storage"} />}
          />
        </Routes>
      </main>
    </div>
  );
}

export default App;
