import { useEffect, useRef } from "react";

export function Tbody({ children, entries, ...props }) {
  const listRef = useRef(null);

  useEffect(() => {
    // Si hay nuevos datos, hacer scroll al último elemento de la lista
    if (listRef.current && listRef.current.lastElementChild) {
      listRef.current.lastElementChild.scrollIntoView({ behavior: "smooth" });
    }
  }, [entries]);

  return (
    <tbody ref={listRef} {...props}>
      {children}
    </tbody>
  );
}
