import { ErrorMessage, Field, Form, Formik } from "formik";
import { ToggleablePasswordField } from "../../ToggleablePasswordField/ToggleablePasswordField";

import "src/Components/User/Register/index.css";
import { Notification } from "src/Components/Notification/Notification";
import { useAdmin } from "src/hooks/useAdmin";
import { Redirect } from "wouter";

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
  const { isLogged, login, errorMessage, setErrorMessage } = useAdmin();

  const handleOnSubmit = (admin) => {
    return login(admin);
  };

  return (
    <>
      {isLogged && <Redirect to="/dashboard" replace />}

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
