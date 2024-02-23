import { ErrorMessage, Field, Form, Formik } from "formik";
import { loginService } from "src/Services/loginService.js";
import { ToggleablePasswordField } from "src/Components/ToggleablePasswordField/ToggleablePasswordField";

import "../Register/index.css";
import { useState } from "react";
import { Notification } from "../../Notification/Notification";

const initialValues = {
  studentCode: "",
  password: "",
};

const validateFiels = ({ studentCode, password }) => {
  const ERRORS = {};

  if (!studentCode) ERRORS.studentCode = "Este campo es obligatorio";

  if (!password) ERRORS.password = "Este campo es obligatorio";
  else if (password.length < 10)
    ERRORS.password = "Utiliza minimo 10 caracteres";

  return ERRORS;
};

export const Login = () => {
  const [error, setError] = useState("");

  const handleOnSubmit = ({ studentCode, password }) => {
    loginService({ studentCode, password })
      .then((token) => {
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

      <Formik
        initialValues={initialValues}
        validate={validateFiels}
        onSubmit={handleOnSubmit}
      >
        {() => (
          <Form className="form">
            <label>
              Codigo
              <Field
                name="studentCode"
                type="text"
                placeholder="Codigo de estudiante"
              />
              <ErrorMessage
                className="error"
                name="studentCode"
                component="small"
              />
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
