import "./index.css";

export function Form({ children, showForm = true, ...props }) {
  return (
    <>
      {showForm ? (
        <form className="admin-form" {...props}>
          {children}
        </form>
      ) : null}
    </>
  );
}
