import { useState } from "react";
import { getCurrentPath } from "src/utils/getCurrentPath";

const EVENTS = {
  PUSHSTATE: "pushState",
  POPSTATE: "popstate",
};

export function Match({ path, component, default: Default = () => "404" }) {
  const [currentPath, setCurrentPath] = useState(getCurrentPath);

  useState(() => {
    const onLocationChange = () => {
      setCurrentPath(getCurrentPath());
    };

    window.addEventListener(EVENTS.PUSHSTATE, onLocationChange);
    window.addEventListener(EVENTS.POPSTATE, onLocationChange);

    return () => {
      window.removeEventListener(EVENTS.PUSHSTATE, onLocationChange);
      window.removeEventListener(EVENTS.POPSTATE, onLocationChange);
    };
  }, []);

  return path === currentPath ? component : null;
}
