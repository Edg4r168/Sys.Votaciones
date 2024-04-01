import "./Buttons.css";

export function ButtonDefault({ children, ...props }) {
  return (
    <>
      <button className="buttonDefault standardStyles" {...props}>
        {children}
      </button>
    </>
  );
}

export function ButtonSearch({ children, ...props }) {
  return (
    <>
      <button className="buttonSearch standardStyles" {...props}>
        {children}
      </button>
    </>
  );
}

export function RoundButtonAdd({ children, ...props }) {
  return (
    <button className="roundButtonAdd" {...props}>
      {children}
    </button>
  );
}

export function ButtonRed({ children, ...props }) {
  return (
    <button className="buttonRed standardStyles" {...props}>
      {children}
    </button>
  );
}

export function ButtonGreen({ children, ...props }) {
  return (
    <button className="buttonGreen standardStyles" {...props}>
      {children}
    </button>
  );
}

export function ButtonShowMore({ children, ...props }) {
  return (
    <button className="buttonShowMore" {...props}>
      {children}
    </button>
  );
}
