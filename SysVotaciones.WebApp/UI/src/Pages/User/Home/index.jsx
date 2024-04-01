import { useUser } from "src/hooks/useUser";
import { Link } from "wouter";

export const Home = () => {
  const { logout } = useUser();

  return (
    <>
      <header>
        <ul>
          <li>
            <Link to="/" replace aria-label="Cerrar sesión" onClick={logout}>
              Cerrar sesión
            </Link>
          </li>
        </ul>
      </header>
      <h1>Home</h1>
    </>
  );
};
