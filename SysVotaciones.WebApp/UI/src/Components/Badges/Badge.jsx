import "./index.css";

export function Badge({ children, className }) {
  return <span className={`badge ${className}`}>{children}</span>;
}
