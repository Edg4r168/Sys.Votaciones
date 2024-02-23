import "./index.css";
import { Login } from "src/Components/User/Login/Login";

export const LoginPageUser = () => {
  return (
    <>
      <section className="login-page">
        <h1>Iniciar sesión</h1>
        <Login />
      </section>
    </>
  );
};
