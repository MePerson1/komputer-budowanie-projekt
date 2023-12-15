import React from "react";
import { Route, Routes } from "react-router-dom";
import {
  Home,
  Build,
  NotFound,
  Parts,
  ComponentsView,
  Configurations,
} from "../pages";
import PartDetail from "../components/shared/PartDetail";
import pcParts from "./constants/pcParts";

const AppRoutes = ({
  pcConfiguration,
  setPcConfiguration,
  configurationInfo,
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
        />
      }
    />
    <Route path="*" exect element={<NotFound />} />
    <Route path="parts" exect element={<Parts />} />
    <Route path="configurations" exect element={<Configurations />} />

    {pcParts.map((part) => (
      <Route
        key={part.key}
        path={`/parts/${part.key}`}
        element={
          <ComponentsView
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
