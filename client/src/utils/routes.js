import React from "react";
import { Route, Routes } from "react-router-dom";
import {
  Home,
  Build,
  NotFound,
  Parts,
  PcPartsView,
  Configurations,
  UserConfigurations,
  AccountView,
} from "../pages";
import PartDetail from "../components/shared/PartDetail";
import pcParts from "./constants/pcParts";
import Login from "../pages/Login";
import Register from "../pages/Register";
import PcConfigurationDetails from "../components/PcConfigurations/PcConfigurationDetails";

const AppRoutes = ({
  pcConfiguration,
  setPcConfiguration,
  configurationInfo,
  loggedUser,
}) => (
  <Routes>
    <Route path="/" exect element={<Home />} />
    <Route
      path="build"
      exect
      element={
        <Build
          pcConfiguration={pcConfiguration}
          setPcConfiguration={setPcConfiguration}
          configurationInfo={configurationInfo}
          loggedUser={loggedUser}
        />
      }
    />
    <Route path="*" exect element={<NotFound />} />
    <Route path="parts" exect element={<Parts />} />
    <Route path="configurations" exect element={<Configurations />} />
    <Route
      path="configurations/:id"
      exect
      element={<PcConfigurationDetails />}
    />
    <Route path="konto" exect element={<AccountView />} />
    <Route
      path="twoje-konfiguracje"
      exect
      element={<UserConfigurations loggedUser={loggedUser} />}
    />
    <Route path="logowanie" exect element={<Login />} />
    <Route path="rejestracja" exect element={<Register />} />
    {pcParts.map((part) => (
      <Route
        key={part.key}
        path={`/parts/${part.key}`}
        element={
          <PcPartsView
            partType={part}
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
            partType={part}
            pcConfiguration={pcConfiguration}
            setPcConfiguration={setPcConfiguration}
          />
        }
      />
    ))}
  </Routes>
);

export default AppRoutes;
