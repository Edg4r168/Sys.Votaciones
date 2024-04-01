import { ErrorMessage, Field, Form, Formik } from "formik";
import { ToggleablePasswordField } from "src/Components/ToggleablePasswordField/ToggleablePasswordField";

import { Notification } from "../../Notification/Notification";
import { useUser } from "src/hooks/useUser";
import { Redirect } from "wouter";

import "../Register/index.css";

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
  const { isLogged, errorMessage, login, setErrorMessage } = useUser();

  const handleOnSubmit = (user) => {
    return login(user);
  };

  return (
    <>
      {isLogged && <Redirect to="/home" replace />}

      {errorMessage && (
        <Notification
          duration={5000}
          type="error"
          onAnimationEnd={() => setErrorMessage("")}
        >
          {errorMessage}
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

            <button className="btn button" type="submit">
              Aceptar
            </button>
          </Form>
        )}
      </Formik>
    </>
  );
};
