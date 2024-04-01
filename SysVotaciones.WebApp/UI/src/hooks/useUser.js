import { useCallback, useContext, useState } from "react";
import { loginService } from "src/Services/loginService";
import { UserContext } from "src/context/userContext";

const updateJwt = (jwt) => {
  window.sessionStorage.setItem("jwt", jwt);
};

const removeJwt = () => {
  window.sessionStorage.removeItem("jwt");
};

export const useUser = () => {
  const context = useContext(UserContext);

  if (context === undefined)
    throw new Error("useUser must be used whitin a CartProvider");

  const { jwt, setJwt } = context;
  const [errorMessage, setErrorMessage] = useState("");

  const login = useCallback(
    (user) => {
      setErrorMessage(null);

      loginService(user)
        .then(({ token, message }) => {
          if (!token) return setErrorMessage(message);

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
    errorMessage,
    login,
    logout,
    setErrorMessage,
  };
};
