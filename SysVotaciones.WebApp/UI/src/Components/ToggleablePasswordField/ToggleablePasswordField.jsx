import { Field, ErrorMessage } from "formik";
import { useState } from "react";

import { LogoShowPass } from "./LogoShowPass";
import { LogoHidePass } from "./LogoHidePass";

import "./index.css";

export const ToggleablePasswordField = () => {
  const [showPass, setShowPass] = useState(false);

  return (
    <>
      <label>
        Contraseña
        <Field
          type={showPass ? "text" : "password"}
          name="password"
          placeholder="Constraseña"
        />
        <ErrorMessage name="password" component="small" className="error" />
      </label>
      <button
        className="btn-showPass"
        type="button"
        onClick={() => {
          setShowPass(!showPass);
        }}
      >
        {showPass ? <LogoShowPass /> : <LogoHidePass />}
      </button>
    </>
  );
};
