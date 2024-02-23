import { ErrorMessage, Field, Form, Formik } from "formik";
import { loginAdService } from "src/Services/loginAdService";
import { ToggleablePasswordField } from "../../ToggleablePasswordField/ToggleablePasswordField";

import "src/Components/User/Register/index.css";
import { useState } from "react";
import { Notification } from "src/Components/Notification/Notification";

const initialValues = {
  user: "",
  password: "",
};

const validateFiels = ({ user, password }) => {
  const ERRORS = {};

  if (!user) ERRORS.user = "Este campo es obligatorio";

  if (!password) ERRORS.password = "Este campo es obligatorio";
  else if (password.length < 10)
    ERRORS.password = "Utiliza minimo 10 caracteres";

  return ERRORS;
};

export const Login = () => {
  const [error, setError] = useState("");
  const [logged, setLogged] = useState("");

  const handleOnSubmit = ({ user, password }) => {
    loginAdService({ user, password })
      .then(({ token, message }) => {
        if (!token) setError(message);
        setLogged(message);
        console.log(token);
      })
      .catch((err) => {
        setError(err.message);
      });
  };

  return (
    <>
      {error && (
        <Notification duration={5000} onTimeout={() => setError("")}>
          {error}
        </Notification>
      )}

      {logged && (
        <Notification duration={3000} onTimeout={() => setLogged("")}>
          {logged}
        </Notification>
      )}

      <Formik
        initialValues={initialValues}
        validate={validateFiels}
        onSubmit={handleOnSubmit}
      >
        {() => (
          <Form className="form">
            <label>
              Usuario
              <Field name="user" type="text" placeholder="Usuario" />
              <ErrorMessage className="error" name="user" component="small" />
            </label>

            <div className="container-elmentsPass">
              <ToggleablePasswordField />
            </div>

            <button className="btn" type="submit">
              Aceptar
            </button>
          </Form>
        )}
      </Formik>
    </>
  );
};
