// routes.js
import React from "react";
import { Route, Routes } from "react-router-dom";
import {
  Home,
  Build,
  NotFound,
  Parts,
  ComponentsView,
  PartDetail,
} from "./pages";
import pcParts from "./utils/constants/pcParts";

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
);

export default AppRoutes;
