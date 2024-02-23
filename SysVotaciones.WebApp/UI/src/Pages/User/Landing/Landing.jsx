import { Link } from "wouter";
import "./index.css";

export const Landing = () => {
  return (
    <>
      <section className="landing-page">
        <header className="nav-landing">
          <ul>
            <li>
              <Link
                to="/login-user"
                replace
                aria-label="Ir al inicion de sisión"
              >
                Iniciar sesión
              </Link>
            </li>
            <li>
              <Link to="/register" replace aria-label="Ir a crear cuenta">
                Registrarse
              </Link>
            </li>
          </ul>
        </header>

        <main>
          <h1 className="title">Vota por tus participantes favoritos</h1>
        </main>
      </section>
    </>
  );
};
