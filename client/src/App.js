import NavBar from "./components/shared/NavBar";
import { Route, Routes } from "react-router-dom";
import Home from "./pages/Home";
import NotFound from "./pages/NotFound";
function App() {
  return (
    <div className="App">
      <header className="App-header">
        <NavBar />
      </header>
      <main className="pt-5">
        <Routes>
          <Route path="/" exect element={<Home />} />
          <Route path="*" exect element={<NotFound />} />
        </Routes>
      </main>
    </div>
  );
}

export default App;
