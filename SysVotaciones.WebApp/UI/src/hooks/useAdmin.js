import { useCallback, useContext, useState } from "react";
import { loginAdminService } from "src/Services/loginAdminService";
import { AdminContext } from "src/context/adminContext";

const updateJwt = (jwt) => {
  window.sessionStorage.setItem("jwt", jwt);
};

const removeJwt = () => {
  window.sessionStorage.removeItem("jwt");
};

export const useAdmin = () => {
  const context = useContext(AdminContext);

  if (context === undefined)
    throw new Error("useAdmin must be used whitin a CartProvider");

  const { jwt, setJwt } = context;
  const [errorMessage, setErrorMessage] = useState("");

  const login = useCallback(
    (admin) => {
      loginAdminService(admin)
        .then(({ token, message }) => {
          if (!token) setErrorMessage(message);

          updateJwt(token);
          setJwt(token);
        })
        .catch((err) => {
          setErrorMessage(err.message);
        });
    },
    [setJwt]
  );

  const logout = () => {
    removeJwt();
    setJwt(null);
  };

  return {
    isLogged: Boolean(jwt),
    jwt,
    errorMessage,
    setErrorMessage,
    login,
    logout,
  };
};
