import NavBar from "./components/shared/NavBar";
import { Route, Routes } from "react-router-dom";
import { Home, Build, NotFound, Parts, ComponentsView } from "./pages";
import PartsTable from "./components/shared/PartsTable";
import pcParts from "./utils/constants/pcParts";
import PartDetail from "./components/shared/PartDetail";
import { useEffect, useState } from "react";
import { PcConfiguration } from "./utils/models";

function App() {
  let [pcConfiguration, setPcConfiguration] = useState(PcConfiguration);

  return (
    <div className="App">
      <header className="App-header">
        <NavBar />
      </header>
      <main className="pt-5">
        <Routes>
          <Route path="/" exect element={<Home />} />
          <Route
            path="build"
            exect
            element={
              <Build
                pcConfiguration={pcConfiguration}
                setPcConfiguration={setPcConfiguration}
              />
            }
          />
          <Route path="*" exect element={<NotFound />} />
          <Route path="parts" exect element={<Parts />} />

          {pcParts.map((part) => (
            <Route
              key={part.key}
              path={`/parts/${part.key}`}
              element={
                <ComponentsView
                  partType={part.key}
                  pcConfiguration={pcConfiguration}
                  setPcConfiguration={setPcConfiguration}
                />
              }
            />
          ))}
          {pcParts.map((part) => (
            <Route
              path={`/parts/${part.key}/:id`}
              element={
                <PartDetail
                  partType={part.key}
                  pcConfiguration={pcConfiguration}
                  setPcConfiguration={setPcConfiguration}
                />
              }
            />
          ))}
        </Routes>
      </main>
    </div>
  );
}

export default App;
