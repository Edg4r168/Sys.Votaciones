import { createContext, useState } from "react";

export const AdminContext = createContext();

export function AdminProvider({ children }) {
  const [jwt, setJwt] = useState(
    () => window.sessionStorage.getItem("jwt") ?? ""
  );

  return (
    <AdminContext.Provider
      value={{
        jwt,
        setJwt,
      }}
    >
      {children}
    </AdminContext.Provider>
  );
}
