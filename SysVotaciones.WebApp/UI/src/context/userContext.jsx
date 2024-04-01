import { createContext, useState } from "react";

export const UserContext = createContext();

// eslint-disable-next-line react/prop-types
export function UserProvider({ children }) {
  const [jwt, setJwt] = useState(
    () => window.sessionStorage.getItem("jwt") ?? ""
  );

  return (
    <UserContext.Provider
      value={{
        jwt,
        setJwt,
      }}
    >
      {children}
    </UserContext.Provider>
  );
}
