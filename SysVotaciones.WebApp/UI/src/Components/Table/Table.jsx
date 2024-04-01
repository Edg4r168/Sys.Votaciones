// import { EditIcon, TrashIcon } from "../Icons";
import "./index.css";

export function Table({ children, className = "", ...props }) {
  return (
    <>
      <div className="container-table border">
        <table {...props} className={`table ${className}`}>
          {children}
        </table>
      </div>
    </>
  );
}
