import { useEffect, useRef, useState } from "react";
import "./index.css";
import { UserCircleIcon } from "../Icons";

export function DropDownMenu({ children }) {
  const [isOpen, setIsOpen] = useState();
  const menuRef = useRef(null);

  useEffect(() => {
    const handleOutsideClick = (event) => {
      const elementClicked = event.target;
      const firtsChild = menuRef.current.children[0];
      const secondChild = menuRef.current.children[1];

      if (
        menuRef.current &&
        firtsChild !== elementClicked &&
        secondChild !== elementClicked
      ) {
        setIsOpen(false);
      }
    };

    document.addEventListener("click", handleOutsideClick);

    return () => {
      document.removeEventListener("click", handleOutsideClick);
    };
  }, []);

  return (
    <div className="menu-container" ref={menuRef}>
      <button onClick={() => setIsOpen(!isOpen)}>
        <UserCircleIcon />
      </button>

      <ul className={`menu ${isOpen ? "active" : ""}`}>
        <h2>Mi perfil</h2>
        <div></div>
        {children}
        <li>Cerrar sesión</li>
        <li>Cerrar sesión</li>
        <div></div>
        <li>Cerrar sesión</li>
      </ul>
    </div>
  );
}
