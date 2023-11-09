import NavBar from "./components/shared/NavBar";
import { Route, Routes } from "react-router-dom";
import { Home, Build, NotFound } from "./pages";
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
        </Routes>
      </main>
    </div>
  );
}

export default App;
