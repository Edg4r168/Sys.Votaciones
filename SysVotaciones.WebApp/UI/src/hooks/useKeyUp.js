import { useEffect } from "react";

export const useKeyUp = (callback) => {
  useEffect(() => {
    const handleKeyUp = (e) => {
      if (e.key === "Enter") callback(e);
    };

    window.addEventListener("keyup", handleKeyUp);

    return () => window.removeEventListener("keyup", handleKeyUp);
  }, [callback]);
};
