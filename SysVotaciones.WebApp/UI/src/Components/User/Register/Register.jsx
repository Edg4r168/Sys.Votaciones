import "./index.css";
import { useLocation } from "wouter";
import { useState } from "react";
import { Formik, Form, Field, ErrorMessage } from "formik";

import { ListOfCareerYear } from "src/Components/User/ListOfCareerYear/ListOfCareerYear";
import ListOfCareer from "../ListOfCareer/ListOfCareer";
import { ToggleablePasswordField } from "src/Components/ToggleablePasswordField/ToggleablePasswordField";
import { Notification } from "src/Components/Notification/Notification";

import { useCareers } from "src/hooks/useCareers.js";
import { useCareerYears } from "src/hooks/useCareerYears.js";
import { registerService } from "src/Services/registerService.js";

const NOTIFICATION_DURATION = 1000;
const initialValues = {
  studentCode: "",
  careerId: "",
  careerYearId: "",
  password: "",
};

const validateFiels = (values) => {
  const ERRORS = {};

  if (!values.studentCode) ERRORS.studentCode = "Este campo es obligatorio";

  if (!values.careerId) ERRORS.careerId = "Este campo es obligatorio";

  if (!values.careerYearId) ERRORS.careerYearId = "Este campo es obligatorio";

  if (!values.password) {
    ERRORS.password = "Este campo es obligatorio";
  } else if (values.password.length < 10) {
    ERRORS.password = "Debes usar 10 caracteres como minimo";
  }

  return ERRORS;
};

export const Register = () => {
  const [registered, setRegistered] = useState(false);
  const [error, setError] = useState("");
  const { careers } = useCareers();
  const { careerYears } = useCareerYears();
  const [, navigate] = useLocation();

  const hanleOnSubmit = (values) => {
    // Deveulver una promesa y estara en submit hata que se resuelve
    return registerService(values)
      .then((res) => {
        setRegistered(res);
      })
      .catch((err) => {
        setError(err.message);
      });
  };

  return (
    <>
      {registered && (
        <Notification
          duration={NOTIFICATION_DURATION}
          onTimeout={() => navigate("/login-user")}
        >
          Te has registrado correctamente
        </Notification>
      )}

      {error && (
        <Notification duration={5000} onTimeout={() => setError("")}>
          {error}
        </Notification>
      )}

      <Formik
        initialValues={initialValues}
        validate={validateFiels}
        onSubmit={hanleOnSubmit}
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
                name="studentCode"
                component="small"
                className="error"
              />
            </label>

            <label>
              Carrera
              <Field as="select" name="careerId">
                <option value="" disabled>
                  Selcionar carrera
                </option>
                <option value="1">Selcionar carrera</option>

                {careers && <ListOfCareer careers={careers} />}
              </Field>
              <ErrorMessage
                name="careerId"
                component="small"
                className="error"
              />
            </label>

            <label>
              Año
              <Field as="select" name="careerYearId">
                <option value="" disabled>
                  Selcionar Año
                </option>
                <option value="1">Selcionar carrera</option>

                {careerYears && <ListOfCareerYear careerYears={careerYears} />}
              </Field>
              <ErrorMessage
                name="careerYearId"
                component="small"
                className="error"
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
