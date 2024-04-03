import { Form } from "../Forms/Form";
import { ButtonsGroup, FormGroup } from "../Forms/FormGroup";
import { useForm } from "src/hooks/useForm";
import { ButtonGreen, ButtonRed } from "src/Components/Layout/Buttons";
import { useTable } from "src/hooks/useTable";

export function FormEdit({ onCancel, onSubmit }) {
  const { entry, update } = useTable();

  const { values, setValues } = useForm(entry);

  const handleOnSubmit = (e) => {
    e.preventDefault();

    update(values);
    onSubmit && onSubmit();
  };

  const handleChangeYear = (e) => {
    // const { value } = e.target;
    // const careerYear = careerYears.find(
    //   (careerYear) => careerYear.id === +value
    // )?.careerYear;
    // setValues({
    //   ...values,
    //   careerYearId: value,
    //   careerYear,
    // });
  };

  const handleChangeCareer = (e) => {
    // const { value } = e.target;
    // const career = careers.find((career) => career.id === +value)?.name;
    // setValues({
    //   ...values,
    //   careerId: value,
    //   career,
    // });
  };

  return (
    <>
      <Form onSubmit={handleOnSubmit}>
        <FormGroup>
          <label htmlFor="studentCode">Codigo de estudiante</label>
          <input
            id="studentCode"
            name="studentCode"
            type="text"
            value={values.studentCode}
            disabled
          />
        </FormGroup>

        <FormGroup>
          <label htmlFor="careerYearId">Año</label>
          <select
            id="careerYearId"
            name="careerYearId"
            value={values.careerYearId}
            onChange={handleChangeYear}
          >
            <option value={entry.careerYearId}>{entry.careerYear}</option>
            {/* {careerYears.map((careerYear) => {
              if (careerYear.id === entry.careerYearId) return;

              return (
                <option key={careerYear.id} value={careerYear.id}>
                  {careerYear.careerYear}
                </option>
              );
            })} */}
          </select>
        </FormGroup>

        <FormGroup>
          <label htmlFor="careerId">Carrera</label>
          <select
            id="careerId"
            name="careerId"
            value={values.careerId}
            onChange={handleChangeCareer}
          >
            <option value={entry.careerId}>{entry.career}</option>
            {/* {careers.map((career) => {
              if (career.id === entry.careerId) return;

              return (
                <option key={career.id} value={career.id}>
                  {career.name}
                </option>
              );
            })} */}
          </select>
        </FormGroup>

        <ButtonsGroup>
          <ButtonGreen>Actualizar</ButtonGreen>
          <ButtonRed type="button" onClick={() => onCancel && onCancel()}>
            Canelar
          </ButtonRed>
        </ButtonsGroup>
      </Form>
    </>
  );
}
