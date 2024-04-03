import { ButtonGreen, ButtonRed } from "src/Components/Layout/Buttons";
import { Form } from "../Forms/Form";
import { ButtonsGroup, FormGroup } from "../Forms/FormGroup";
import { useForm } from "src/hooks/useForm";
import { useTable } from "src/hooks/useTable";
import { useCareers } from "src/hooks/useCareers";
import { useCareerYears } from "src/hooks/useCareerYears";

export function FormSave({ onCancel, onSubmit }) {
  const { save } = useTable();
  const { careers } = useCareers();
  const { careerYears } = useCareerYears();
  const { values, onChange, setValues } = useForm({
    studentCode: "",
    careerYearId: "",
    careerYear: "",
    careerId: "",
    career: "",
  });

  const handleChange = (e) => {
    const { value: id, name } = e.target;
    let newValue;
    let propertyName;

    if (name === "careerYearId") {
      propertyName = "careerYear";
      newValue = careerYears.find(
        (careerYear) => careerYear.id === +id
      )?.careerYear;
    } else {
      propertyName = "career";
      newValue = careers.find((career) => career.id === +id)?.name;
    }

    setValues({
      ...values,
      [name]: id,
      [propertyName]: newValue,
    });
  };

  const handleOnSubmit = (e) => {
    e.preventDefault();
    save(values);
    onSubmit && onSubmit();
  };

  return (
    <Form onSubmit={handleOnSubmit}>
      <FormGroup>
        <label htmlFor="studentCode">Codigo de estudiante</label>
        <input
          name="studentCode"
          id="studentCode"
          type="text"
          value={values.studentCode}
          onChange={onChange}
        />
      </FormGroup>

      <FormGroup>
        <label htmlFor="careerYearId">Año</label>
        <select
          name="careerYearId"
          id="careerYearId"
          value={values.careerYearId}
          onChange={handleChange}
        >
          <option>Seleccionar</option>
          {careerYears.map((careerYear) => {
            return (
              <option key={careerYear.id} value={careerYear.id}>
                {careerYear.careerYear}
              </option>
            );
          })}
        </select>
      </FormGroup>

      <FormGroup>
        <label htmlFor="careerId">Carrera</label>
        <select
          name="careerId"
          id="careerId"
          value={values.careerId}
          onChange={handleChange}
        >
          <option>Seleccionar</option>
          {careers.map((career) => {
            return (
              <option key={career.id} value={career.id}>
                {career.name}
              </option>
            );
          })}
        </select>
      </FormGroup>

      <ButtonsGroup>
        <ButtonGreen>Aceptar</ButtonGreen>
        <ButtonRed type="button" onClick={() => onCancel && onCancel()}>
          Canelar
        </ButtonRed>
      </ButtonsGroup>
    </Form>
  );
}
